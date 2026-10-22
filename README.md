# JiuNuo Desktop Pet

A WPF desktop pet that lives on your Windows windows and taskbar: Nuonuo treats recycle bin files as food, and Feibi Jiubi treats windows as toys.

> [!NOTE]
> The official product name is JiuNuoDesktopPet, with two characters, Nuonuo and Feibi Jiubi. The official release is a Windows x64 self-contained portable directory that runs as soon as it is fully extracted; the on-device acceptance status is in [the character guide](docs/CHARACTERS.md#current-progress-and-phase-exit-conditions).

## Licence and assets

The code the maintainer holds rights to is under [PolyForm Noncommercial 1.0.0](LICENSE), which permits noncommercial use, modification and redistribution within its terms; commercial use is not granted. This project is positioned as "source published, noncommercial use". The scope is in [NOTICE](NOTICE).

The provenance of the characters and assets, the production contributions and the permissions still to be confirmed are in [ASSET_RIGHTS.md](ASSET_RIGHTS.md). The assets are not automatically covered by the code licence, and this project being public does not mean redistribution permission has been obtained from the upstream characters' owners or the artists.

## About the project

Nuonuo is not an ornament pinned to the corner of the screen. She treats the real desktop as the space she moves around in: she walks on the taskbar and on application windows, falls off ledges, gets knocked flying by a quickly moving window, can be put inside a window to rock and bounce between its four walls, and chooses her own behaviour based on hunger, sleepiness and curiosity.

When a file reaches the recycle bin the ordinary way, Nuonuo remembers where the original desktop item was, runs over and completes the feeding interaction. The whole process uses only file system events and the necessary system metadata; it never reads file content, and it never deletes a file on the user's behalf.

## Feature highlights

- **Character selection and coexistence**: Nuonuo and the livelier Feibi Jiubi can be switched individually, or both shown at once with "Both pets together". Their animations, recordings and progression saves stay separate, while size, mute, quiet mode and the optional speech bubbles (off by default) are shared. Assets and acceptance status are in [the character guide](docs/CHARACTERS.md).
- **Feibi's window mischief**: kicking, punching, charging and collecting windows in her hat, and once the mischief bar is full, throwing the hat to smash or tear a window snapshot and then putting the shards or the two halves into the hat. Windows are never restored automatically on a timer; the user returns them with "Give the window back" from the right-click menu. Attacks drop the extra crouch and reach at the end, and the finishers are slowed down so the wind-up, the destruction and the storing are all visible. The on-device acceptance status is in [the character guide](docs/CHARACTERS.md#current-progress-and-phase-exit-conditions). Feibi does not eat or lick icons and has no hunger bar.
- **Desktop physics**: the top edges of application windows, the taskbar and the monitor work area can all become terrain to walk or land on.
- **Solid window collisions**: the left and right edges of an ordinary window can knock Nuonuo flying, and putting her inside a window turns its four walls into a closed container she can rock around in.
- **Dragging with inertia**: a gentle placement, a horizontal fling, an upward toss and a diagonal fling each produce their own release velocity, gravity and soft bounce.
- **Autonomous life state**: Nuonuo maintains hunger, fullness, sleepiness and curiosity; Feibi maintains sleepiness, curiosity and her own mischief state, and accumulates no mischief while offline.
- **Adjustable character size**: the tray menu offers a live 60%-200% size slider, keeping the sole anchor and the collision proportions consistent while scaling.
- **Optional life modes**: Nuonuo can enable fasting mode, both characters share quiet mode and infinite mode, and Feibi's autonomous mischief is on by default and can be switched off in the menu.
- **System startup option**: optionally start when the current Windows user logs in, with no administrator rights required.
- **Recycle bin feeding**: identifies the position of a desktop item before deletion, and supports queuing consecutive deletions and handling them in batches.
- **Multi-monitor support**: supports monitors at negative coordinates, windows spanning screens, work area changes and Per-Monitor DPI.
- **Local first**: no network access at runtime, no uploading of logs or screenshots, no reading of file content and no deleting files on its own; window effect snapshots are held only briefly in memory.

The complete capabilities, behaviour boundaries and current limitations are in [the feature guide](docs/FEATURES.md).

## Requirements

| Scenario | Requirement |
| --- | --- |
| Running a packaged build | Windows 10/11 x64 |
| Running from source | Windows 10/11 x64, .NET 8 SDK |
| Producing a complete portable package | the above, plus Python/Pillow, and either the Visual Studio 2022 C++ component or an explicitly configured LLVM-MinGW; see the development guide |

The project targets Windows only. The transparent window, tray menu, desktop item location and window terrain all depend on Win32 and WPF, so macOS and Linux are not supported for now.

## Quick start

### Download the portable edition

The current version is **v1.3.3**, which adds the noncommercial code licence, the asset provenance notes and the third-party notices, pins the bundled .NET/Windows Desktop to 8.0.31, and strengthens the packaging checks. The character features are unchanged. The release decision, verification results and follow-up items are in [the release record](docs/RELEASE_READINESS.md).

Download the portable ZIP from [GitHub Releases](https://github.com/nicedoctors/JiuNuoDesktopPet/releases/latest), extract it in full, and run `JiuNuoDesktopPet.exe` from the root folder. Note that the published v1.3.3 archive predates the English rename, so inside that archive the entry point still carries its original Chinese name; a build from the current source produces `JiuNuoDesktopPet.exe`. The public source is in this repository with an independent history, and contains no old commits or PR references from the original private repository.

### Run from source

```powershell
git clone https://github.com/nicedoctors/JiuNuoDesktopPet.git
cd JiuNuoDesktopPet
.\run.ps1
```

You can also use the .NET CLI directly:

```powershell
dotnet run --project .\src\SoftMochiPet\SoftMochiPet.csproj -c Release
```

### Produce the portable package

```powershell
.\package.ps1
```

When it finishes it will have produced:

```text
dist\JiuNuoDesktopPet-Portable\
dist\JiuNuoDesktopPet-Portable.zip
```

Extract the ZIP in full and double-click `JiuNuoDesktopPet.exe` in the root folder. That is the only entry point in the root of an official package; the `Program` subdirectory holds what the program needs to run and must not be moved away from the launcher. The feature-test entry point is kept only in the source and in development builds, and is not released with the official package.

More build commands and packaging notes are in [the development guide](docs/DEVELOPMENT.md).

### Feibi's standalone feature demonstration

The feature-test entry point is for development builds only and is not included in the official portable package. To run it, build the test launcher from source or use a separate development test directory; the official package provides only the everyday pet entry point.

## Basic controls

| Action | Effect |
| --- | --- |
| Hold the left button on Nuonuo and move | Drags her; autonomous roaming pauses immediately |
| Release the left button slowly | Sets her down gently, after which she falls to the nearest usable terrain |
| Release the left button quickly | Flings or tosses her according to the gesture's final velocity |
| Put Nuonuo inside an ordinary window | Enters a closed container, colliding and bouncing off the window's four walls; a deletion event makes her run out to eat |
| Move an ordinary window sideways into her | Sends her flying in the window's impact direction |
| Right-click Nuonuo | Opens the same menu as the tray entry point |
| Menu, "Pet character" | Choose Nuonuo or Feibi Jiubi; a switch is deferred while dragging or eating |
| Menu, "Both pets together" | Shows both at once; clicking again returns to single mode, and choosing "Pet character" also switches back to single mode |
| Menu, "Infinite mode" | Hides both characters' bars and pauses Nuonuo's hunger behaviour; Feibi still makes mischief on her usual rhythm |
| Drag the character size slider in the menu | Scales live between 60% and 200% and saves automatically |
| Tray menu, "Start with Windows" | Sets or clears starting when the current Windows user logs in |
| Nuonuo's menu, "Fasting mode" | Hides the hunger bar, zeroes hunger, and stops begging, icon licking and the hungry roll; Feibi does not have this item |
| Feibi's menu, "Allow autonomous mischief" | A persistent toggle, on by default. Settings at version 4 or older get it enabled once on upgrade, after which a manual choice is preserved. Both autonomous and manual modes may act on any compatible non-maximized, non-fullscreen foreground window, not just ordinary ones |
| Feibi's menu, "Play a prank" / "Tease her" | Try the window behaviour deliberately / raise the mischief bar, for use once the complete assets are ready |
| Feibi's menu, "Give the window back" | Deliberately restores a window held in the hat, smashed or torn; nothing is returned automatically on a timer |
| Tray menu, "Quiet mode" | Stops autonomous walking, eating and mischief, leaving only idling or sleeping; an already stored window is kept and can still be restored by right-clicking |
| Double-click the tray entry | Brings Nuonuo back to the currently visible work area |
| Tray menu, "Mute" | Immediately switches the character voices off or back on |
| Tray menu, "Exit" | Saves the life state and quits |

Once an ordinary delete puts something in the recycle bin, Nuonuo goes to eat it whenever the original desktop position was captured. A watcher failure never affects the file itself, and never intercepts or changes the system's deletion process.

While a window is held, quiet mode, going home, size changes and dragging across screens do not restore the target by themselves. Switching characters, quitting, locking, sleeping, or a real display layout or work area change all return it safely so no window is left behind; that is different from an everyday timed automatic restore.

## Development and testing

```powershell
# Release build
.\build.ps1

# Run the headless logic tests
dotnet run --project .\tests\SoftMochiPet.LogicTests\SoftMochiPet.LogicTests.csproj -c Release

# Produce a self-contained Windows x64 directory build
.\publish.ps1

```

The logic tests cover physics, size, roaming, Nuonuo's feeding, character isolation, mischief cooldowns and animation events; the production asset checks genuinely decode every required frame for each character and verify the real contact points, the hat masks and the hashes. The current acceptance results are in [character progress](docs/CHARACTERS.md#current-progress-and-phase-exit-conditions), and anything involving external windows, Explorer, visuals and mixed DPI is verified by the user following [the manual checklist](SELF_TEST.md).

## Project structure

```text
JiuNuoDesktopPet/
├─ src/SoftMochiPet/              the WPF main program
│  ├─ Core/                       state machine, physics and behaviour policies
│  ├─ Interop/                    Win32 interop declarations
│  ├─ Models/                     settings and life state
│  └─ Services/                   desktop, logging, sound and file watching services
├─ tests/SoftMochiPet.LogicTests/ headless logic tests
├─ launcher/                      the native launcher for the portable package
├─ packaging/                     the portable edition instruction files
├─ docs/                          feature and development documentation
└─ *.ps1                          build, run and release scripts
```

## Data, logs and privacy

JiuNuoDesktopPet saves runtime data only under the current Windows user's directories:

| Content | Path |
| --- | --- |
| Settings | `%APPDATA%\SoftMochiPet\settings.json` |
| Life state | `%APPDATA%\SoftMochiPet\life-state.json` |
| Feibi's character data | See [characters and data boundaries](docs/CHARACTERS.md#characters-and-data-boundaries); her life and mischief state are saved separately |
| Diagnostic log | `%LOCALAPPDATA%\SoftMochiPet\activity.log` |
| Previous rotated log | `%LOCALAPPDATA%\SoftMochiPet\activity.previous.log` |

"Start with Windows" uses the current user's `HKCU\Software\Microsoft\Windows\CurrentVersion\Run` entry, and turning the option off removes it.

### Official rename and compatibility with older versions

The product name, the window and tray titles and the released EXE and DLL names are all `JiuNuoDesktopPet`; the characters are still called Nuonuo and Feibi Jiubi. So that existing settings and both characters' saves keep loading, the `SoftMochiPet` data and log directories, the internal namespaces, the single-instance mutex and activation events, and the Windows Run value name are all retained. No second branded save or startup entry is created, and the original images, recordings and character IDs are not renamed.

When upgrading, exit the old version first, then extract the new one into a new folder; do not replace only the EXE, and do not mix old and new `Program` contents. An older version's start-with-Windows command is never modified silently; to have the new version start automatically, tick "Start with Windows" again in the new version's menu and the existing Run value will be updated to the new entry point. The repository address and the source project file names are unchanged, which does not mean the released files still use the old branding.

The log rotates once it reaches 8 MB, and records behaviour state, window terrain, monitor parameters and event correlation IDs. Since v1.3.2 it writes no complete file paths, file display names or window titles; for errors it keeps the type, the error code and the call site, and does not store the original exception message. Older logs, screenshots and screen recordings still need checking by hand before publication. Public credits, asset metadata and the historical processing rules are in [privacy and the public boundary](docs/PRIVACY.md).

## Known limitations

- The current release package has no Authenticode signature, so Windows may show "unknown publisher".
- Locating desktop items depends on the current implementation of Windows Explorer; restarting Explorer, a third-party desktop management tool, or a non-standard deletion flow can mean the feeding interaction never triggers.
- Mixed DPI, monitor hot-plug and unusual taskbar layouts are supported but remain the main areas to test.
- The repository offers no automatic updater and never checks for versions in the background.

When reporting a problem, please include your Windows version, monitor layout and scale factors, the steps to reproduce, and a sanitized `activity.log`. The detailed format is in [the contributing guide](CONTRIBUTING.md).

## Contributing

Bug reports, reproduction logs and suggestions for improvement are all welcome. Please read [CONTRIBUTING.md](CONTRIBUTING.md) before submitting code, and make sure the logic tests pass; do not commit `dist`, `bin`, `obj`, temporary logs or data containing personal paths.

## Licence

The code licence and the scope of the assets are in [licence and assets](#licence-and-assets) on this page. When distributing, [LICENSE](LICENSE), [NOTICE](NOTICE) and the applicable third-party notices must all be retained; the asset permission status is in [ASSET_RIGHTS.md](ASSET_RIGHTS.md).
