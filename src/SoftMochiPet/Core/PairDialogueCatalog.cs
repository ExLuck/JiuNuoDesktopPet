namespace SoftMochiPet.Core;

public readonly record struct PairDialogue(bool FeibiFirst, string First, string Reply);

public static class PairDialogueCatalog
{
    public static IReadOnlyList<PairDialogue> Exchanges { get; } =
    [
        new(true, "Nuonuo, guess what is in my hat?", "Cake...?"),
        new(true, "Let us go look over there!", "Wait for me."),
        new(true, "Did I jump high?", "So high."),
        new(true, "There is a surprise in my hat!", "Is it edible?"),
        new(true, "Nuonuo, wake up.", "Just a little longer..."),
        new(false, "What are you giggling at now?", "Hehe, secret!"),
        new(false, "Let me lie in your hat.", "Sure, climb in!"),
        new(false, "Run a bit slower.", "Then hold my hand!"),
        new(false, "Is there cake today?", "Let me go look!"),
        new(false, "Stop making mischief.", "Just a tiny bit!"),
    ];
}
