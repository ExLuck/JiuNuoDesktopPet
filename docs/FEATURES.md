# Feature guide

This page describes what JiuNuo Desktop Pet can currently do, the boundaries of its behaviour, and the parts that still need verifying on real hardware. The repository front page keeps only quick-start information; the actual implementation status is defined by this page and the source. Character configuration, asset provenance and the progress of the current character phase are all maintained together in [the character guide](CHARACTERS.md).

## Status markers

| Marker | Meaning |
| --- | --- |
| Stable | Has automated logic tests and has been verified in an ordinary desktop environment |
| Experimental | Works, but is sensitive to monitor layout, window types or system behaviour |
| Awaiting testing | The implementation and the applicable offline checks are done, pending user acceptance on a Windows desktop |
| Awaiting verification | Implementation or asset production has started but this phase's acceptance is not complete, so it cannot be treated as a stable capability |
| Planned | Not implemented, and should not be treated as a current capability |

## Character selection

| Capability | Status | Notes |
| --- | --- | --- |
| Switching Nuonuo / Feibi Jiubi | Awaiting verification | Switches the current character in single mode; in coexistence mode, choosing a character ends the companion window first and then switches. Assets and acceptance progress are in the character guide |
| Both pets together with synced settings | Awaiting testing | Shows both characters in one process, sharing a single tray icon plus size, mute, quiet and infinite modes; each keeps its own state and exclusive behaviour, see [both pets together and synced settings](CHARACTERS.md#both-pets-together-and-synced-settings-2026-09-23-adopted-awaiting-on-device-acceptance) |
| Two-pet relay for deleted icons | Awaiting testing | When both pets are available and the desktop icon position is unambiguous, Feibi kicks the existing fake icon and Nuonuo catches it in her mouth; where the position or state is unsuitable it falls back to the solo slurp. Real file operations remain Windows' responsibility, and the timing and boundaries are in [two-pet interactions](INTERACTION_PLAN.md#two-pet-relay-for-deleted-icons) |
| Separate progression saves | Awaiting verification | Nuonuo's old save is unchanged; Feibi separately saves sleepiness, curiosity and mischief state, and adds no feeding record. Size, mute and quiet mode are shared |
| Feibi Jiubi's temperament | Awaiting testing | A livelier tendency to move, explore and jump, matched with her own recordings; sleepiness, terrain limits and the life modes still take priority |
| Feibi Jiubi's window mischief | Awaiting verification | Kicking, punching and charging windows, storing and returning them in the hat, and throwing the hat to smash or tear once the bar is full. The code is wired up, but the complete new assets and the real desktop effect are not yet accepted |
| Feibi Jiubi's character isolation | Awaiting verification | Does not eat or lick icons, does not start the food consumption watcher, and has no hunger bar or fasting menu item; the bar above her head becomes the mischief bar |
| Feibi Jiubi's voice | Wired up, awaiting manual testing | Uses the user's own recordings, with everyday rotation and emotional and mischief responses; the mapping and acceptance status are in [Feibi's own voice](CHARACTERS.md#feibis-own-voice) |

Nuonuo is the default until a character has been chosen, and the last choice is saved after a successful switch. A switch requested while dragging, or during the current swallow or satisfied animation, is deferred; clicking the currently ticked character cancels a pending switch request. Where assets are incomplete, the current character and menu tick are kept and the reason for refusal is logged; the menu tick only ever indicates the character actually in effect. The switching boundaries and the acceptance checklist are in [the character guide](CHARACTERS.md) and [manual testing](../SELF_TEST.md#04-character-switching-and-isolation).

Speech bubbles above the head have their own toggle in the right-click menu, and are off by default for existing settings. Once on, they show short status lines, and with both pets present they can take turns automatically or manually. The bubbles never show test prompts, and they do not replace the voice, Nuonuo's hunger bar or Feibi's mischief bar. Timing and boundaries are in [two-pet interactions](INTERACTION_PLAN.md#speech-bubbles). Status: awaiting testing.

Switching to Feibi clears Nuonuo's pending food queue and fake icons rather than handing the feeding task over; switching away from Feibi first ends window mischief and tries to return any held window. Older image generation results are kept for traceability and do not mean the current character still supports food behaviour.

## Feibi Jiubi's window mischief

Feibi's own menu offers "Allow autonomous mischief", "Play a prank", "Tease her" and "Give the window back". The autonomy toggle is on by default. Manual ordinary actions rotate through kick, punch, charge and hat-hiding, and once the bar is full you can choose the hatless burst kick or the tear. The bar value, cooldowns, action catalogue and phase status are all in [the character guide](CHARACTERS.md), and the rationale for the effects and implementation is in [the window mischief design](FEIBI_WINDOW_PLAY_PLAN.md).

Ordinary kicks and punches move the real window, while the hat and the finishers only deform an in-memory snapshot of the real window, leaving the application minimized with its taskbar entry intact; no application is closed and no document is modified. After smashing or tearing, Feibi picks her hat up, puts the shards or the two halves inside, then wears it again and returns to her day. Once that is done there are no shards left on the desktop, and there is no timed return and no automatic window restore. Only choosing "Give the window back" from the right-click menu takes it out of the hat, reassembles it and restores it; clicking the hat or the shards does not. While something is held, no second window mischief begins. A manual capture may target any foreground window that is on the same screen, unobscured, and neither maximized nor fullscreen; autonomous mode still prefers ordinary restorable windows. Known fullscreen capture layers are ignored by identity rather than treated as solid windows. Permissions and content protection are unchanged. Everyday dragging, going home, size changes, quiet mode and turning autonomy off do not return anything automatically; switching characters, quitting, locking, a real display change and error paths all keep the safe return. It releases immediately when the user restores or takes over the target from the taskbar, and the complete boundaries are in [window holding rules](CHARACTERS.md#window-results-are-restored-by-the-user-adopted).

Screenshots are never written to disk or uploaded, and no window title or body text is written to the mischief log. If something ends abnormally, the user can still find the window through the taskbar; no automatic recovery after a crash is promised.

The standalone feature test uses `Revision 6`. The order of the eight items is unchanged, a complete window hat-storing is demonstrated only once, and the two finishers add a check that the shards go into the hat after the destruction. The four storing and destruction steps wait for the user to right-click and restore once the performance finishes; that wait has no timeout and the test never returns the window itself. Only once the request arrives does it verify the real restore result and continue. Returning early without completing the storing, or without observing the hatless sequence, is marked `NeedsReview`. `Tease` does not depend on a window precheck, while window interactions still check the actual target and the safety conditions. Progress uses only the right-click menu, the test window title and the report; a genuine failure can be retried or stopped. Starting and acceptance are in [manual testing](../SELF_TEST.md#06-feibis-standalone-feature-demonstration), and the status is in [character progress](CHARACTERS.md#current-progress-and-phase-exit-conditions).

## Interaction and physics

| Capability | Status | Notes |
| --- | --- | --- |
| Free dragging | Stable | The left button can interrupt idling and autonomous roaming; pressing repeatedly does not re-enter the drag state |
| Cheek pinching for both characters | Awaiting testing | Hold and pull either character's cheek outward and it stretches with your hand and keeps nibbling; release and it springs back. Outside the face it is still the original drag. Animation and assets are in [the cheek pinch guide](CHEEK_PINCH.md) |
| Gesture release | Stable | Samples mouse velocity for about 140 ms before release, distinguishing a gentle placement, a horizontal fling, an upward toss and a diagonal fling |
| Gravity and landing | Stable | Keeps two-dimensional inertia and air resistance after release, bouncing softly at the monitor boundary and landing on usable terrain |
| Window platforms | Experimental | Treats the top edge of an ordinary visible application window as a standing platform, and falls again once the window closes or moves away |
| Moving platform inertia | Experimental | Moving a window up quickly tosses Nuonuo, and the ledge catching up with her soles lifts her again; dragging downward produces a brief inertial separation and a landing back on the live ledge, instead of sticking to her soles |
| Window side collisions | Experimental | Continuously tracks the solid edges of ordinary visible windows; when a left or right edge sweeps through the character's body, an impulse is applied based on the impact direction and the window's speed |
| Window enclosures | Experimental | Dragging her inside a window and releasing switches to a container closed on four sides; she keeps her inertia, is affected by gravity, and bounces off the walls relative to their movement. Icon licking is paused inside a container, and a deletion feeding request makes her run out on her own |
| Ledge behaviour | Experimental | Supports climbing, jumping, sliding down, deliberately jumping to the next level, and being flung outward on maximize |
| Multi-monitor terrain | Experimental | Computes physical-pixel terrain for each monitor separately, supporting negative coordinates and windows segmented across monitors |

The non-character area of the window passes the mouse through using Win32 hit testing, while the character's body can still be dragged.

Mouse interaction only responds to a deliberate press, drag, release or right-click menu; mouse movement by itself never triggers chasing, dodging, following or hugging. Autonomous exploration is driven by the life state and the window terrain, so the character does not follow the pointer and get in the way.

## Autonomous behaviour

The character switches between idling, roaming, climbing, falling, landing, sleeping and character-specific interactions through a state machine. Nuonuo combines four long-term values; Feibi uses only sleepiness, curiosity and her own mischief value, and is driven by neither hunger nor fullness:

- **Hunger**: the higher it is, the more likely she asks for food or acts hungry.
- **Fullness**: rises after eating and changes slowly over time.
- **Sleepiness**: grows while awake and recovers during sleep.
- **Curiosity**: changed by exploration and interaction with the environment.

Life state is written atomically per character, and Nuonuo continues to use her original save. The table of official paths is in [characters and data boundaries](CHARACTERS.md#characters-and-data-boundaries). On the next load it continues evolving according to a bounded offline duration, and an interrupted write never corrupts the last valid data.

Current autonomous behaviour includes:

- standing idle uses its own blink loop, and each autonomous decision also has a small chance of switching to a short nap, which becomes more likely the sleepier she is;
- only while Feibi is standing idle, she tries to play an everyday recording every 20 to 50 seconds at random, subject to mute, quiet mode and the voice cooldown; no character name text is shown;
- the next autonomous decision begins according to each character's idle ceiling, with the specific rhythm in [temperament and voice](CHARACTERS.md#temperament-and-voice);
- turning back at a window edge, climbing, sliding down, or jumping to neighbouring terrain;
- sleeping and exploring as needed; only Nuonuo asks for food, and Feibi can interact with windows once the user enables autonomous mischief;
- important events use the current character's local voice, with priority, per-scene cooldowns and per-recording repeat protection. An obvious landing gets its own response while a slight drop stays silent, and whether text appears above the head is controlled by its own toggle.

The tray menu offers two life modes that persist:

- **Fasting mode, Nuonuo only**: hides the hunger label and bar, pins hunger at 0, and rules out begging, icon licking and the hungry roll; ordinary walking, sleeping and deletion feeding remain. This item is not shown in Feibi's menu.
- **Quiet mode, shared by both characters**: only autonomous idling and sleeping, with no roaming, climbing, icon licking, response to deletions or mischief; an already stored window is kept until a right-click restores it, and no mischief bar is accumulated. User dragging and direct window collisions remain.

## Recycle bin feeding

The feeding features below apply only to Nuonuo, and work around Windows file events and Explorer metadata:

1. before the deletion, try to record the display name and physical coordinates of the selected desktop item;
2. once the file reaches the recycle bin, match the original name from the recycle bin metadata;
3. add the target to the pending queue;
4. Nuonuo moves to the target and completes the feeding interaction;
5. consecutive deletions are handled one at a time, with at most 24 items kept in the queue.

A feeding task can be interrupted by the user dragging her. An interrupted current target is put back at the head of the queue, and Nuonuo continues once she lands, so a task is never silently lost.

### Safety boundaries

- The content of a deleted file is never read.
- No deletion or restore is performed on the user's behalf.
- The contents of the recycle bin are never changed.
- When capture or matching fails, only the interaction is skipped and the original system operation is unaffected.
- By default it watches the desktop, the downloads directory and the recycle bin information visible to the current user, and extra directories can be added in the settings.

## Display and system integration

- A transparent, borderless, always-on-top WPF window.
- The tray menu offers "Bring her home", "Pet character", a live 60%-200% size slider, start with Windows, fasting mode, quiet mode, "Show speech bubbles", "Mute" and "Exit"; with both pets present a two-pet conversation can also be triggered manually. The character name is used consistently in the tray tooltip and the size slider label.
- Start with Windows registers for the current Windows user, and a portable package prefers the root launcher; unticking it deletes the corresponding startup entry.
- A Per-Monitor V2 DPI manifest.
- Watches for monitor hot-plug, resolution, DPI and taskbar work area changes.
- Shrinks automatically in a very small work area and returns to the configured size back on an ordinary monitor.
- Structured logging of state transitions, monitor geometry, window support and file events.
- The log rotates at 8 MB and is kept only on the local machine.

## Current limitations

- Some UWP apps, games, hardware-accelerated overlay layers and custom windows do not provide usable standard window bounds.
- Restarting Explorer, or a third-party desktop organizing tool, can invalidate the desktop position snapshot.
- Moving across monitors with different scale factors involves WPF to Win32 coordinate conversion and still needs verifying on more hardware combinations.
- The application has no digital signature, no automatic updates and no crash telemetry.
- There is currently no separate settings window; the common options are collected in the tray menu.

## Planned directions

The following are candidate directions only, with no commitment on timing:

- GitHub Releases with checksums.
- Automated Windows builds and tests.
- A more complete settings interface.
- A more complete mixed-DPI and multi-taskbar regression matrix.
- Optional behaviour intensity and movement speed settings.
