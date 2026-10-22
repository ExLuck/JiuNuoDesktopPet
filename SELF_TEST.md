# JiuNuo Desktop Pet manual test checklist

The tests below use only temporary files you create yourself. Do not use an existing file for a deletion test, and do not empty your whole recycle bin.

The character phase's scope, assets and acceptance progress are defined by [the character guide](docs/CHARACTERS.md). The official portable package contains both characters, the complete animations and their own voices; after delivery you run the on-device checks below yourself, and the agent does not start the pet automatically. Use the most recently delivered `dist\JiuNuoDesktopPet-Portable`, not an archive or an old release directory under the old branding.

Feibi's animations and calibration assets are complete. The official package contains no standalone test tools, so check her from the ordinary right-click menu following sections 0.4-0.5. Nuonuo keeps the original feeding tests; Feibi should not eat, lick or show a hunger bar. Mischief testing uses only a plain empty test window you have prepared deliberately, never an application where you are editing something that matters.

The retest entry point for the official edition is `dist\JiuNuoDesktopPet-Portable\JiuNuoDesktopPet.exe`, which can be run after extracting the ZIP of the same name in full; packaging and verification status are defined by [the character release progress](docs/CHARACTERS.md#current-progress-and-phase-exit-conditions). Exit the old process from the tray first, then open the new version's normal EXE. Do not mix in copied files from an old preview, test or Behavior Control build.

The existing separate Behavior Control edition is retained and is not currently updated with the official edition; sections 0.6-0.7 are for reference only if you still have an old standalone development test package.

The vertical tear is still observed the original way: the seventh item of the standalone demonstration is the window tear, and the earlier hat-hiding and smash items need a right-click return before it can continue. The important parts are the temporary growth → gripping firmly and pressing low to wind up → pulling apart upwards and downwards → a brief hold at maximum spread → releasing and shrinking back to the original size → both pieces fully stored in the hat → a right-click returning it to its original position. Check this separately at 60%, 100% and 200% body size, at different Windows scale factors, and on a secondary monitor; the performance should not change the saved size, and a window where her hand or face overlaps should still be right-clickable or draggable. After an interruption the temporary size should also be cleared. The size evidence for a normal completion is `PrankTearGrowth` and `PrankTearSizeRestored`, and the storage evidence is still `PrankRemainsStored`.

## 0. Starting and quitting

### v1.3.1 four fixes (awaiting user testing)

Exit the old version from the tray first, then start the v1.3.1 official package; turn coexistence on and quiet mode off.

1. **Volume**: fix the volume slider in one position, compare both characters' everyday recordings, then listen to Feibi's line before a finisher. A loud recording should be attenuated, and there should be no sudden return to the original loud volume after moving the slider. Whispered and sleepy clips should still be quieter, and with mute on neither character makes a sound. The `VoicePlayed` and `VoiceBurstStarted` logs include the shared volume and the actual compensated volume.
2. **Icon relay**: delete a newly created temporary shortcut and check that after it is kicked into her mouth she only carries on chewing, without opening her mouth to slurp again; then delete three temporary shortcuts at once. Immediately after a catch, pick her up, open the menu or switch modes, and an icon already eaten must not be queued again; the log should show one `GhostConsumed` per item.
3. **Cheek pinching**: for each character, pull each cheek lightly, pull far and hold, and pull diagonally up and down before releasing. Check the narrowed eyes, the blush, the mouth opening and closing and the soft rebound, with no leftover black edges, skin splits, body size changes or ghosting. The body and hat should still drag normally. Retest after turning both characters to face the other way, and test at 100%, 125% and 150% scaling. The effect is referenced against [the offline preview](docs/media/pinch-continuity-v131.gif), and the subjective result is still to be confirmed.
4. **Long-distance feeding**: put the two at opposite ends of the screen and feed from the right-click menu; they should turn where they stand, take the hat off, pull the food out and throw and catch it, without walking together first. Swap left and right, then put one on a window ledge; with two monitors, put one on each. The food should reach her open mouth, and hunger should drop only after the catch. Dragging a character away during the flight, moving the window she is standing on, or turning coexistence off should all end it safely with no food left behind. `KeepPositions=True` for Feed in `PairSceneStarted`, along with the distance and the flight time, helps locate problems.

The automated verification runs silently in the background throughout and does not start the pet for you. If something goes wrong, providing the step, a complete screen recording and the log for that period is enough; the actual file contents are not needed.

### Cheek pinching and nibbling (official package, awaiting testing)

Exit the old version first, then double-click the root EXE of the current official package. Switch between Nuonuo and Feibi Jiubi, or turn on coexistence and try each in turn:

1. While the character is idle, hold the left button on her left cheek and drag slowly outward, then pull quickly to a greater distance and hold for 1-2 seconds. The cheek outline should stretch continuously, and holding should open and close her mouth briefly; the eyes, hairpin, body and feet should not stretch along with it, and there should be no character pulling her own face, no double outline and no body size change. Releasing shrinks back from the current position.
2. Repeat on the right cheek, checking that both sides register hits, and that Feibi Jiubi's blue hairpin stays on its original side. Repeat at 60%, 100% and 200% size and on a secondary monitor at a different scale factor; the gesture range should scale proportionally with the character size.
3. Dragging from the hat or the body should still be the original pick-up and throw, and must not be mistaken for a cheek pinch. Releasing the mouse mid-pinch, right-clicking to open the menu, clicking "Bring her home", or turning coexistence off should leave the character neither stuck nor with a permanently stretched face.
4. If there is misalignment, dropped frames, stuttering or continuous nibbling that never plays, give the character, which cheek, the size, the monitor scale factor and the time, and provide the nearby `CheekPinchStarted`, `CheekPinchReleased`, `CheekPinchSettled` or `UiFrameSlow` entries from `activity.log` plus a screen recording.

The implementation and asset provenance are in [the cheek pinch guide](docs/CHEEK_PINCH.md).

### v1.2.1 animation continuity retest

First watch two consecutive loops of solo walking and running, then turn coexistence on and watch the cheek pinch, the nap and the food throw in turn. The key things are whether the body size and the top of the hat stay complete across transitions, whether the body jumps sideways when a hand or foot extends, and whether the food lands in her mouth. Test at 100% and 125% separately. She should visibly grow only during the tear finisher's designated phase; if the whole pet still scales repeatedly at other times, provide the name of the action that misbehaved, a complete screen recording and the log for the same period, so asset problems can still be told from display environment problems.

### Right-click menu stutter retest (8 September local official build)

1. Exit the old process and run the normal entry point of `dist\JiuNuoDesktopPet-Portable\JiuNuoDesktopPet.exe`, not the automated feature demonstration.
2. Right-click the character while she is walking, and separately try right-clicking the tray once. She should stop once the menu opens, and hovering items, opening the "Pet character" submenu and dragging the size slider should all respond.
3. Press Esc or click outside the menu to close it, then open it again; she should carry on moving, with no sudden fast-forward and no permanent freeze. Check once for each character.
4. Open the menu while a window is in the hat; the window must not be returned automatically because the menu changed the foreground. Clicking "Give the window back" deliberately should still work.
5. If it still freezes, note roughly when and what she was doing, and provide `activity.log` (and `activity.previous.log` if needed). The new `PetMenuOpened` and `PetMenuClosed` locate a menu session, and `UiFrameSlow` means one interface frame's work exceeded 100 milliseconds, which is not the same as the whole freeze duration.

### Both pets together and synced settings (v1.2.2 official package, awaiting testing)

Exit the old process first and start the updated official package. Once both appear, right-click either one and use that single menu to check:

1. Tick "Both pets together" and confirm both appear at once, side by side initially; drag each one and the other should still move on its own. The coexistence preference should persist when you start the program again.
2. Adjust the "size" slider and watch both characters' ordinary body size change together; drag them to different monitors and adjust again, allowing different actual pixel sizes because of each screen's work area and DPI.
3. Tick "Quiet mode" and confirm both stop autonomous walking and mischief; unticking restores it. Tick "Mute" and trigger each character's sound; neither should make a sound.
4. With both modes off, both bars are shown. Tick "Fasting mode" and "Infinite mode" separately and confirm the hunger bar and the mischief bar disappear together immediately; with both on, whichever you untick first, both bars should stay hidden, and they return together only when the last one is unticked. Hiding does not clear Feibi's mischief value, and she still makes mischief on her original cooldown.
5. Right-click either one and choose "Bring her home"; both should return to adjacent positions on the same screen. "Play a prank" and "Give the window back" affect only Feibi, and fasting changes only Nuonuo's hunger behaviour, but while coexisting it hides both characters' bars in step.
6. Turn "Both pets together" off, or choose "Pet character", and confirm only the chosen one remains with a single tray icon; a window in Feibi's hat is handled by the original safe return rules before she leaves coexistence. Quit from the tray and both should disappear.

7. With fasting on and infinite off, quit and start again; both bars should still be hidden. Repeat with Nuonuo and then Feibi as the primary character. After turning coexistence off, a solo Feibi should get her mischief bar back while a solo Nuonuo still hides the hunger bar; turning coexistence back on hides both again.

If a step fails, note the time of the action and provide the `CoexistenceModeChanged`, `ProgressDisplayVisibilityChanged`, `PetLoaded` and related settings events from the log. The official record of the rules is in [both pets together and synced settings](docs/CHARACTERS.md#both-pets-together-and-synced-settings-2026-09-23-adopted-awaiting-on-device-acceptance).

Before testing, exit the old version from the tray so the old process's single-instance protection does not take over the new launch request. After extracting this round's ZIP in full, double-click `JiuNuoDesktopPet.exe` in the root folder; locally you can also use:

```text
.\dist\JiuNuoDesktopPet-Portable\JiuNuoDesktopPet.exe
```

The release package bundles the runtime, so no extra components need installing. Only when developing from source, use this alternative entry point in the project directory:

```powershell
dotnet '.\src\SoftMochiPet\bin\x64\Release\net8.0-windows\JiuNuoDesktopPet.dll'
```

When testing finishes, right-click the current character's pet icon in the system tray and choose "Exit". Do not kill the process directly, since this way the current character's life state is saved immediately.

### 0.0.1 Official name and upgrade compatibility

- The portable root has exactly one normal launch EXE, `JiuNuoDesktopPet.exe`, and the official package contains no test tools. A normal launch runs no demonstration automatically.
- The product name of both the root launcher and the main EXE inside `Program` is `JiuNuoDesktopPet`; the window and tray tooltip show "JiuNuo Desktop Pet · current character", and the character menu still reads "Nuonuo" and "Feibi Jiubi".
- The new version should continue reading the old version's character choice, size, volume and mute and both characters' saves, without creating a new branded data directory. The specific compatibility decisions are in [the official rename notes](README.md#official-rename-and-compatibility-with-older-versions).
- With an old version's process running, the new version still honours the same single-instance protection; exit the old version before starting the new one, so you do not mistake the old version's display for the update having failed.
- To start with the system, tick "Start with Windows" again in the new version's menu and check that the Run command points at `JiuNuoDesktopPet.exe` in the new directory, that the value name is still `SoftMochiPet`, and that no second branded startup entry is created.

To watch the log, open another PowerShell:

```powershell
Get-Content "$env:LOCALAPPDATA\SoftMochiPet\activity.log" -Wait
```

The log saves a runtime snapshot every two seconds, and state changes are recorded immediately; past 8 MB the old content moves to `activity.previous.log`.

## 0.1 Voice

Choose Nuonuo for these recording checks first. Voice is on by default on a first launch, and existing mute and volume preferences are retained. Trigger each of the following:

- delete a dedicated test file to trigger feeding: the slurp should play a positive tone, and finishing should play an affirmative one;
- autonomous icon licking at high hunger: setting off should sound aggrieved; with no visible desktop icon it should play the very sad tone;
- autonomous exploration and sleep: you should hear the questioning and the calm tones respectively;
- being tossed up quickly by a window: it should rotate through at least 7 softer short responses, and the previous sharp "scream" should no longer appear; a quick downward pull can still play the panicked out-of-control clip, interrupting any ordinary line playing;
- flinging her or dropping her from a height: a short landing response plays only on an obvious ground impact where no line played on the way down; ordinary small steps, her own jumps, and a "shout on the way plus shout on landing" pair should never occur;
- begging, icon licking and the roll while hungry share one response interval, and should not take turns within tens of seconds repeating the same line;
- dragging her with the left button: there should be an aggrieved tone, but clicking repeatedly does not stack the audio.

After ticking "Mute" in the tray, triggering an action should be completely silent; unticking plays an everyday response. The same recording must not play again within 12 seconds even if it belongs to a different action; `VoiceSkipped Reason=FileRepeatCooldown` in the log means that repeat protection took effect. `VoicePlayed`, `VoiceCompleted`, `VoiceMissing` and `Voice media failed` also help locate asset, interruption or decoding problems.

### 0.1.1 Feibi's own recordings

Using this round's portable package, switch to "Feibi Jiubi" and unmute. All the recordings and the situations they apply to are in [the voice guide](docs/CHARACTERS.md#feibis-own-voice). Leave a cooldown between actions, and do not expect a sound at every animation phase:

1. On switching in, being brought home, unmuting, and during standing idle outside quiet mode, you should hear Feibi's own everyday recordings. All 9 numbered recordings are in the random pool, though random playback does not guarantee hearing them all in a short time. The `File` field of `VoicePlayed Character=feibijiubi` confirms which one was chosen.
2. Check "sleepy" during autonomous sleep and the aggrieved set while dragging; when flung or tossed by a window she should respond from the 9 numbered recordings at random and no longer play the "Feibi Jiubi falling" file, while stopping after exploration is still "puzzled". Fling her a few times with cooldowns between, and `Cue=Scream` / `Panic` in the log may only select a numbered file; a numbered file just played in an everyday context must not be reused for a fall within 12 seconds. A slight drop should not make her cry out repeatedly, the short landing response is unchanged, and none of Nuonuo's recordings should slip in.
3. Prepare a test window as in section 0.5: kicks, punches and charges get a positive or excited response, hat hiding leans towards soft whispering, a full-bar tear gets the gritted-teeth response, and the ending gets a short playful one. Mischief lines are at least 3.5 seconds apart, and a short phase being skipped is the anti-overlap protection.
4. Tick "Mute" while a sound is playing and it should stop immediately; dragging and mischief afterwards stay silent. Unticking restores it, and the volume bar in the right-click menu works. In quiet mode there is no automatic everyday chatter or mischief sound, while sleepiness and manual dragging reactions are still governed by mute.
5. Switch characters or cancel mischief while a sound is playing and the old sound should stop. Switching back and forth should not bleed audio, play two clips at once, or repeat the same file within 12 seconds.
6. Check the actual loudness, noise floor, clipping and how well each fits its action, paying particular attention to whether `041` and "dejected soft" are too quiet. Nothing was auditioned or adjusted in the background; if something does not fit, give the `File` name and the triggering action so the mapping can be changed precisely.

## 0.2 The speech bubble toggle

Bubbles are off by default; verify the off state first, then deliberately tick "Show speech bubbles" in the right-click menu:

- While off, watch startup, standing idle, exploring, waking, climbing, dragging and going home: neither character should pop up a speech bubble. After a restart the toggle should still be at its default or at whatever you chose.
- Once on, each character shows short lines matching her temperament, the bubble follows the character, passes the mouse through and never steals focus from the current application; dragged to a monitor edge, or onto a monitor at a different scale factor, the text should still be complete and not covered by the taskbar. The file name on Nuonuo's fake icon is still displayed as before.
- With coexistence on, trigger it manually from "Two-pet interactions → Let them chat"; one should speak a line and the other answer, never both in one bubble. After standing idle for a while they should also chat on their own occasionally. Dragging or the start of a dedicated interaction should interrupt the conversation, and must not freeze either of them.
- Turning "Quiet mode" on should hide the bubbles immediately, and turning it off restores your toggle preference. Ticking "Mute" alone only silences sound and does not turn off the text; turning the bubbles off should likewise not silence her.
- Check the existing voice and volume preferences as in section 0.1. Feibi can still play an everyday recording at random during standing idle outside quiet mode, and the `IdleVoiceAttempt` log locates when it was attempted.
- Nuonuo's hunger bar when not fasting, Feibi's mischief bar, and the right-click and tray menus should all display normally; the bubble toggle should not hide any of those controls.
- Feature test progress appears only in the right-click menu, the test window title and the report, and should never show a step, a countdown or closing text above her head.

## 0.3 Startup and life modes

Right-click the character or the tray icon to verify start with Windows and quiet mode; fasting mode appears only in Nuonuo's menu:

1. Tick "Start with Windows" and the current user's Run entry should point at a valid entry point for the edition in use; unticking should remove it. Source mode should not mistakenly register an old portable package entry point, and if the current source launch method cannot register, it should clearly report a failure rather than showing success.
2. Tick "Fasting mode (never hungry)" and the hunger label and bar above her head should disappear completely and immediately, and stay hidden after a restart; the internal hunger value is pinned at 0, and during observation there should be no begging, icon licking or hungry roll, while ordinary walking and responding to ordinary deletion feeding remain.
3. After unticking fasting mode the hunger bar should return immediately and hunger should grow normally over time, without jumping back to the old high value from before it was enabled.
4. Tick "Quiet mode (stand or sleep only)" and the current roaming or interaction should stop; while watching, she should only stand and blink or enter sleep at random, with no walking, climbing or icon licking.
5. In quiet mode, delete a dedicated test file; it should trigger no running or feeding. Left-button dragging, falling after release and direct window impacts should all still work. Autonomous behaviour resumes once quiet mode is turned off.

The log should show `StartupRegistrationChanged`, `FastingModeChanged` and `QuietModeChanged` respectively. Once testing is done you can leave the toggles however you prefer.

## 0.4 Character switching and isolation

Record the settings, Nuonuo's `TotalMeals` and Feibi's mischief value first, and do not delete, merge or rename a save. The paths are defined by [characters and data boundaries](docs/CHARACTERS.md#characters-and-data-boundaries).

1. Open "Pet character" in the tray and you should see "Nuonuo" and "Feibi Jiubi". Switch between them repeatedly, back and forth; there should always be exactly one pet window and one tray icon and the process should never restart, with the menu tick, the tooltip name and the size label all matching the actual character.
2. Switch during ordinary idling, walking, sleep, on a window platform and inside a window container. The old character's visuals, sound and actions must not bleed into the new one; with bubbles off neither shows text, and with them on only the current character's lines appear. After switching she should find valid support again or fall naturally, never hang in the air.
3. Request a switch while dragging; before you let go the old character and the drag state should be kept, and the request applied after release, without the mouse capture getting stuck. Request again and click the currently ticked character before it takes effect, which should cancel the pending switch and keep the original character after release.
4. Choose Nuonuo, delete a dedicated file, and request a switch to Feibi at three moments: while she is heading to the fake icon, while her mouth is slurping it in, and during the satisfied animation. The current item already in the swallow or satisfied phase finishes first; once the switch takes effect, all pending fake icons and the queue are cleared, nothing is handed to Feibi, and no meal is counted twice.
5. Create and delete 5 dedicated files as in section 6, switching from Nuonuo to Feibi partway through. Only what Nuonuo genuinely finished eating before the switch should be recorded, and the remaining fake icons cleared. Feibi produces no feeding increment, and switching back to Nuonuo does not make up for the cancelled old queue. The system's real deletion results are unaffected.
6. Switch at 60%, 100% and 200%; the size percentage holds and the label changes to the current character's name. Tick "Mute" and switch back and forth, and both should be silent; untick it and each should return to her own recordings with no bleed.
7. Turn fasting on for Nuonuo and switch to Feibi: Feibi should still have only the mischief bar and no fasting menu item; switching back to Nuonuo keeps the original fasting preference. Quiet mode is shared; Feibi makes no mischief and gains no bar value, while an already stored window is kept and can be restored by right-clicking.
8. Have Nuonuo eat a dedicated file, and use "Tease her" to raise Feibi's mischief value. Switching back and forth and a normal exit and restart should leave each character's record continuous, with Feibi inheriting no meal count and Nuonuo no mischief value. When Feibi deletes a dedicated file she should leave no fake icon and neither run to eat nor lick it.
9. If Feibi's assets are still missing frames, selecting her should keep the current character, save and tick, with `CharacterChangeRejected` and the missing asset reason logged and no bubble shown. Do not modify the original assets for this step; a missing-frame situation may only be created deliberately in a separate test copy.

`CharacterChangeRequested`, `CharacterChanged`, `CharacterChangeRejected`, `CharacterStartupFallback` and `CharacterSpritesReady` help locate the timing; an event simply not existing is no substitute for accepting the visuals. If something goes wrong, record the characters before and after, the state she was in, and the menu mode.

### 0.4.1 Feibi's visuals and character behaviour

The checks below require the 33 action sets, the hat prop and the masks to be fully prepared; the current status is defined by [the character phase progress](docs/CHARACTERS.md#current-progress-and-phase-exit-conditions).

- Watch the complete animations during idling, running and walking, being tossed, falling, landing, sleeping, climbing, dragging, jumping and a whole mischief sequence: the big white hat, the blonde hair and both little feet should stay complete and must not be cropped because an action's range grew.
- Set 60%, 100% and 200% in turn and check the hat brim and hair for magenta edges, white halos or black backing; the white hat and clothes must not become transparent, and the purple eyes and their original highlights must not be keyed out.
- Both feet keep one standing baseline; adjacent frames should not jump in size or position suddenly, and a loop should not twitch at its start and end. Jumping and running should visibly push off the ground with the hat brim and hair following slightly behind, rather than conveying liveliness through faster playback alone.
- Feibi should never show a tongue, swallowing, satisfaction or hunger performances; check mischief contact of the foot, fist, shoulder and both hands as in section 0.5, with no hits through thin air. Climbing and edge sliding should keep her hands and feet against the window edge.
- Watch for several minutes at similar sleepiness and curiosity: Feibi should be brisker and lean towards exploring and jumping, still resting when sleepy and never bypassing quiet mode. Mischief is not mouse tracking and she does not chase the mouse.

## 0.5 Feibi's window mischief

Prepare a test window you have just opened yourself, such as an empty folder window, at a normal size rather than maximized, entirely inside a single monitor's work area, with at least one whole pet's width of empty space to its right. Put Feibi on that same screen. A manual "Play a prank" can attack the current foreground window directly; a maximized, fullscreen, genuinely obscured, protected, or permission- or boundary-unsafe window is still refused by the safety checks, and refusing mischief when no compatible target is found is normal.

1. Switch to Feibi and confirm there is only a mischief bar above her head, with no hunger number or hunger bar; the menu should have "Allow autonomous mischief", "Mischief", "Play a prank", "Tease her" and "Give the window back", and no fasting mode. On a brand-new configuration, or the first upgrade from v4 or earlier, the autonomy toggle should be ticked; a choice already turned off at v5 should be preserved. The status row is not clickable, and the return item is unavailable while no window is stored.
2. To concentrate on the manual actions, turn autonomy off first and confirm the bar turns grey and the status row says it is off. With the bar unfilled and the hat free, click "Play a prank", wait for it to finish completely, then click again; it should rotate through kick, punch, charge and hat hiding. Each one moves a single target and must not knock other windows in a chain. With something already stored, test that separately in the next section.
   Also click "Play a prank" once during her `sleep` or a just-landed `land` animation: once the menu closes she should wake up or wait for the landing animation to end, then run it automatically. The log should show `MischiefManualRequested`, plus `MischiefManualDeferred` and `MischiefManualDeferredExecuting` where applicable, and must neither lose the request nor turn the menu click into a drag.
3. Kicks, punches and charges should all wind up first, and the window should move only once the foot, fist or shoulder touches the real window edge. The window is not activated, not resized and not pushed out of the work area; the character ends after the action's own hand recovery, leg recovery and settling, with no crouch, reach or gloating added. The window must not slide away before she has touched it.
4. Hat hiding should show, in order, the hat coming off, being held, the real window shrinking to a card, passing the hat opening and being covered by the front brim, and then going inside; the real application is only minimized and its taskbar entry remains. Once Feibi's hat is back on she can move normally with the window still stored; wait a while yourself and it should never be taken out automatically. It must not use a generic fake window or merely fade out transparently.
5. Right-click "Give the window back" and you should see the same card unfold and restore the real window with its content unchanged. Clicking the hat or the effects no longer triggers a return. Separately, restore the target yourself from the Windows taskbar: Feibi should stop holding it and never store it again.
6. While the window is being kicked or during a performance, deliberately activate, drag, maximize or close the test window, and check that the pet gives up control immediately; it must not keep pulling it back to its old position, re-minimize it repeatedly, or leave a screenshot on the desktop.
7. With autonomy still off, click "Tease her" repeatedly at least 1.5 seconds apart, each adding 12 points; rapid consecutive clicks should not stack. The grey bar stays unpulsing, and even at full she does not modify a window by herself — you still have to click "Play a prank". Separately, with autonomy on, watch for the slight bounce as it approaches full, which should not strobe.
8. With the bar full, trigger it manually: the hat is thrown first, flies to one side and bounces once, and the character becomes hatless; then the burst kick, with the cartoon shattering appearing only on the hit. The wind-up and the shards spreading are more deliberate than an ordinary attack so the action is clear; the shards keep that same test window's content and then go into the hat, rather than restoring the real window automatically. The character retrieves that same hat and puts it on, with no crouch or reach added; only after you wait and right-click to restore does it come back out of the hat and reconnect to the real window.
9. After a finisher the bar resets and the status row shows the cooldown; the durations are defined by [the official rhythm parameters](docs/CHARACTERS.md#autonomous-mischief-defaults-and-rhythm). During the cooldown the bar does not keep filling and nothing fires automatically, while manual small actions can still be tried. Right-click to restore the stored window first, then after the cooldown fill the bar by teasing and trigger manually again; the next full-bar move is the tear: first both hands grip and build effort, then it pulls clearly apart up and down. The rupture and both halves must follow both hands, rather than a separately played centre-line cut. The halves then go into the hat, the real window stays minimized, and only a right-click restore returns it.
10. After throwing the hat and taking over a window, grab Feibi deliberately, drag her, toss her up, then set her down gently. The window should stay stored and must not restore automatically because of the interruption; dragging, being tossed, falling and landing all stay hatless, and once steady she goes back to pick the hat up and wear it, with you ultimately restoring the window by right-clicking. No orphaned hat should be left behind, and the mouse capture must not lock up.
11. After hat hiding and after a hatless finisher respectively, turn quiet mode on, click "Bring her home", change the size, or drag her across screens: the window should stay stored and the right-click restore should still work. Switching characters, a normal exit, locking, sleeping, or a real display layout or work area change are safe exceptions and should return the held window and clean up the effects; the old character's animations must not bleed into the new one, and the content and saved state stay unchanged.
12. Turn "Allow autonomous mischief" on, the bar returns to blue-purple, and watch the status row move from building up to cooling down or waiting. Accumulate genuine active time as in [the official rhythm parameters](docs/CHARACTERS.md#autonomous-mischief-defaults-and-rhythm): completing an ordinary action does not reduce the bar, and a full bar triggers exactly one finisher. With no suitable window it shows waiting and the retry time, and a full bar must not drop back from 100 because of that; autonomous mode never force-activates a window or widens the safety range in order to attack. Leaving the menu open for a long time, sleeping, and the performance itself do not count as fill time.
13. Nothing fills during sleep, quiet mode or a Windows lock; after unlocking, waking or restarting from offline there should be no burst of activity. Turning autonomy off should cancel a pending intent immediately, and turning it back on should not fire the old task straight away.
14. Repeat kicking, punching and hat hiding at 60%, 100% and 200% and on screens at different DPI, confirming the contact, the card, the brim occlusion and the hat prop all still align; if a window spans screens, is obscured, or has too little space to its right, it should be refused safely and must not capture the wrong application's image.

Record the type of misbehaving action, the bar value, the toggles, the menu status row, whether the target was foreground or obscured, the monitor scale factors and a complete screen recording. In the log the key entries are `MischiefStatus`, `PrankStarted`, `PrankPhaseChanged`, `PrankInterrupted`, `PrankCompleted`, `prank.motion.*`, `prank.snapshot.*` and `prank.cancel`; the mischief log never records a window title, its content or a screenshot.

### 0.5.1 Autonomy upgrade, status and two-window storage

This part uses the normal entry point, and you prepare two windows A and B with nothing important in them. Each has to meet the safety requirements of section 0.5; manual testing allows the current foreground window, but it must not be maximized, fullscreen or genuinely obscured. Everything below awaits on-device acceptance, and offline tests are no substitute for the result with two real windows.

1. After the first normal launch of the new version, check that `settings.json` is at version 5. If the original file was below 5, autonomy should be enabled this once; then turn it off from the menu, exit normally and start again, and it should still be off with a grey bar. Do not delete or tamper with your existing configuration for this test; new installations and the other overrides in an old-version migration are checked by isolated offline fixtures. The feature test entry point should not trigger a real configuration upgrade.
2. With autonomy off, first hide A in the hat, or complete a smash or tear and store it. A stays minimized with its taskbar entry. Put B somewhere safe on the same screen that Feibi can reach, click the desktop to take B out of the foreground, then click "Play a prank": she should be able to kick, punch and charge B, A should not be restored, B should not be hidden in the hat, and A's original image must not be overwritten.
3. Run small actions through to completion repeatedly. Even with the bar full and the hat occupied, only small actions on B are possible, the status row explains that it is waiting for the hat to free up, and B is neither stored nor destroyed again. An ordinary small action does not reduce the bar, and A is never returned automatically just because time has passed.
4. While B is being kicked, punched or charged, activate or move B deliberately so that this small action is cancelled. A should stay minimized, and B's ordinary failure or takeover must not trigger A's return. Then try clicking "Give the window back" during another small action: B's action stops first, then A is retrieved; the restored content, window identity and original position must all belong to A and must not get crossed with B.
5. Store A again, and while a small action on B is running, restore A yourself from the taskbar: A's storage ownership should be released, A should not be re-minimized, and no old snapshot should remain, while B finishes according to its own safety result. Only once the hat is free can a later hat hiding or finisher use that slot again.
6. With A stored, going home, quiet mode, size changes, ordinary dragging across screens and opening and closing the right-click menu should all keep A. Separately check switching characters, a normal exit, locking or sleeping, and a real display layout or work area change: the current small action should stop and A should be released safely. Do not substitute killing the process for testing a normal exit.
7. Turn autonomy on and prepare a suitable B, and check that with A stored she still attempts small actions on the ordinary rhythm. With no safe B she only waits and retries, and a full bar stays at 100. Watch whether the menu status accurately distinguishes off, building up, cooling down, waiting for a window, and the hat being occupied; while dragging she can still build up and the status says it is waiting for you to let go, and she must not suddenly strike while held. An unfilled value should never show as 100 early. At the same time, retest the right-click highlight, the submenus, the slider, and movement continuing after closing with Esc, with no catch-up fast-forward.
8. On a 100% primary and a 125% secondary monitor, prepare a compatible non-maximized window with nothing important in it and let Feibi launch a smash finisher autonomously. The key sequence is `FetchHat → PickHat → CollectRemains → WearHat`: picking the hat up should neither hang nor exit the process, and the shards should go into the hat; the target stays minimized during the wait, and right-clicking "Give the window back" restores that same window. If something goes wrong, keep the lines near `PrankPhaseChanged`, `Hat prop`, `PrankStoppedSafely`, `prank.cancel` and `Stack` from that run's `activity.log`, and sanitize local information before sharing.

In the log, `PrankStorageParked` means A was parked for the next small action and `PrankStorageReturnRequested` means the stored one was reconnected; judge this together with `PrankManualRestoreCompleted`, the native confirmation and `MischiefStatus`, rather than deciding the visuals passed from an event name alone. Before sharing a log, check it and sanitize personal information beyond paths and handles.

## 0.6 Feibi's standalone feature demonstration

This entry point demonstrates only Feibi Jiubi's exclusive gameplay and creates its own dedicated test window automatically. The official scope, the problems found and the fix acceptance status are in [the standalone feature test entry point](docs/CHARACTERS.md#the-standalone-feature-test-entry-point-adopted) and that document's current progress.

1. Exit the current pet from the tray first. The feature test entry point is provided only in development builds, and the official package contains no `Test Tools`. In a development build directory, double-click `Test Tools\JiuNuoDesktopPet-FeatureTest.exe` without moving it on its own, and use the official package's root `JiuNuoDesktopPet.exe` for everyday launches.
2. Watch the current step in the right-click menu and the dedicated test window title; no speech bubble should appear. The order is fixed: kick a window → punch it away → charge → hide it in the hat and wait for a right-click return → tease until the mischief bar fills → throw the hat, smash the window, store it and wait for a right-click restore → throw the hat, tear the window, store it and wait for a right-click restore → pick the hat up after a hatless interruption and wait for a right-click restore. The step identifiers in the report are, in order, `Kick / Punch / Charge / HatStoreReturn / Tease / Shatter / Tear / HatlessInterruption`. There is no separate demonstration of walking, running, sleeping, window physics, size, mute or going home; running to approach a target is still part of the exclusive actions.
3. The demonstration inherits your existing size, volume and mute preferences, and will not turn sound on if you had it muted. If you want to accept how it sounds, it is up to you whether to unmute. The demonstration saves only temporary session settings and state, and changes no real preference, no real life save and no start with Windows, and never operates your application windows.
4. If an item fails, confirm the flow stops at that item and that later steps are not marked as failures one after another. Note the hint and the step, fix whatever is in the way, then right-click and choose "Retry this item"; it should redo the current action, and only on a pass does the next item run.
5. To stop early, right-click the character or the tray and choose "Stop feature test"; left-clicking the character also stops it. Locking, sleeping and quitting all cancel the demonstration. Confirm the dedicated test window and any leftover effects are cleaned up and that later steps do not continue; mute still takes effect immediately during a test.
6. After the demonstration finishes, pauses on a failure, or is stopped, look at `%TEMP%\SoftMochiPet.FeatureTest\<session>\feature-test-report.json`. If you cannot find this run's directory, look up `ReportDirectory` on `FeatureDemoStarted` / `FeatureDemoFinished` in the diagnostic log.

Hat hiding is demonstrated only once. The hat hiding, smash, tear and hatless-interruption items enter `WaitingForManualRestore` once the performance finishes, which means a normal wait and not a failure. You can watch for a while first to confirm the window is not restoring on its own, then right-click "Give the window back"; restoring immediately is also allowed, with no minimum wait. Human waiting has no timeout, while the animations and the restore check after a request still do. The program must never call the restore itself to continue the demonstration, clicking the hat or the effects is no substitute for the right-click, and clicking the character stops the whole demonstration.

If, in the eighth item, you right-click to restore before the hatless dragging, tossing and hat retrieval have been observed, the simulated action should stop immediately and returning the window takes priority. Once the real return is confirmed, that item and the whole report are marked `NeedsReview` with `Evidence.HatlessInterruptionObserved=false`; that is not a window restore failure, and it must not be taken as the complete hatless performance having passed. To observe it fully, run this entry point again and right-click to restore only after the hatless performance ends.

Filling the bar by teasing is an independent step and should not be blocked by a window precheck failing; the later window actions still have to prepare their own test window and pass the real safety checks. Watch that preparing the window does not steal focus and that it enters the action only once the target has a visible position; this build's whole run on real hardware still needs confirming as described in this section.

This build also needs you to watch the attack's wind-up and sudden strike, the brief pause and small star flash at the moment of contact, the window moving away quickly and the character's single rebound, with no hits through thin air, no back-and-forth jitter and no sliding on the spot. An attack no longer adds a crouch, reach or gloat at the end; the finishers are slowed down so the wind-up, the strike and the destruction are all visible. At the smash, tear and hatless-interruption steps, once the bar is full she should first say the specified "Feibi Jiubi" line, and only once the whole line finishes should she approach the window and throw the hat; with mute on the sound is skipped and she should not stall there. After quitting or stopping during playback, the attack must not continue. The voice logs are `VoiceBurstStarted` and `VoiceBurstFinished`, which should end before `PrankPhaseChanged Phase="Approach"`.

`Completed` requires that window's real displacement, or the minimize and return check, to have passed, and says nothing about the visuals or sound being acceptable. A target in the hat, smashed, torn, or hatless-interrupted must first receive a genuine right-click request, after which that same window is confirmed restored; the eighth item cannot pass on retrieving the hat alone. The report version should be `Revision: 6`, `ManualRestore.State` distinguishes `WaitingForManualRestore`, `ManualRestoreRequested` and `Restored`, `HeldSeconds` records how long it actually waited, and `RequestedAt` records the time of the genuine right-click request. A finisher should pick the hat up first, then put the shards or halves into the hat opening, and once it is on there should be no shards left on the desktop; after a right-click it comes out of the hat, the window image is reassembled, the real window is restored and the hat goes back on. Observation and a screen recording of it still not restoring after you have waited can be accepted alongside those fields; clicking restore quickly does not prove that a long hold was adequately observed.

For a target failure look at `LastTargetCheck`, for a successful action look at that run's `Evidence`, and for the phase look at `PhaseHint`; the log contains `FeatureDemoWaitingForManualRestore`, `FeatureDemoManualRestoreRequested` and `FeatureDemoPhase` plus the native events `prank.target.inspected`, `prank.selection.rejected` and `prank.operation.confirmed`. Please provide this run's report and the matching log, checking and sanitizing local paths before sharing. Multiple monitors and different scale factors, mouse dragging and clicking in normal mode, start with Windows, and locking and unlocking all still need testing separately and are not part of these automated steps.

## 0.7 The standalone Behavior Control edition

The entry point is `dist\JiuNuoDesktopPet-BehaviorControl\JiuNuoDesktopPet-BehaviorControl.exe`, started from the root folder after extracting the Behavior Control ZIP in full; there is no "Test Tools". The scope and isolation rules are in [the Behavior Control guide](docs/CHARACTERS.md#the-standalone-behavior-control-edition-adopted), and the delivery status is defined by the final section of that document. Everything below is for you to accept personally, and background checks are no substitute for these real actions.

1. Exit the old pet before opening the Behavior Control edition. Right-clicking should show "Trigger a behavior", categorised by the current character, with no automatic walking or running by default; "Automatic daily activity (Behavior Control)" is off at every restart. The ordinary and Behavior Control editions cannot run at the same time, and opening a second one should tell you to exit first rather than pulling the character you are working with back home.
2. For each character, click through stand, curious, walking and running, explore, roam, hop, roll, sleep and wake, free fall and toss. It should begin only once the menu closes, walking and running should produce real displacement, and "Wake up" should only be available after she is asleep. Use a window's top edge to check climbing, sliding down and jumping down; with no terrain, space or support it must not pretend to succeed.
3. Switch to Nuonuo, turn fasting off, show the desktop, and click "So hungry", "Lick an icon" and "Slurp an icon copy" in turn. The copy's appearance should come from the real icon, and after she eats it the original icon and file should still be there; "Satisfied (Animation showcase)" plays the animation only and adds no feeding count. Feibi's menu must not show any of these food items.
4. Prepare a window with nothing important in it for Feibi, positioned on the same screen where she can reach it and unobscured. Click kick, punch, charge, hide in the hat, smash, and tear directly; a targeted attack does not need a full bar first. The current foreground window should also be targetable manually, as long as it is not maximized or fullscreen. Where a target is unsuitable, read the menu status; the program must not activate a target or widen the safety range on its own to make something work.
5. After hat hiding, or after a smash or tear is stored, wait and the real window should still be stored; "Stop current action" must not act as a return. Choose another compatible window and check that kicking, punching and charging still work, while a new hat hiding or finisher should be greyed out because the hat is occupied. Finally click "Give the window back" and confirm the original window's content and position are fully restored. Check the endings for stopping partway, going home, switching characters and a normal exit separately.
6. Click toss the hat and fetch it, tease, and fill the mischief meter. Throwing the hat should end with it fully retrieved and worn, leaving no hatless intermediate state, and the interval between consecutive teases is retained. With quiet mode on, with her asleep, while held in your hand, or with the hat occupied, check that the relevant items are correctly greyed out with accurate status. Opening the menu, moving between submenus and the size slider should all stay responsive, with no catch-up fast-forward after the menu closes.
7. Change the character, size, volume and mute in the Behavior Control edition, then exit and reopen; the preferences should persist and "Automatic daily activity (Behavior Control)" should still default to off. Then exit the Behavior Control edition, open the official edition and verify your original settings and life record were not overwritten by it. Do not delete or tamper with the original configuration to create a migration scenario; the first-run whitelist and the protection of an old configuration are covered by isolated offline fixtures.

For a problem, record the character, the menu item, the conditions at the time and the time itself, and locate it with `BehaviorControlStarted` / `BehaviorControlUnavailable` / `BehaviorControlStopped` / `BehaviorControlCompleted` plus the existing window logs. The menu showing completion is not the same as the look of it passing; window actions still have to be checked against the real target, and whether to unmute for anything involving sound is up to you.

## 1. Basic visuals and stability

Watch for 30 seconds after starting:

- The window should be about 220×220, and the transparent area should show no black backing or magenta edges.
- Both feet should touch the bottom of the work area above the taskbar, without sinking, floating or shaking violently.
- The top of her head, her hair and both feet must never be cropped.
- Nuonuo, when not fasting, shows `Hunger 0-100`, turning orange above 55 and red above 80; fasting hides the whole bar. Feibi never has a hunger bar and only shows the mischief bar as described in section 0.5.
- Idle frames should change slowly, with no teleporting or obvious dropped frames between actions.
- Standing idle should only blink naturally, with no left-right drift of the body; after a few rounds she should enter the original lying-down sleep animation at random, then get up normally.
- The mouse merely passing by should not trigger eye tracking or the body chasing, dodging, following or hugging; Nuonuo has no extra pupils, and Feibi keeps the original image's purple eyes and native pupil highlights.

### 1.1 The size slider

Right-click the current character or the tray icon and drag the size slider labelled with the current character's name:

1. Go from 100% to 60%, 140% and 200% in turn; the character should scale smoothly and immediately with no restart needed.
2. If she is standing on the taskbar or a window, her soles should stay in the same place before and after scaling, without popping up, sinking into the platform or jumping sideways.
3. The character's body, hit area, collision box and the current character's hunger or mischief bar should all stay usable as the window size changes, with the hair and feet never cropped; with bubbles off no text should appear, and with them on a bubble must not cover her face.
4. Quit and restart, and it should return to the last percentage set; `PetSize` in `settings.json` should be updated to match.
5. Choosing 200% on a small screen or a high-DPI monitor may let the program shrink temporarily for the current work area; moving back to a monitor with enough space should restore the configured size.

### 1.2 Dragging and flinging feel

1. Move her slowly a short distance with the left button and release: she should settle directly as if placed gently, and must not fly off for no reason.
2. Hold and fling horizontally at speed, releasing mid-movement: she should carry on flying in the fling's direction, decelerating gradually, rather than dropping vertically at the release point.
3. Fling quickly upward, diagonally up and diagonally down in turn: the trajectory should keep the corresponding two-dimensional direction, and an upward fling should pass an apex before falling.
4. Fling her towards the outside of the monitor: the sole anchor should bounce back softly before reaching an area with no monitor, and she must never be lost off screen permanently.
5. Dragging in mid-air uses the dedicated soft dangling animation with the head, hair and both feet complete; after release it should transition smoothly into the jump or fall and the bouncy landing.

The log should show either `DragReleasedGently` or `DragReleasedWithInertia`, the latter with `Velocity`, `Speed` and `SampleSeconds`, followed by `FallStarted` and `Landed`.

## 2. Window terrain and gravity

Open the test platform first:

```powershell
powershell -ExecutionPolicy Bypass -File .\tools\runtime_test_platform.ps1
```

Then test in order:

1. Drag Nuonuo to about 200-400 pixels above the platform and release.
2. She should accelerate downward gradually, play the falling animation, and land on the window's top edge.
3. After landing she should play one complete squash-and-rebound of about 1 second, with her feet finally against the window edge.
4. Move the platform window down by about 150 pixels. She should hold her height and separate naturally from the edge first, then fall under gravity to the platform's new position.
5. Move the platform sideways or close it. With no platform under her feet she should fall to the next window down or to the bottom of the desktop.
6. Stack two ordinary windows at different heights and release her from above; she should land on the upper window she passes first, rather than falling through to the lower one.

Then test ledge inertia:

7. With Nuonuo settled, move the platform slowly up and down; she should follow stably without shaking or jumping by herself.
8. Drag the platform upward quickly by about 120-250 pixels; she should rise with it, then separate from the edge and be tossed upward, playing a tight surprised weightless animation with both feet visible and inertia in her hair and sleeves, switching to falling after the apex. While airborne, the platform catching up with her soles should lift her again and must not pass through her; while the platform has not caught up, or is moving downward, she should keep her free trajectory and must not move in sync as though stuck to the edge.
9. Drag the platform downward quickly; her feet should separate briefly from the edge, then catch up and land on the platform's live position. She must not descend in sync as though stuck, pass through the platform, or fall to the desktop for no reason.
10. A quick upward move should log `PlatformLaunch`, `FallStarted`, `TossApexReached` and `Landed` in order; a downward move should log `PlatformDownwardRelease` and `FallStarted`, then `Landed` back on the same platform handle, with no `PlatformSupportLost`.

Then test window terrain behaviour:

11. Let her stand on an ordinary window's top edge and keep watching; when she chooses to jump autonomously she should play the separate squash, take-off and mid-air ball animation, forming a real parabola horizontally and vertically and landing on a collidable window or the desktop.
12. Keep an ordinary window open and watch for 2-5 minutes: on reaching an edge she should at random turn back, jump off, hug the edge and slide down, or deliberately jump to the level below; while sliding, both hands should stay against the real window's side and her body should descend continuously along it before she lets go and falls.
13. While she is standing on an ordinary window, suddenly click that window's maximize button: with the original top edge gone she should be flung up and outward off screen rather than hanging in place or sinking into the window; the log shows `WindowMaximizeBounce`.
14. With Nuonuo standing outside a window, drag another ordinary window quickly sideways into her from the left and from the right. She should fly off in the impact direction with a slight upward arc; the log shows `WindowSideImpact` with `Direction` matching the window's direction.
15. Drag Nuonuo inside an ordinary window that is large enough and release. She should land on the window's inner bottom edge; rocking that window side to side or up and down should keep her inertia and bounce her off the left, right, top and bottom walls, never sticking to the window. Dragging her out or closing the window should release the container.
16. This item tests Nuonuo only: high hunger inside a container should not send her off to lick an icon; deleting a dedicated file should release the container and send her out to eat. Feibi is unaffected by a deletion and produces no feeding task.

The container logs are `WindowEnclosureEntered`, `WindowEnclosureImpact` and `WindowEnclosureSettled` in order, with `WindowEnclosureReleased` when she is dragged out or the window closes. Deletion feeding inside the box shows `WindowEnclosureMealEscape`, and an icon lick request inside the box logs `IconLickSuppressedInEnclosure`.

The edge behaviour logs are `TerrainEdgeActionChosen`, `TerrainHopStarted`, `EdgeSlideStarted` / `EdgeSlideReleased` or `TerrainJumpDownStarted` in order, so a random non-selection can be told from a physics transition failing.

For multi-monitor testing, span the platform across two monitors, adjusting `Left` to your own screen arrangement:

```powershell
powershell -ExecutionPolicy Bypass -File .\tools\runtime_test_platform.ps1 -Left 2300 -Top 650 -Width 700 -Height 320
```

Put her on the part of the window on each screen in turn. She should settle on both, and the size, sole position and velocity should not change abruptly across screens. A maximized window, the taskbar, tooltips and hidden windows should never become standable platforms.

## 2.5 Mouse interaction boundaries

Turn quiet mode off and let Nuonuo and Feibi Jiubi idle and roam normally, watching for 3-5 minutes:

- parking the mouse nearby, circling slowly, or passing quickly should never trigger chasing, dodging, following or hugging the mouse;
- holding the left button on the character should still interrupt roaming and drag her, and the gentle placement, fling and toss after release are still accepted as in section 1.2;
- right-clicking the character should still open the menu, and a click on the transparent non-character area should still land on the application underneath.

## 3. Autonomous behaviour

Leave it alone for 3-5 minutes and watch the new life loop:

- After returning to idle, Nuonuo stays in place for at most about 5 seconds and Feibi about 4, then starts a new behaviour; she must not stay on the same frame for a long time for no reason.
- Once she starts walking slowly in an ordinary state she should keep walking: on reaching one side of the platform she turns back or heads for a climbing target directly, with no "walk a little, stop a few seconds, walk again" rhythm.
- With a climbable window available she walks to its left or right side, plays the alternating hands-and-feet climbing animation and ascends along the real side edge.
- A climb may succeed or slip; a success must land her accurately on the target window's top edge and continue roaming, and a failure must enter a real fall and landing, never teleporting back to the ground.
- Even at low sleepiness she naps at random occasionally, staying visible for at least a few seconds; the probability rises noticeably at higher sleepiness. She should go frame by frame from standing down to lying on her side, keeping the same lying pose and breathing slowly; waking reverses it to get up, and must not snap straight back to standing.
- Only Nuonuo chooses to lick an icon, beg or do a complete roll at high hunger. Feibi shows none of those food behaviours, and her autonomous mischief is accepted as in section 0.5.

Acceptable behaviour is "a continuous life, random but not mechanically repetitive". A walk has to move along the platform under her feet, and turn into a fall when that platform disappears.

For the climbing test you can keep section 2's test platform open and put Nuonuo back on the desktop floor of the same monitor. By default the platform's bottom edge is within her jump-and-grab range; watch for about 1-3 minutes. The log should show `ClimbTargetChosen` and `ClimbStarted`, followed by `ClimbSucceeded` or `ClimbSlipped`. On success, `Support` should become the platform window's handle.

Separately, Nuonuo should occasionally choose a visible icon, run to its right and lick it once, with about a 70-second cooldown after each lick. The real icon should not disappear, move or be renamed. Feibi never performs this behaviour.

To force a particular need for observation, exit the pet from the tray first and back up:

The example below is for checking Nuonuo's needs only. Feibi's sleep and exploration can be driven by changing her own `Sleepiness` and `Curiosity` after a separate backup; changing hunger or fullness should affect no behaviour and should not show a hunger bar. Test Feibi's mischief through the menu rather than editing a save to bypass a cooldown. Do not mix the two characters' backups.

```powershell
Copy-Item "$env:APPDATA\SoftMochiPet\life-state.json" "$env:APPDATA\SoftMochiPet\life-state.self-test-backup.json" -Force
notepad "$env:APPDATA\SoftMochiPet\life-state.json"
```

Change only the four numbers below and leave the other fields alone:

| Goal | Hunger | Fullness | Sleepiness | Curiosity |
|---|---:|---:|---:|---:|
| Hungry icon licking / roll | 100 | 5 | 10 | 10 |
| Sleepy napping | 5 | 20 | 100 | 10 |
| Curious exploration | 10 | 20 | 10 | 100 |
| Full resting | 5 | 100 | 20 | 10 |

Save, restart, and watch for up to 90 seconds. Behaviour has random weighting, so the first decision is not required to trigger it. Exit before restoring the backup.

```powershell
Copy-Item "$env:APPDATA\SoftMochiPet\life-state.self-test-backup.json" "$env:APPDATA\SoftMochiPet\life-state.json" -Force
```

## 4. High hunger and icon licking

Sections 4-6 apply to Nuonuo only; the inverse check for Feibi is no licking, no feeding and no fake icons.

Using the method in section 3, set `Hunger` to `100` and `Fullness` to no more than `8`, then start the pet. Make sure there is at least one fully visible desktop icon not covered by a window, and wait for her to choose icon licking autonomously:

- She should immediately choose a real desktop icon nearby, rather than heading for the mouse or a menu position.
- She should stand to the icon's right and play the roughly 1.8-second licking animation facing left, with the middle of the tongue extension touching the icon's right edge.
- The top of her head, her hair and both feet stay complete throughout; the tongue should be short, wide and rounded, and after retracting there should be puffed cheeks, a squash and a soft spring back, with no sudden collapse or violent frame jumping.
- After the animation the icon's position, image and name should all be completely unchanged; `Hunger` in the state should be about 100, `Fullness` no more than 8, and `TotalMeals` unchanged.
- If she is falling, landing, being dragged or eating a file when it triggers, she should wait until she is steady or has finished before going to lick.
- Hide the desktop icons temporarily or cover them completely with a window, and a high-hunger decision should report "cannot reach to lick", then stay where she is and play the complete hungry roll; she must not run to the old coordinates.

## 5. Single deletion animation

Create a dedicated file on the desktop:

```powershell
$desktop = [Environment]::GetFolderPath('Desktop')
Set-Content -LiteralPath (Join-Path $desktop 'FNN_TEST_FOOD_SINGLE.txt') -Value 'disposable test file'
```

Wait 2 seconds, then select that real icon on the desktop and press `Delete`:

- Once the real file disappears, the same icon and file name should be left immediately in exactly the same place.
- The fake icon must not move to the mouse menu or any other clicked position.
- Nuonuo should run to the icon's right and open her mouth wide facing left, not stand in the wrong direction.
- The icon and text should stay in place first, then shake, stretch, shrink and be drawn into her mouth.
- The whole swallow should be clear and on the slow side, and the fake icon should disappear only after it is inside her mouth.
- After swallowing she plays the satisfied animation, and in the state hunger drops while fullness and `TotalMeals` increase.

If the fake icon never appears, exit the pet first and confirm `ReactToDeletes` is `true` in `%APPDATA%\SoftMochiPet\settings.json`.

## 6. Consecutive deletions and the buffet

Create 5 temporary files:

```powershell
$desktop = [Environment]::GetFolderPath('Desktop')
1..5 | ForEach-Object {
    Set-Content -LiteralPath (Join-Path $desktop ("FNN_TEST_FOOD_{0:D2}.txt" -f $_)) -Value 'disposable test file'
}
```

Wait 2 seconds. Hold `Ctrl` and select only those 5 test icons (do not use `Ctrl+A`), then press `Delete` exactly once:

- As each real icon is deleted, its own fake icon and text should be left immediately in its own original position, and the five fake icons must not pile up at one point.
- The icons and file names should correspond to their original positions; a single `DesktopSelectionCaptured` in the log should list five different centre points, and the five following `DeletionQueued` entries should have different `QueueId` values and coordinates.
- While she eats the first one, the other fake icons must stay in their own positions and must not disappear and then suddenly reappear.
- A second quick deletion should produce the "buffet" reaction.
- She should go to each position in turn and slurp each one completely, never losing the order; the next is handled only after the previous one is finished.
- The satisfied animation between courses may be shortened, but the slurp animation must not be skipped.
- Finally all 5 fake icons disappear and `TotalMeals` should increase by 5.

Do not test with existing files. In the recycle bin, handle only these `FNN_TEST_FOOD_*` items, and do not empty the whole recycle bin just to clean up a test.

## 7. Character state and saving

The four needs and the feeding statistics checks below are for Nuonuo. For Feibi, separately check her own sleepiness, curiosity and mischief saving: hunger and fullness are zeroed and the meal count does not grow, and the mischief save holds no window handle or snapshot, does not increase offline and does not restore an old action. The official paths are in [characters and data boundaries](docs/CHARACTERS.md#characters-and-data-boundaries), and cross-character isolation is in section 0.4.

After exiting from the tray, look at:

```powershell
Get-Content "$env:APPDATA\SoftMochiPet\life-state.json"
Get-ChildItem "$env:APPDATA\SoftMochiPet" -Filter '*.tmp'
```

Check that:

- after a while awake: hunger, sleepiness and curiosity rise slowly while fullness falls;
- after eating a file: hunger falls, fullness rises, `TotalMeals` increases, and the extension is counted in `FoodTypeCounts`;
- after finishing exploring: curiosity falls;
- after sleep ends: sleepiness falls noticeably;
- all four values stay within 0-100;
- `life-state.json` reads normally and no `.tmp` file is left in the directory;
- after quitting for a while and starting again, the state evolves by an offline duration of at most 12 hours, without dying or becoming permanently invalid.

## The two-pet relay for deleted icons (test this yourself)

### v1.3.0 four daily interactions (awaiting user testing)

1. Exit the old process, start the new version, turn coexistence on and quiet mode off. Bring both home first, then drag them to a roomy spot at the bottom of the same monitor; do not leave one on a window ledge and one on the ground. Right-click "Two-pet interactions" and choose Follow along, Nuzzle in passing, Play with the ball and Watch together in turn.
2. Following: Feibi walks ahead, looks back partway, and Nuonuo follows a little later; at a screen edge Feibi should stop and wait rather than crossing it. Nuzzling: the left-right order should not swap, and after the cheeks come close they return to position with no cropping of the head or feet.
3. Ball: watch the complete hat removal and ball appearing, six passes back and forth, and the ball being put away and the hat worn again. After the second kick it must still be kicked rather than the ball simply flying; the ball changes direction only at the toe, never jumping to her feet out of nowhere or passing through the hat. Test with Feibi on the left and on the right, then at 60%/100%/200% body size and 100%/125% display scaling.
4. Watching together: open or clearly move an ordinary window on the screen they are on; with both idle and at least 18 seconds since the last interaction, watch them turn their heads. Feibi looks first and Nuonuo a little later, and the behaviour does not change the window's position or size. A maximized foreground window can also be watched, but all the windows already present at startup should not fire a burst of events.
5. Watch autonomously for 3-5 minutes; walking apart, meeting, being idle and a window change should each produce their own trigger, and the same behaviour should not repeat within seconds. In quiet mode none of the four should start; the existing priorities for feeding when hungry, napping when sleepy and the deletion relay are retained.
6. During the ball, in turn, drag one of them, delete a newly created temporary file, change the size, turn coexistence off, and trigger mischief deliberately; the action should end, the ball disappear, and both return to normal. Standing on the same wide window ledge, move or minimize that ledge and it should end safely without them floating or sliding away. When a manual scene is refused, report the `PairLifeUnavailable` reason and time from the log.

### v1.2.3 coexistence life and relay retest

1. Exit the old version completely, start the new official package, turn "Both pets together" on and quiet mode off, and leave both on the same monitor. The first invitation is attempted after about 12 seconds; ordinary walking can accept an invitation too, so you do not have to wait for both to stand still. It defers while one is busy attacking or being dragged, retrying every 2 seconds.
2. Without opening the menu, watch for two or three minutes: you should see an autonomous cheek pinch, food throw or nap, and after each ends they pause together for a moment before going their own ways. Napping takes priority at high sleepiness, and in normal mode feeding takes priority at high hunger. It is not a fixed interruption of the current action every few tens of seconds.
3. Create a recoverable test shortcut and delete it while Feibi is walking: the two should set off together, with Feibi kicking and Nuonuo catching with her mouth open. Repeat in the desktop's first column, at the right edge and at the bottom edge; while waiting and running over, the icon and text should stay in place, and only once they are in position and winding up should a slight slide be allowed.
4. Delete the test icon while Feibi is falling or asleep: it should wait up to 5 seconds and relay if she becomes ready, otherwise Nuonuo eats alone. Do not drag her over to force a relay. Select and delete 3 temporary files in a row and check they are handled in order, with no piled-up icons and nothing eaten twice.
5. With a test window containing no unsaved content already in Feibi's hat, check the two-pet interactions and the deletion relay again; the original window should stay in the hat until you deliberately return it. Right-clicking "Give the window back" or "Play a prank" during an interaction should end the two-pet scene safely first, and two timelines must never run at once.
6. Right-click from each of them in turn, swap which is primary, and retest; crossing monitors, turning coexistence off, changing the body size and interrupting with a drag must never leave anything stuck. When it falls back to solo, providing the `PairIconKickSkipped/Waiting/Ended` log for that period is enough to show why.

Turn on "Both pets together" and deletion responses, put a **recoverable test shortcut** in the middle of the desktop and delete it: Feibi should run to the right with the icon and text staying in place at first; the icon may drop slightly as she winds up, the text fades at the moment of the kick, the icon flies into Nuonuo's open mouth, and only then does she chew. Then put the test shortcut in the desktop's leftmost column and confirm the icon bounces off the left screen edge into Nuonuo's mouth on the right. Select three test shortcuts and delete them at once, confirming each icon appears exactly once, they are handled in order, and the hunger statistics increase exactly three times. Finally, try picking up either pet before the kick, and putting the two pets on different monitors; the former should restore the fake icon to its original position and hand it back to the queue, and the latter should use Nuonuo's original solo slurp. Do not test with files that matter.

If there is a problem, providing the `activity.log` section containing `PairIconKickStarted`, `PairIconKickImpact`, `PairIconKickEnded` or `PairIconKickSkipped` plus a screen recording is enough. This feature has so far only had background testing, and no foreground acceptance was done by starting the pet for you.

## 8. Information to provide when you find a problem

Please record:

1. which step above went wrong, which character, and whether you had just switched characters;
2. a screen recording covering the complete action, not just a single screenshot of the end;
3. the monitor resolution, the scale factors, and the primary and secondary screen arrangement;
4. the output of these commands:

```powershell
Get-Content "$env:LOCALAPPDATA\SoftMochiPet\activity.log" -Tail 80
Get-Content "$env:APPDATA\SoftMochiPet\life-state.json"
```

If the problem happened during a batch deletion or a ledge toss, widen the log range to 200 lines so it includes the selection snapshot and the whole rise and fall:

```powershell
Get-Content "$env:LOCALAPPDATA\SoftMochiPet\activity.log" -Tail 200
```

That makes it possible to tell an animation asset problem from a coordinate conversion, window terrain, deletion capture or state machine problem.
