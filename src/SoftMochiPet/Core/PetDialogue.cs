namespace SoftMochiPet.Core;

public enum DialogueCue
{
    Startup,
    Buffet,
    MealSpotted,
    MealQueueContinues,
    IconsHidden,
    ManualIconLick,
    CuriousIconLick,
    PlatformLaunch,
    HardLanding,
    VeryHungry,
    ABitHungry,
    ConfidentClimb,
    CarefulClimb,
    ClimbSlip,
    ClimbSuccess,
    HungryRoll,
    Suction,
    Exploration,
    Sleep,
    WakeForFood,
    WakeNormally,
    ComfortablyFull,
    IconTaste,
    FeastContinues,
    AnotherBite,
    Stuffed,
    TastedItem,
    PickedUp,
    Released,
    DragThrow,
    TerrainHop,
    TerrainJumpDown,
    EdgeSlide,
    WindowMaximized,
    HungryRequestQueued,
    Home,
    DeleteReactionEnabled,
    DeleteReactionDisabled,
    VoiceEnabled,
    VoiceDisabled,
}

/// <summary>
/// Short reactions selected from each character's own dialogue catalog.
/// </summary>
public static class PetDialogueCatalog
{
    private static readonly IReadOnlyDictionary<DialogueCue, IReadOnlyList<string>> Lines =
        new Dictionary<DialogueCue, IReadOnlyList<string>>
        {
            [DialogueCue.Startup] =
            [
                "Morning.",
                "I am awake.",
                "What are we eating today?",
                "Just gonna space out a bit.",
            ],
            [DialogueCue.Buffet] =
            [
                "So many!",
                "All mine?",
                "I cannot finish it all.",
                "Hehe, dinner time.",
            ],
            [DialogueCue.MealSpotted] =
            [
                "There is food.",
                "Smells nice.",
                "Here I come.",
                "Can I eat that?",
            ],
            [DialogueCue.MealQueueContinues] =
            [
                "There is more.",
                "Next one.",
                "I want them all.",
                "Do not run.",
            ],
            [DialogueCue.IconsHidden] =
            [
                "No icons...",
                "Where did they hide?",
                "Cannot reach to lick.",
                "All empty.",
            ],
            [DialogueCue.ManualIconLick] =
            [
                "So hungry...",
                "One lick first.",
                "Just a tiny bite.",
                "Is this edible?",
            ],
            [DialogueCue.CuriousIconLick] =
            [
                "What flavour is it?",
                "A little lick.",
                "Looks tasty.",
                "Just a taste.",
            ],
            [DialogueCue.PlatformLaunch] =
            [
                "I am flying!",
                "Wooah!",
                "Where are my feet?",
                "So high!",
            ],
            [DialogueCue.HardLanding] =
            [
                "Squish.",
                "I went flat.",
                "Phew, made it.",
                "I am round again.",
            ],
            [DialogueCue.VeryHungry] =
            [
                "So hungry...",
                "Flat with hunger.",
                "I want a snack.",
                "My tummy is empty.",
            ],
            [DialogueCue.ABitHungry] =
            [
                "A little hungry.",
                "I want one bite.",
                "Any food?",
                "My mouth is watering.",
            ],
            [DialogueCue.ConfidentClimb] =
            [
                "I can get up there.",
                "Watch me.",
                "Heave ho.",
                "Is there food up there?",
            ],
            [DialogueCue.CarefulClimb] =
            [
                "So high...",
                "Slow down.",
                "Do not fall.",
                "Let me try.",
            ],
            [DialogueCue.ClimbSlip] =
            [
                "Sliding back down...",
                "Lost my grip.",
                "Aww... so close.",
                "Again.",
            ],
            [DialogueCue.ClimbSuccess] =
            [
                "I made it up.",
                "Hehe.",
                "Pretty good, right?",
                "It is high up here.",
            ],
            [DialogueCue.HungryRoll] =
            [
                "So so hungry.",
                "Give me food.",
                "I will roll.",
                "Flattened by hunger.",
            ],
            [DialogueCue.Suction] =
            [
                "Slurp it over!",
                "Aaah--",
                "Into my mouth.",
                "I am going to eat.",
            ],
            [DialogueCue.Exploration] =
            [
                "Where is this?",
                "Never been here.",
                "Let me look over here.",
                "Curious.",
            ],
            [DialogueCue.Sleep] =
            [
                "I am going to sleep.",
                "Sleepy.",
                "Just a little nap.",
                "Good night.",
            ],
            [DialogueCue.WakeForFood] =
            [
                "Food?",
                "I am awake!",
                "Smells so nice!",
                "Is it dinner time?",
            ],
            [DialogueCue.WakeNormally] =
            [
                "Awake now.",
                "Still sleepy...",
                "Slept enough.",
                "Lie down a bit more?",
            ],
            [DialogueCue.ComfortablyFull] =
            [
                "All full.",
                "Round little tummy.",
                "So stuffed.",
                "Not moving.",
            ],
            [DialogueCue.IconTaste] =
            [
                "Not tasty.",
                "Tastes like icon only.",
                "Empty.",
                "Another lick?",
            ],
            [DialogueCue.FeastContinues] =
            [
                "I can eat more!",
                "One more.",
                "Give them all to me.",
                "Not full yet.",
            ],
            [DialogueCue.AnotherBite] =
            [
                "Next one.",
                "Any more?",
                "Keep eating.",
                "I want more.",
            ],
            [DialogueCue.Stuffed] =
            [
                "Too stuffed.",
                "My tummy is so round.",
                "Really full now.",
                "Cannot move.",
            ],
            [DialogueCue.TastedItem] =
            [
                "{item} is tasty.",
                "{item} smells nice.",
                "I like {item}.",
                "This one is called {item}?",
            ],
            [DialogueCue.PickedUp] =
            [
                "You caught me.",
                "Gently.",
                "I am stretching.",
                "Ooow...",
            ],
            [DialogueCue.Released] =
            [
                "Let go.",
                "I am falling.",
                "Where are my feet?",
                "Spinning around...",
            ],
            [DialogueCue.DragThrow] =
            [
                "Flying away!",
                "Wooah--",
                "I cannot stop!",
                "I got flung!",
            ],
            [DialogueCue.TerrainHop] =
            [
                "A little jump.",
                "Heave ho!",
                "I can fly.",
                "Bouncing up.",
            ],
            [DialogueCue.TerrainJumpDown] =
            [
                "I am jumping.",
                "Catch me down there.",
                "Off I go!",
                "Flying down.",
            ],
            [DialogueCue.EdgeSlide] =
            [
                "Sliding down.",
                "Sliding slowly.",
                "Hold on tight.",
                "Is this a slide?",
            ],
            [DialogueCue.WindowMaximized] =
            [
                "I got bigger!",
                "I got bounced away!",
                "Wooah, so big!",
                "The window grew!",
            ],
            [DialogueCue.HungryRequestQueued] =
            [
                "So hungry... one moment.",
                "I will eat once I am steady.",
                "Going to lick it now.",
                "My tummy cannot wait.",
            ],
            [DialogueCue.Home] =
            [
                "Home at last.",
                "Back to my nest.",
                "This spot is good.",
                "Not leaving.",
            ],
            [DialogueCue.DeleteReactionEnabled] =
            [
                "Let me eat what you delete.",
                "I will catch it.",
                "Remember to call me.",
                "Food at last.",
            ],
            [DialogueCue.DeleteReactionDisabled] =
            [
                "Not eating today.",
                "Holding back for now.",
                "Not chasing any more.",
                "The icons are safe.",
            ],
            [DialogueCue.VoiceEnabled] =
            [
                "I have a voice now.",
                "Can you hear me?",
                "Hehe, it is me.",
                "I am talking.",
            ],
            [DialogueCue.VoiceDisabled] =
            [
                "I will be quiet.",
                "Shh.",
                "No more talking.",
                "A bit softer.",
            ],
        };

    public static IReadOnlyList<string> GetLines(DialogueCue cue, PetCharacterProfile? profile = null) =>
        profile?.Id == PetCharacterProfile.FeibiJiubi.Id ? FeibiJiubiDialogue.GetLines(cue) : Lines[cue];
}

public sealed class PetDialogueSelector
{
    private readonly Random _random;
    private readonly PetCharacterProfile _profile;
    private readonly Dictionary<DialogueCue, int> _lastLineByCue = [];

    public PetDialogueSelector(Random? random = null, PetCharacterProfile? profile = null)
    {
        _random = random ?? Random.Shared;
        _profile = profile ?? PetCharacterProfile.Nuonuo;
    }

    public string Choose(DialogueCue cue, string? itemName = null)
    {
        var lines = PetDialogueCatalog.GetLines(cue, _profile);
        var index = _random.Next(lines.Count);
        if (lines.Count > 1 && _lastLineByCue.TryGetValue(cue, out var previous) && index == previous)
        {
            index = (index + 1) % lines.Count;
        }

        _lastLineByCue[cue] = index;
        return lines[index].Replace("{item}", itemName ?? "this file", StringComparison.Ordinal);
    }
}
