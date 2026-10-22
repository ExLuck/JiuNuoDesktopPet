namespace SoftMochiPet.Core;

internal static class FeibiJiubiDialogue
{
    private static readonly IReadOnlyDictionary<DialogueCue, IReadOnlyList<string>> Lines =
        new Dictionary<DialogueCue, IReadOnlyList<string>>
        {
            [DialogueCue.Startup] = ["Chirp, you are here!", "Hi, play with me!", "Where are we going today?", "Hehe, we meet again."],
            [DialogueCue.IconsHidden] = ["Huh, everyone hid?", "Are we playing hide and seek?", "Come out and play!", "I definitely saw you."],
            [DialogueCue.PlatformLaunch] = ["Woohoo--!", "Hat, hold on tight!", "Wow, light little feet!", "Can we go higher?"],
            [DialogueCue.HardLanding] = ["Oops, a bounce!", "My shoes got there first!", "Hey, I can still bounce!", "Chirp, not squashed!"],
            [DialogueCue.ConfidentClimb] = ["Hey, watch me climb!", "What is up there?", "My little feet are strong!", "Go go go!"],
            [DialogueCue.CarefulClimb] = ["Shoes, do not slip.", "Just a little higher!", "Can I hold on?", "Hat, do not cover my eyes."],
            [DialogueCue.ClimbSlip] = ["Oh no, slippery hands!", "So close!", "The wall is so slippery.", "Not giving up, climbing again!"],
            [DialogueCue.ClimbSuccess] = ["Look, I am up here!", "Hehe, high enough!", "The wind is blowing my hat!", "Everything looks tiny below!"],
            [DialogueCue.Exploration] = ["It is all shiny over there!", "Hey, let us go look!", "Where have we not played yet?", "Follow my little feet!"],
            [DialogueCue.Sleep] = ["My eyelids are so heavy.", "Play... a little longer.", "Chirp, let me lean on you.", "Are you in my dream too?"],
            [DialogueCue.WakeNormally] = ["Hey, we meet again!", "I ran so far in my dream.", "Slept my hair into a tuft!", "Chirp, I want to go out again!"],
            [DialogueCue.PickedUp] = ["Lift me up high!", "Chirp, where are you taking me?", "My little feet cannot reach.", "Swinging is pretty fun!"],
            [DialogueCue.Released] = ["Whoa, here comes the floor!", "Little feet, find the floor!", "Wait for my hat!", "Chirp, gently!"],
            [DialogueCue.DragThrow] = ["Whoosh-- so fast!", "My hat cannot keep up!", "Whoa, the wind is whooshing!", "Fly a little longer!"],
            [DialogueCue.TerrainHop] = ["Bounce a bit higher!", "Hey, springy little feet!", "Chirp, I can reach!", "Over in one go!"],
            [DialogueCue.TerrainJumpDown] = ["I want to wander down there too!", "Hey, catch my hat!", "Bounce on down!", "Chirp, that looks fun!"],
            [DialogueCue.EdgeSlide] = ["Wheee, so slippery!", "Little hands, hold tight!", "My shoes are tickling.", "One more little slide!"],
            [DialogueCue.WindowMaximized] = ["Whoa, it is squishing me!", "The window grew so fast!", "My hat got knocked crooked!", "Why am I suddenly flying!"],
            [DialogueCue.Home] = ["My nest is so cosy!", "Hey, this place is the best!", "Came back to see you!", "My hat is resting too."],
            [DialogueCue.VoiceEnabled] = ["Chirp, over here!", "Hey, look at me!", "Hello, buddy!", "Did you miss me?"],
            [DialogueCue.VoiceDisabled] = ["Shh, quietly.", "Tiptoeing along.", "Chirp... very softly.", "Secretly keeping you company."],
        };

    private static readonly IReadOnlyList<string> PlayLines = ["Hey, what are we playing?", "Bounce with me!", "I have a little idea!", "My hat is well hidden."];

    public static IReadOnlyList<string> GetLines(DialogueCue cue) =>
        Lines.TryGetValue(cue, out var lines) ? lines : PlayLines;
}
