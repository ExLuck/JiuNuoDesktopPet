namespace SoftMochiPet.Core;

public enum BehaviorControlAction
{
    Stop,
    Stand,
    Walk,
    Run,
    Curious,
    Explore,
    Patrol,
    Hop,
    Climb,
    SlideDown,
    JumpDown,
    Roll,
    Sleep,
    Wake,
    FreeFall,
    Toss,
    Hungry,
    LickIcon,
    EatIcon,
    Satisfied,
    Kick,
    Punch,
    Charge,
    HatStore,
    Shatter,
    Tear,
    Tease,
    FillMischief,
    ReturnWindows,
    ThrowAndRecoverHat,
}

[Flags]
public enum BehaviorControlCharacters
{
    None = 0,
    Nuonuo = 1,
    FeibiJiubi = 2,
    Both = Nuonuo | FeibiJiubi,
}

[Flags]
public enum BehaviorControlRequirements
{
    None = 0,
    SessionAvailable = 1 << 0,
    NotDragging = 1 << 1,
    Awake = 1 << 2,
    GroundSupport = 1 << 3,
    OutsideEnclosure = 1 << 4,
    ReachableClimbWindow = 1 << 5,
    WindowPlatform = 1 << 6,
    FreeAirSpace = 1 << 7,
    Sleeping = 1 << 8,
    FoodEnabled = 1 << 9,
    DesktopIcon = 1 << 10,
    EligibleWindow = 1 << 11,
    EmptyHat = 1 << 12,
    HeldWindow = 1 << 13,
    MischiefReady = 1 << 14,
}

public enum BehaviorControlKind
{
    Behavior,
    StateChange,
    AnimationPreview,
}

public sealed record BehaviorControlItem(
    BehaviorControlAction Action,
    string Group,
    string Title,
    BehaviorControlCharacters Characters,
    BehaviorControlRequirements Requirements,
    BehaviorControlKind Kind,
    string Description)
{
    public bool Supports(PetCharacterProfile character)
    {
        ArgumentNullException.ThrowIfNull(character);
        var flag = character.SupportsFood ? BehaviorControlCharacters.Nuonuo
            : character.UsesMischief ? BehaviorControlCharacters.FeibiJiubi : BehaviorControlCharacters.None;
        return (Characters & flag) != 0;
    }
}

/// <summary>
/// Menu metadata for the behavior-control build. Conditions describe the real
/// action route; the dispatcher must recheck live targets before execution.
/// </summary>
public static class BehaviorControlCatalog
{
    private const BehaviorControlCharacters Both = BehaviorControlCharacters.Both;
    private const BehaviorControlCharacters Nuonuo = BehaviorControlCharacters.Nuonuo;
    private const BehaviorControlCharacters Feibi = BehaviorControlCharacters.FeibiJiubi;
    private const BehaviorControlKind Behavior = BehaviorControlKind.Behavior;
    private const BehaviorControlKind StateChange = BehaviorControlKind.StateChange;
    private const BehaviorControlRequirements Ready = BehaviorControlRequirements.SessionAvailable |
        BehaviorControlRequirements.NotDragging;
    private const BehaviorControlRequirements Awake = Ready | BehaviorControlRequirements.Awake;
    private const BehaviorControlRequirements Ground = Awake | BehaviorControlRequirements.GroundSupport;
    private const BehaviorControlRequirements Travel = Ground | BehaviorControlRequirements.OutsideEnclosure;
    private const BehaviorControlRequirements WindowAction = Awake | BehaviorControlRequirements.OutsideEnclosure |
        BehaviorControlRequirements.EligibleWindow;
    private const BehaviorControlRequirements IconAction = Awake | BehaviorControlRequirements.OutsideEnclosure |
        BehaviorControlRequirements.FoodEnabled | BehaviorControlRequirements.DesktopIcon;

    public static IReadOnlyList<BehaviorControlItem> All { get; } = Array.AsReadOnly(new[]
    {
        new BehaviorControlItem(BehaviorControlAction.Stop, "Behavior Control", "Stop current action", Both,
            BehaviorControlRequirements.None, StateChange, "Stop the current behavior and wind down safely; a window already stored in the hat can still be given back."),
        new BehaviorControlItem(BehaviorControlAction.Stand, "Pose and mood", "Stand", Both,
            Ground, Behavior, "Return to a supported spot and stand idle."),
        new BehaviorControlItem(BehaviorControlAction.Curious, "Pose and mood", "Look around", Both,
            Ground, Behavior, "Look around curiously in place."),
        new BehaviorControlItem(BehaviorControlAction.Walk, "Walking and exploring", "Take a walk", Both,
            Travel, Behavior, "Walk along the current surface to a nearby spot."),
        new BehaviorControlItem(BehaviorControlAction.Run, "Walking and exploring", "Have a run", Both,
            Travel, Behavior, "Really run along the surface, not just play running frames in place."),
        new BehaviorControlItem(BehaviorControlAction.Explore, "Walking and exploring", "Explore", Both,
            Travel, Behavior, "Walk somewhere nearby to explore, then look around on arrival."),
        new BehaviorControlItem(BehaviorControlAction.Patrol, "Walking and exploring", "Roam around", Both,
            Travel, Behavior, "Keep roaming along the surface, deciding what to do at each edge based on the terrain."),
        new BehaviorControlItem(BehaviorControlAction.Hop, "Jumping and terrain", "Hop", Both,
            Travel | BehaviorControlRequirements.FreeAirSpace, Behavior, "Hop up from the surface and fall back under gravity."),
        new BehaviorControlItem(BehaviorControlAction.Climb, "Jumping and terrain", "Climb a window", Both,
            Travel | BehaviorControlRequirements.ReachableClimbWindow, Behavior, "Walk to a climbable real window edge and try to climb it; she may lose her grip."),
        new BehaviorControlItem(BehaviorControlAction.SlideDown, "Jumping and terrain", "Slide down a window", Both,
            Travel | BehaviorControlRequirements.WindowPlatform, Behavior, "Walk to the edge along a real window top, slide down the side and land."),
        new BehaviorControlItem(BehaviorControlAction.JumpDown, "Jumping and terrain", "Jump off a window", Both,
            Travel | BehaviorControlRequirements.WindowPlatform | BehaviorControlRequirements.FreeAirSpace,
            Behavior, "Jump from the current window top edge and finish with the real landing rules."),
        new BehaviorControlItem(BehaviorControlAction.Roll, "Jumping and terrain", "Roll", Both,
            Ground, Behavior, "Roll along the surface; Feibi can try it manually too, with no hunger attached."),
        new BehaviorControlItem(BehaviorControlAction.FreeFall, "Jumping and terrain", "Free fall", Both,
            Awake | BehaviorControlRequirements.FreeAirSpace, Behavior, "Release her where there is room to fall and let gravity land her, without touching the system mouse."),
        new BehaviorControlItem(BehaviorControlAction.Toss, "Jumping and terrain", "Toss up", Both,
            Awake | BehaviorControlRequirements.FreeAirSpace, Behavior, "Give her an upward push to show the physics of being tossed and landing."),
        new BehaviorControlItem(BehaviorControlAction.Sleep, "Sleeping and waking", "Go to sleep", Both,
            Ready | BehaviorControlRequirements.GroundSupport, Behavior, "Fall asleep on the surface and play the falling-asleep and breathing loops."),
        new BehaviorControlItem(BehaviorControlAction.Wake, "Sleeping and waking", "Wake up", Both,
            Ready | BehaviorControlRequirements.Sleeping | BehaviorControlRequirements.GroundSupport,
            Behavior, "Wake from sleep and return to the usual standing pose."),
        new BehaviorControlItem(BehaviorControlAction.Hungry, "Nuonuo interactions", "So hungry", Nuonuo,
            Ground | BehaviorControlRequirements.FoodEnabled, StateChange, "Enter the hungry state and say so; unavailable in fasting mode."),
        new BehaviorControlItem(BehaviorControlAction.LickIcon, "Nuonuo interactions", "Lick an icon", Nuonuo,
            IconAction, Behavior, "Run to a visible desktop icon and lick it, without changing the icon or the file."),
        new BehaviorControlItem(BehaviorControlAction.EatIcon, "Nuonuo interactions", "Slurp an icon copy", Nuonuo,
            IconAction, Behavior, "Use a copy of the real icon appearance, run over, slurp it up and finish satisfied; the real file is not deleted."),
        new BehaviorControlItem(BehaviorControlAction.Satisfied, "Animation showcase", "Satisfied (Animation showcase)", Nuonuo,
            Ground, BehaviorControlKind.AnimationPreview, "Watch the after-eating satisfied animation on its own, without pretending a file was eaten or adding to the feeding record."),
        new BehaviorControlItem(BehaviorControlAction.Kick, "Feibi mischief", "Kick a window away", Feibi,
            WindowAction, Behavior, "Kick a real window that meets the safety conditions; still works when the hat already holds something."),
        new BehaviorControlItem(BehaviorControlAction.Punch, "Feibi mischief", "Punch a window away", Feibi,
            WindowAction, Behavior, "Punch a real window that meets the safety conditions; still works when the hat already holds something."),
        new BehaviorControlItem(BehaviorControlAction.Charge, "Feibi mischief", "Charge a window", Feibi,
            WindowAction, Behavior, "Charge a real window that meets the safety conditions and let it slide to a stop on impact."),
        new BehaviorControlItem(BehaviorControlAction.HatStore, "Feibi mischief", "Hide a window in the hat", Feibi,
            WindowAction | BehaviorControlRequirements.EmptyHat, Behavior, "Store the window snapshot in the hat and minimize the real window until you give it back."),
        new BehaviorControlItem(BehaviorControlAction.Shatter, "Feibi mischief", "Smash a window", Feibi,
            WindowAction | BehaviorControlRequirements.EmptyHat, Behavior, "Smash the window snapshot and store the pieces in the hat; no app or file is harmed."),
        new BehaviorControlItem(BehaviorControlAction.Tear, "Feibi mischief", "Tear a window apart", Feibi,
            WindowAction | BehaviorControlRequirements.EmptyHat, Behavior, "Grow larger, tear the window snapshot apart and store it in the hat; the original window can be returned intact."),
        new BehaviorControlItem(BehaviorControlAction.Tease, "Feibi interactions", "Tease her", Feibi,
            Awake | BehaviorControlRequirements.MischiefReady, StateChange, "Raise the mischief meter under the existing teasing rules; repeated teasing and the safety cooldown still apply."),
        new BehaviorControlItem(BehaviorControlAction.FillMischief, "Feibi interactions", "Fill the mischief meter", Feibi,
            Awake, StateChange, "Set the mischief meter to full; this does not bypass the real-window safety conditions or auto-return what the hat holds."),
        new BehaviorControlItem(BehaviorControlAction.ReturnWindows, "Hat interactions", "Give the window back", Feibi,
            Ready | BehaviorControlRequirements.HeldWindow, Behavior, "Play the retrieve-and-reassemble sequence, then confirm the original window really came back."),
        new BehaviorControlItem(BehaviorControlAction.ThrowAndRecoverHat, "Hat interactions", "Toss the hat and fetch it", Feibi,
            Travel | BehaviorControlRequirements.EmptyHat | BehaviorControlRequirements.FreeAirSpace,
            Behavior, "Toss the hat, run over, pick it up and put it back on, without playing the hatless in-between frames on their own."),
    });

    private static readonly IReadOnlyList<BehaviorControlItem> NuonuoItems =
        Array.AsReadOnly(All.Where(item => item.Supports(PetCharacterProfile.Nuonuo)).ToArray());
    private static readonly IReadOnlyList<BehaviorControlItem> FeibiItems =
        Array.AsReadOnly(All.Where(item => item.Supports(PetCharacterProfile.FeibiJiubi)).ToArray());

    public static IReadOnlyList<BehaviorControlItem> ForCharacter(PetCharacterProfile character)
    {
        ArgumentNullException.ThrowIfNull(character);
        return character.UsesMischief ? FeibiItems : NuonuoItems;
    }

    public static BehaviorControlItem Get(BehaviorControlAction action) =>
        All.FirstOrDefault(item => item.Action == action) ?? throw new ArgumentOutOfRangeException(nameof(action), action, null);
}
