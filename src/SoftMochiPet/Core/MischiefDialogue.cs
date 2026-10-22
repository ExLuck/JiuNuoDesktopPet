namespace SoftMochiPet.Core;

public enum MischiefCue
{
    Teased,
    Sneak,
    Kick,
    Punch,
    Charge,
    Recoil,
    HideWindow,
    HoldingWindow,
    ReturnWindow,
    Burst,
    Smash,
    Tear,
    Finished,
    Interrupted,
    NoTarget,
}

public sealed class MischiefDialogueSelector
{
    private static readonly IReadOnlyDictionary<MischiefCue, IReadOnlyList<string>> Lines =
        new Dictionary<MischiefCue, IReadOnlyList<string>>
        {
            [MischiefCue.Teased] = ["Hehe, again!", "Tickles!", "Teasing me again!", "Chirp! Got you!"],
            [MischiefCue.Sneak] = ["Shhh--", "You did not see me.", "Coming quietly.", "Hehe, just wait!"],
            [MischiefCue.Kick] = ["Watch the feet!", "Hiyah!", "Off you go!", "Just one little kick!"],
            [MischiefCue.Punch] = ["Thud!", "Little punch!", "Hey!", "Take this!"],
            [MischiefCue.Charge] = ["Move aside!", "Charge--!", "Here I come!", "Hold on, hat!"],
            [MischiefCue.Recoil] = ["Ouchie.", "Landed on my bottom!", "Pretending I did not see.", "I am steady now!"],
            [MischiefCue.HideWindow] = ["Let me hide it a moment!", "My hat can fit it!", "Hehe, it is mine now.", "No peeking!"],
            [MischiefCue.HoldingWindow] = ["I did not hide anything.", "Behave, hat!", "Hm? What window?", "Hehe..."],
            [MischiefCue.ReturnWindow] = ["Here it is back!", "Ta-daa--!", "Right here!", "Okay, no more hiding."],
            [MischiefCue.Burst] = ["I cannot hold back!", "Hat, sit this one out!", "I am going for real now!", "Hehe, watch closely!"],
            [MischiefCue.Smash] = ["And away--!", "Chirp! Big stomp!", "Whoa, it scattered!", "I am so good at this!"],
            [MischiefCue.Tear] = ["Hey-- heave!", "Why is it so tough!", "Pull a little more!", "Oops, sticky hands!"],
            [MischiefCue.Finished] = ["So fun!", "My hat is back!", "Hehe, all good.", "I have been good!"],
            [MischiefCue.Interrupted] = ["Caught me!", "Fine, fine.", "I am letting go.", "We will play again next time."],
            [MischiefCue.NoTarget] = ["Where did the toy hide?", "Play again in a bit!", "A couple of bounces first!", "Then I will rest a bit."],
        };

    private readonly Random _random;
    private readonly Dictionary<MischiefCue, int> _previous = [];

    public MischiefDialogueSelector(Random? random = null) => _random = random ?? Random.Shared;

    public static IReadOnlyList<string> GetLines(MischiefCue cue) => Lines[cue];

    public string Choose(MischiefCue cue)
    {
        var lines = GetLines(cue);
        var index = _random.Next(lines.Count);
        if (_previous.TryGetValue(cue, out var previous) && index == previous)
        {
            index = (index + 1) % lines.Count;
        }
        _previous[cue] = index;
        return lines[index];
    }
}
