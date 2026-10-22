namespace SoftMochiPet.Core;

public enum FeatureDemoAction
{
    Kick, Punch, Charge, HatStoreReturn, Tease,
    Shatter, Tear, HatlessInterruption,
}

public sealed record FeatureDemoStep(FeatureDemoAction Action, string Title, double TimeoutSeconds)
{
    public string Id => Action.ToString();
    public bool RequiresWindowTarget => Action != FeatureDemoAction.Tease;
    public bool RequiresManualRestore => Action is FeatureDemoAction.HatStoreReturn or FeatureDemoAction.Shatter
        or FeatureDemoAction.Tear or FeatureDemoAction.HatlessInterruption;
    public FeatureDemoAction CompletionAction => Action == FeatureDemoAction.HatlessInterruption
        ? FeatureDemoAction.Shatter : Action;
}

public sealed record FeatureDemoManualCheck(string Id, string Title);

public static class FeatureDemoPlan
{
    public const int Revision = 6;
    public const double RestoreTimeoutSeconds = 20;
    public static IReadOnlyList<FeatureDemoStep> Steps { get; } = Array.AsReadOnly(new[]
    {
        new FeatureDemoStep(FeatureDemoAction.Kick, "Kick the test window away", 20),
        new FeatureDemoStep(FeatureDemoAction.Punch, "Punch the test window away", 20),
        new FeatureDemoStep(FeatureDemoAction.Charge, "Charge into it", 20),
        new FeatureDemoStep(FeatureDemoAction.HatStoreReturn, "Window hidden; right-click to give it back", 40),
        new FeatureDemoStep(FeatureDemoAction.Tease, "Tease her and watch the mischief bar rise", 10),
        new FeatureDemoStep(FeatureDemoAction.Shatter, "Smashed and stored in the hat; right-click to restore", 60),
        new FeatureDemoStep(FeatureDemoAction.Tear, "Torn apart and stored in the hat; right-click to restore", 60),
        new FeatureDemoStep(FeatureDemoAction.HatlessInterruption, "Interrupted hatless and hat retrieved; right-click to restore", 60),
    });

    public static IReadOnlyList<FeatureDemoManualCheck> ManualChecks { get; } = Array.AsReadOnly(new[]
    {
        new FeatureDemoManualCheck("PrankAppearance", "Check the tear edges stay whole, the pieces go into the hat, and right-click reassembles them in place"),
        new FeatureDemoManualCheck("PrankTakeover", "After storing, no pieces remain on the desktop, the window stays minimized, right-click restores it; she stops when you take over"),
        new FeatureDemoManualCheck("PrankVoice", "Her own voice lines, mute stops instantly, and back-to-back actions do not overlap sounds"),
    });

    public static string FailureHint(string reason) => reason switch
    {
        "foreground_window" => "The test window is in the foreground: click an empty spot on the desktop, then right-click to retry this item",
        "occluded" or "window_occluded" => "The test window is covered: move what covers it, then right-click to retry this item",
        "elevated_process" => "Exit the pet running as administrator and start it normally",
        "test_window_closed" => "The test window is closed; right-clicking to retry this item will create it again",
        "step_timeout" => "This item did not finish in time; check the report and retry it",
        "unexpected_restore_before_manual_request" => "The window was released without a right-click request; check the report and retry",
        "manual_restore_identity_mismatch" => "The restore request does not match the current test window; check the report and retry",
        _ => $"This item is paused ({reason}); right-click to retry it",
    };

    public static string ManualRestoreHint(double elapsedSeconds)
    {
        var elapsed = double.IsFinite(elapsedSeconds) ? Math.Max(0, Math.Floor(elapsedSeconds)) : 0;
        return $"Right-click to restore (waited {elapsed:0}s)";
    }

    public static string ConfirmedStepResult(FeatureDemoAction action, bool hatlessInterruptionObserved,
        bool? remainsStoredBeforeReturn = null) =>
        (action == FeatureDemoAction.HatlessInterruption && !hatlessInterruptionObserved) ||
        (action is FeatureDemoAction.Shatter or FeatureDemoAction.Tear or FeatureDemoAction.HatlessInterruption &&
            remainsStoredBeforeReturn == false) ? "NeedsReview" : "Completed";
}
