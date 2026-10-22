using SoftMochiPet.Core;
using SoftMochiPet.Interop;
using SoftMochiPet.Services;
using DrawingPoint = System.Drawing.Point;

namespace SoftMochiPet;

public partial class MainWindow
{
    private readonly BehaviorControlSession? _behaviorSession;
    private (BehaviorControlAction Action, string Character)? _pendingBehavior;
    private BehaviorControlAction? _activeBehavior;
    private bool _behaviorAutonomyEnabled;
    private bool _behaviorHatOnly;
    private bool _behaviorRequestFailed;
    public bool IsBehaviorControlMode => _behaviorSession is not null;

    private void InitializeBehaviorControl()
    {
        if (!IsBehaviorControlMode) return;
        _trayIcon.BehaviorControlRequested += QueueBehaviorControl;
        _trayIcon.BehaviorControlStopRequested += () => QueueBehaviorControl(BehaviorControlAction.Stop);
        _trayIcon.BehaviorAutonomyChanged += enabled =>
        {
            _behaviorAutonomyEnabled = enabled;
            ScheduleNextAutonomousDecision(4, 4);
            SetBehaviorControlStatus(enabled ? "Autonomous daily activity is on" : "Autonomous daily activity is off; pick a behavior directly");
        };
        SetBehaviorControlStatus("Right-click to choose a behavior · autonomous activity off by default");
    }

    private void QueueBehaviorControl(BehaviorControlAction action)
    {
        if (!IsBehaviorControlMode || _isClosing) return;
        _pendingBehavior = (action, _character.Id);
        SetBehaviorControlStatus("Runs after the menu closes: " + BehaviorControlCatalog.Get(action).Title);
    }

    private void SetBehaviorControlStatus(string text)
    {
        if (IsBehaviorControlMode) _trayIcon.SetBehaviorControlStatus(text);
    }

    private WindowSurface? BehaviorSupport()
    {
        var foot = PrankFoot();
        return _surfaceProvider.Surfaces.FirstOrDefault(surface =>
            surface.SourceHandle == _supportHandle && surface.IsDesktopFloor == _supportIsDesktopFloor &&
            surface.ContainsX((int)Math.Round(foot.X)) && Math.Abs(surface.Top - foot.Y) < 20);
    }

    private string? BehaviorUnavailableReason(BehaviorControlAction action)
    {
        var item = BehaviorControlCatalog.Get(action);
        if (!item.Supports(_character)) return "This character does not have that behavior";
        if (action == BehaviorControlAction.Stop) return null;
        if (_sessionLocked || _isClosing) return "Not available in this session";
        if (_isDragging) return "Let go of the pet first";
        if (action == BehaviorControlAction.ReturnWindows)
            return !HasHeldPrank ? "There is no window in the hat" : _manualRestoreInProgress ? "Returning the window" : null;
        if (_prankPhase != PrankPhase.None || _prankAwaitingSystem) return "Stop the current prank first, or wait until she stows her hat";
        if (_animator.Hatless) return "Wait for her to pick her hat back up";
        if (_settings.QuietMode && action is not (BehaviorControlAction.Stand or BehaviorControlAction.Sleep or BehaviorControlAction.Wake))
            return "Turn off quiet mode first";
        var needs = item.Requirements;
        if (needs.HasFlag(BehaviorControlRequirements.Awake) && _state is PetState.Sleeping or PetState.Waking)
            return "Wake her first";
        if (needs.HasFlag(BehaviorControlRequirements.Sleeping) && _state != PetState.Sleeping)
            return "She is not asleep right now";
        if (needs.HasFlag(BehaviorControlRequirements.EmptyHat) && HasHeldPrank)
            return "The hat already holds a window; give it back first";
        if (needs.HasFlag(BehaviorControlRequirements.OutsideEnclosure) && _enclosureHandle != IntPtr.Zero)
            return "Drag her off the window first";
        if (needs.HasFlag(BehaviorControlRequirements.FoodEnabled) && _settings.FastingMode)
            return "Turn off fasting mode first";
        if (needs.HasFlag(BehaviorControlRequirements.GroundSupport) && BehaviorSupport() is null)
            return "Let her find her footing first";
        if (needs.HasFlag(BehaviorControlRequirements.WindowPlatform) && _supportIsDesktopFloor)
            return "She needs to be standing on a real window top edge first";
        return null;
    }

    private void RefreshBehaviorControlAvailability()
    {
        if (!IsBehaviorControlMode) return;
        _trayIcon.SetBehaviorControlAvailability(BehaviorControlCatalog.ForCharacter(_character)
            .ToDictionary(item => item.Action, item => BehaviorUnavailableReason(item.Action) is null));
    }

    private void ExecutePendingBehavior()
    {
        if (!IsBehaviorControlMode || _trayIcon.IsMenuOpen || _pendingBehavior is not { } request) return;
        _pendingBehavior = null;
        if (request.Character != _character.Id || _pendingCharacter is not null || _isClosing) return;
        var action = request.Action;
        if (BehaviorUnavailableReason(action) is { } unavailable)
        {
            RejectBehavior(unavailable);
            return;
        }
        _behaviorRequestFailed = false;
        try
        {
            if (action == BehaviorControlAction.Stop)
            {
                StopBehaviorControl();
                return;
            }
            if (action == BehaviorControlAction.ReturnWindows)
            {
                _activeBehavior = action;
                RequestWindowReturn();
                SetBehaviorControlStatus("Returning the original window");
                return;
            }
            // Commands replace ordinary movement, never another native-window
            // operation. The latter requires the explicit stop/return entries.
            if (action != BehaviorControlAction.Wake) ResetOrdinaryBehavior();
            _activeBehavior = action;
            var started = StartControlledBehavior(action);
            if (!started)
            {
                _activeBehavior = null;
                if (!_behaviorRequestFailed) RejectBehavior("Not possible right now; check the surface or the target");
                return;
            }
            if (!_behaviorRequestFailed)
                SetBehaviorControlStatus("Running: " + BehaviorControlCatalog.Get(action).Title);
            DiagnosticsLog.WriteEvent("BehaviorControlStarted", ("Action", action), ("Character", _character.Id),
                ("State", _state), ("Animation", _animator.CurrentClip));
        }
        catch (Exception exception)
        {
            CancelWindowPrank("BehaviorControlStop", false, false);
            ClearFoodInteractions();
            _activeBehavior = null;
            ReturnToIdle();
            RejectBehavior("The behavior did not finish; check the log");
            DiagnosticsLog.WriteEvent("BehaviorControlFailed", ("Action", action), ("Reason", exception.GetType().Name));
        }
    }

    private bool StartControlledBehavior(BehaviorControlAction action)
    {
        switch (action)
        {
            case BehaviorControlAction.Stand: return true;
            case BehaviorControlAction.Walk: return StartGroundMove(MovementPurpose.Wander);
            case BehaviorControlAction.Run:
                if (!StartGroundMove(MovementPurpose.Wander)) return false;
                _animator.Play("run");
                return true;
            case BehaviorControlAction.Explore: return StartGroundMove(MovementPurpose.Explore);
            case BehaviorControlAction.Patrol: return StartContinuousPatrol(preferClimb: false);
            case BehaviorControlAction.Curious: BeginCurious(false); return true;
            case BehaviorControlAction.Hop: return BeginTerrainHop();
            case BehaviorControlAction.Roll: return BeginRoll();
            case BehaviorControlAction.Climb: return StartControlledClimb();
            case BehaviorControlAction.SlideDown: return StartControlledEdge(slide: true);
            case BehaviorControlAction.JumpDown: return StartControlledEdge(slide: false);
            case BehaviorControlAction.Sleep: BeginSleep(); return _state == PetState.Sleeping;
            case BehaviorControlAction.Wake: BeginWake(); return true;
            case BehaviorControlAction.FreeFall: return StartControlledFall(toss: false);
            case BehaviorControlAction.Toss: return StartControlledFall(toss: true);
            case BehaviorControlAction.Hungry:
                _lifeState.Hunger = 100;
                _lifeState.Fullness = 0;
                BeginCurious(false, hungryAppeal: true);
                SaveLifeState();
                UpdateHungerDisplay(force: true);
                return true;
            case BehaviorControlAction.LickIcon:
                if (TryBeginIconLick(manualRequest: true)) return true;
                RejectBehavior("No visible desktop icons; show the desktop and try again");
                return false;
            case BehaviorControlAction.EatIcon: return StartControlledIconMeal();
            case BehaviorControlAction.Satisfied:
                _state = PetState.Satisfied;
                _stateElapsed = 0;
                _animator.Play("satisfied");
                return true;
            case BehaviorControlAction.Kick: return StartControlledPrank(PrankKind.Kick);
            case BehaviorControlAction.Punch: return StartControlledPrank(PrankKind.Punch);
            case BehaviorControlAction.Charge: return StartControlledPrank(PrankKind.Charge);
            case BehaviorControlAction.HatStore: return StartControlledPrank(PrankKind.Hat);
            case BehaviorControlAction.Shatter: return StartControlledPrank(PrankKind.Shatter);
            case BehaviorControlAction.Tear: return StartControlledPrank(PrankKind.Tear);
            case BehaviorControlAction.Tease:
                UpdateMischief(0);
                if (!_mischief.Tease()) { RejectBehavior("Teasing or the big move is still on cooldown"); return false; }
                PlayMischiefVoice(MischiefCue.Teased);
                SaveMischiefState();
                return true;
            case BehaviorControlAction.FillMischief:
                if (!_mischief.FillForDirectControl()) return false;
                SaveMischiefState();
                return true;
            case BehaviorControlAction.ThrowAndRecoverHat: return StartControlledHat();
            default: return false;
        }
    }

    private void ResetOrdinaryBehavior()
    {
        _activeBehavior = null;
        ClearFoodInteractions();
        _velocityX = _velocityY = 0;
        _movementPixelRemainderX = _movementPixelRemainderY = 0;
        _wakeForMeal = _wakeForManualLick = false;
        ReturnToIdle();
    }

    private void StopBehaviorControl()
    {
        _pendingBehavior = null;
        _activeBehavior = null;
        if (_isDragging)
        {
            _isDragging = false;
            ReleaseMouseCapture();
        }
        CancelWindowPrank("BehaviorControlStop", false, false);
        ResetOrdinaryBehavior();
        if (!TryResolveGroundSupport(false)) BeginFall(reason: "BehaviorControlStop");
        _voice.Stop();
        SetBehaviorControlStatus(HasHeldPrank ? "Stopped · the hat still holds a window; right-click to give it back" : "Stopped; waiting for a behavior");
        DiagnosticsLog.WriteEvent("BehaviorControlStopped", ("HatOccupied", HasHeldPrank));
    }

    private void RejectBehavior(string reason)
    {
        _behaviorRequestFailed = true;
        SetBehaviorControlStatus(reason);
        DiagnosticsLog.WriteEvent("BehaviorControlUnavailable", ("Reason", reason));
    }

    private void UpdateBehaviorControlCompletion()
    {
        if (!IsBehaviorControlMode || _activeBehavior is not { } action || _state != PetState.Idle || _prankPhase != PrankPhase.None) return;
        _activeBehavior = null;
        if (_behaviorRequestFailed) return;
        SetBehaviorControlStatus("Finished: " + BehaviorControlCatalog.Get(action).Title + (HasHeldPrank ? " · window left in the hat" : ""));
        DiagnosticsLog.WriteEvent("BehaviorControlCompleted", ("Action", action), ("HatOccupied", HasHeldPrank));
    }

    private bool StartControlledClimb()
    {
        RefreshSurfaceMap(force: true);
        var surface = BehaviorSupport();
        if (surface is null) return false;
        var foot = PrankFoot();
        var plan = WindowClimbPlanner.Choose(_surfaceProvider.Surfaces, surface, (int)Math.Round(foot.X), PetPhysicalScale(), 0);
        if (plan is null) { RejectBehavior("No reachable window edge nearby"); return false; }
        _pendingClimb = plan;
        _movementPurpose = MovementPurpose.ClimbApproach;
        _targetScreenX = plan.ApproachX;
        _targetScreenY = surface.Top;
        _state = PetState.Running;
        _stateElapsed = 0;
        _animator.Play("walk");
        return true;
    }

    private bool StartControlledEdge(bool slide)
    {
        RefreshSurfaceMap(force: true);
        var surface = BehaviorSupport();
        if (surface is null || surface.IsDesktopFloor) { RejectBehavior("Put her on a real window top edge first"); return false; }
        var foot = PrankFoot();
        _patrolDirection = foot.X - surface.Left < surface.Right - foot.X ? -1 : 1;
        _targetScreenX = _patrolDirection < 0 ? surface.Left + 20 : surface.Right - 20;
        _targetScreenY = surface.Top;
        _movementPurpose = slide ? MovementPurpose.EdgeSlideApproach : MovementPurpose.EdgeJumpApproach;
        _state = PetState.Running;
        _stateElapsed = 0;
        _animator.Play("walk");
        return true;
    }

    private bool StartControlledFall(bool toss)
    {
        var monitor = DisplayGeometry.FromWindow(_windowHandle);
        if (monitor is null) return false;
        if (!toss)
        {
            var available = PrankFoot().Y - monitor.WorkArea.Top - Height * monitor.Scale;
            if (available < 36) { RejectBehavior("Not enough room above; place her lower"); return false; }
            DisplayGeometry.OffsetWindowPhysical(_windowHandle, 0, -(int)Math.Min(available, 220 * monitor.Scale));
        }
        BeginFall(toss ? -650 * monitor.Scale : 0, toss ? "BehaviorControlToss" : "BehaviorControlFall",
            toss ? (FacingTransform.ScaleX < 0 ? -1 : 1) * 90 * monitor.Scale : 0);
        if (toss) _voice.Play(VoiceCue.Scream, VoicePriority.Important, TimeSpan.FromSeconds(8));
        return true;
    }

    private bool StartControlledPrank(PrankKind kind)
    {
        TryStartWindowPrank(manual: true, directKind: kind);
        return _prankPhase != PrankPhase.None;
    }

    private bool StartControlledHat()
    {
        if (_prankArtwork is null || DisplayGeometry.FromWindow(_windowHandle) is null) return false;
        _behaviorHatOnly = true;
        _prankKind = PrankKind.Hat;
        _prankLarge = false;
        _prankTarget = null;
        _prankPresentationFinished = true;
        _prankSystemEngaged = false;
        _prankAwaitingSystem = false;
        _prankStartFoot = _prankApproachFoot = PrankFoot();
        _prankGeneration++;
        _prankCancellation = new CancellationTokenSource();
        BeginPrankPhase(PrankPhase.ThrowHat, "hat_throw");
        return true;
    }

    private bool StartControlledIconMeal()
    {
        var origin = PrankFoot();
        var icon = DesktopIconLocator.GetAllIcons()
            .Where(candidate => DisplayGeometry.FromPoint(candidate.Center, nearest: false) is not null &&
                DesktopIconLocator.FindListViewAt(candidate.Center) != IntPtr.Zero)
            .OrderBy(candidate => Math.Pow(candidate.Center.X - origin.X, 2) + Math.Pow(candidate.Center.Y - origin.Y, 2))
            .FirstOrDefault();
        if (icon is null) { RejectBehavior("No visible desktop icons; show the desktop and try again"); return false; }
        var bitmap = DesktopIconCopyCapture.Capture(icon);
        if (bitmap is null) { RejectBehavior("The icon is covered or unreadable; try a different spot"); return false; }
        var target = DisplayGeometry.ClampPointToNearestWorkArea(
            new DrawingPoint(icon.Center.X + icon.PixelSize.Width + 24, icon.Center.Y), 36);
        var meal = new DeletedItemInfo("behavior-icon-copy.snack", "Icon copy", DateTimeOffset.UtcNow, false,
            CachedIcon: bitmap, ScreenX: target.X, ScreenY: target.Y,
            IconPixelWidth: bitmap.PixelWidth, IconPixelHeight: bitmap.PixelHeight,
            QueueId: Interlocked.Increment(ref _nextMealQueueId), AnchorSource: "BehaviorControlIconCopy");
        _foodQueue.Enqueue(meal, CreateWaitingGhost);
        StartNextMeal();
        return _state == PetState.Running && _movementPurpose == MovementPurpose.Meal;
    }
}
