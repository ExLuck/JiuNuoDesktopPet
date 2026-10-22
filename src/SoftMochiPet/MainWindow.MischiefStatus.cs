using Brush = System.Windows.Media.Brush;
using SoftMochiPet.Core;
using SoftMochiPet.Services;

namespace SoftMochiPet;

public partial class MainWindow
{
    private Brush? _mischiefActiveBrush;
    private Brush MischiefActiveBrush => _mischiefActiveBrush ??= MischiefBarFill.Background;
    private bool _mischiefWaitingForTarget;
    private string? _lastMischiefStatusReason;
    private double _nextMischiefStatusLog;

    private void UpdateMischiefStatus()
    {
        var now = _clock.Elapsed.TotalSeconds;
        var reason = !_settings.AllowMischief ? "disabled"
            : _sessionLocked ? "session_locked"
            : _settings.QuietMode ? "quiet"
            : _state is PetState.Sleeping or PetState.Waking ? "sleeping"
            : _prankPhase != PrankPhase.None ? "performing"
            : _mischief.CooldownRemainingSeconds > 0 ? "cooldown"
            : _isDragging ? "dragging"
            : _animator.Hatless ? "hat_recovery"
            : _prankArtwork is null || _windowPranks is null ? "assets_unavailable"
            : HasHeldPrank && _mischief.Value >= 100 ? "hat_occupied_full"
            : _mischiefWaitingForTarget ? "no_eligible_window"
            : _enclosureHandle != IntPtr.Zero ? "enclosed"
            : _mischief.AutonomousActionDue && _state is not (PetState.Idle or PetState.Curious or PetState.Running)
                ? "pet_busy" : "charging";
        var cooldown = (int)Math.Ceiling(_mischief.CooldownRemainingSeconds);
        var attempt = _mischief.SecondsUntilNextAttempt is { } seconds
            ? Math.Max(seconds, _nextPrankSearch - now) : (double?)null;
        var countdown = (int)Math.Ceiling(Math.Max(0, attempt ?? 0));
        var text = reason switch
        {
            "disabled" => "Autonomous mischief is off",
            "session_locked" => "Paused (screen locked)",
            "quiet" => "Paused (quiet mode)",
            "sleeping" => "Paused (asleep)",
            "performing" => _manualRestoreInProgress ? "Returning window" : "Pranking",
            "cooldown" => $"Cooling down · {cooldown}s",
            "dragging" => "Building up · let go of her",
            "hat_recovery" => "Waiting for her hat",
            "assets_unavailable" => "Animation assets unavailable (see log)",
            "hat_occupied_full" => "Full · waiting for the hat; small pranks continue",
            "no_eligible_window" => $"Waiting for a suitable window · retry in {countdown}s",
            "enclosed" => "Building up · waiting for her to leave the window",
            "pet_busy" => "Waiting for the current action to finish",
            _ => $"Building up · {Math.Floor(_mischief.Value):0}/100" + (HasHeldPrank ? " · hat occupied" : ""),
        };
        _trayIcon.SetMischiefStatus(text);
        if (reason == _lastMischiefStatusReason && now < _nextMischiefStatusLog) return;
        _lastMischiefStatusReason = reason;
        _nextMischiefStatusLog = now + 30;
        DiagnosticsLog.WriteEvent("MischiefStatus", ("Reason", reason),
            ("Value", Math.Round(_mischief.Value, 2)), ("CooldownSeconds", cooldown),
            ("NextAttemptSeconds", attempt is { } remaining ? Math.Round(Math.Max(0, remaining), 2) : null),
            ("AutonomyEnabled", _settings.AllowMischief), ("HatOccupied", HasHeldPrank), ("State", _state));
    }
}
