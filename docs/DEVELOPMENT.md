# Development guide

## Maintaining the source and the release package

GitHub filters non-ASCII attachment file names, so release attachments use `JiuNuoDesktopPet-<version>-win-x64.zip`. Since the English rename, the local ZIP, the extracted directory, the product name and the launching EXE all use `JiuNuoDesktopPet` as well, so the attachment name and the package contents now agree; the attachment content is unaffected by the upload name either way.

The conditions for publishing the source and assets are set by [release readiness](RELEASE_READINESS.md) and [asset rights](../ASSET_RIGHTS.md). Once confirmed, the source and the licensed character runtime assets are committed to `main` in the chosen repository, and the portable ZIP is attached to a GitHub Release; the bundled .NET runtime is not committed to Git. Repository visibility does not change with a release. The original images, the generation source images, the final prompts, the chosen assets and the processing hashes are all retained; generation service addresses, configuration IDs, task state, plan copies and local credentials are not part of what gets committed. The final generation receipt keeps only the original image, prompt and output hashes, the model, the dimensions and the provenance fields, which is enough for offline re-slicing verification; derived `keyframes` are not committed and are produced with the existing processing tools. The project maintainer is responsible for checking that the source, the attachment checksums and the release tag all agree.

## Technology stack

- .NET 8
- WPF
- Win32 P/Invoke
- Windows Forms `NotifyIcon` (used only for the tray entry point and menu)
- PowerShell build and packaging scripts
- A native C x64 launcher for the portable edition

## Setting up the environment

Basic development needs only Windows 10/11 x64 and the .NET 8 SDK:

```powershell
dotnet --info
dotnet restore .\SoftMochiPet.sln
```

Producing a complete portable package also needs Python 3.12+ with Pillow (`python -m pip install Pillow==12.3.0`) for the pre-packaging privacy check. The launcher needs the C++ desktop development component (v143) from Visual Studio 2022 or Build Tools, or `LLVM_MINGW_ROOT` pointing at an LLVM-MinGW 20260616 UCRT x64 release directory. Tool provenance and the scope of maintenance are in [release readiness](RELEASE_READINESS.md). `DOTNET_EXE` and `PYTHON_EXE` can each point at a local tool path, and PATH is used when they are unset.

## Common commands

### Run

```powershell
.\run.ps1
```

`run.ps1` first builds the Release x64 configuration of the current source, then starts the matching EXE, so it will not accidentally start a stale release in `dist`. An official portable directory has to be started from its own root `JiuNuoDesktopPet.exe`.

### Build

```powershell
.\build.ps1
```

The equivalent core command is:

```powershell
dotnet build .\SoftMochiPet.sln -c Release -p:Platform=x64
```

### Test

```powershell
dotnet run --project .\tests\SoftMochiPet.LogicTests\SoftMochiPet.LogicTests.csproj -c Release
```

The test project is a headless test runner, suited to verifying deterministic state machines, physics and queue logic. WPF window interaction, mouse capture, Explorer and monitor switching all require the on-device checks in `SELF_TEST.md` as well.

### Publish as a directory

```powershell
.\publish.ps1
```

Outputs to `dist\JiuNuoDesktopPet` with `JiuNuoDesktopPet.exe` as the main program. This is a Windows x64 self-contained directory that includes the .NET runtime, so the target machine needs no preinstalled .NET.

### Produce the portable package

```powershell
.\package.ps1
```

The script will:

1. publish the self-contained WPF main program into the `Program` subdirectory;
2. compile `JiuNuoDesktopPet.exe`, the single normal entry point in the root;
3. copy the matching edition's instructions, the code licence, the asset rights record and the third-party notices;
4. produce both official artifacts — a folder and a ZIP — with no test tools included.

Output paths:

```text
dist\JiuNuoDesktopPet-Portable\
dist\JiuNuoDesktopPet-Portable.zip
```

Without the Visual C++ toolchain installed, the script reuses a cached launcher only when one exists for the selected mode and its timestamp is no older than the launcher source and the program assets. The feature-test entry point can still use a separate `FeatureTest` compilation intermediate directory in development builds, but it never enters the official package. A freshly cloned repository usually has none of those caches, so Visual C++ Build Tools or an explicit LLVM-MinGW configuration is required.

The product name, the released assembly and the launching entry point are all `JiuNuoDesktopPet`; the source project name and the existing data identifiers are unchanged. The compatibility boundary and the notes on startup after an upgrade are set by [official rename and compatibility with older versions](../README.md#official-rename-and-compatibility-with-older-versions).

## Code structure

| Directory | Responsibility |
| --- | --- |
| `src/SoftMochiPet/Core` | behaviour policies, drag velocity, window climbing, queues and pure logic |
| `src/SoftMochiPet/Interop` | Win32 constants, structs and P/Invoke declarations |
| `src/SoftMochiPet/Models` | settings, deletion events and long-term life state |
| `src/SoftMochiPet/Services` | monitor geometry, window terrain, file watching, logging, sound and the tray service |
| `src/SoftMochiPet/MainWindow.xaml.cs` | the WPF lifecycle, state machine orchestration and user interaction |
| `tests/SoftMochiPet.LogicTests` | headless regression tests |
| `launcher` | the outermost native entry point for the portable edition |

When adding behaviour that can be verified deterministically, prefer putting the calculation and the decision in `Core` or in a separate service and calling it from the window layer. That avoids binding all the logic to the WPF Dispatcher and makes it easier to run tests in a session with no desktop.

## Local data

| Data | Path |
| --- | --- |
| Settings | `%APPDATA%\SoftMochiPet\settings.json` |
| Life state | `%APPDATA%\SoftMochiPet\life-state.json` |
| Current log | `%LOCALAPPDATA%\SoftMochiPet\activity.log` |
| Previous log | `%LOCALAPPDATA%\SoftMochiPet\activity.previous.log` |

When debugging state migration, exit the application first, then back up or move the life-state file. Do not edit that file while the application is running; the shutdown sequence may overwrite manual changes.

## Reading the logs

`activity.log` is structured text with a timestamp, PID and event fields. A good order for locating a problem:

1. find the most recent `PetLoaded` and confirm it belongs to the current process;
2. check the monitor list, DPI, work area and pet size;
3. follow `StateTransition` to see the state and the intent behind each action;
4. for drag problems, check `DragStarted`, the release velocity and the landing events;
5. for deletion problems, check the watcher source, the position snapshot, `QueueId` and the eating-completed events;
6. finally, search for exceptions, capture failures or service-stopped records.

The current log sanitizes external paths and file names; older logs, external tool output and newly added fields still have to be checked by hand. Do not attach a full local log directly to a public issue.

## What should not be committed

```text
bin/
obj/
dist/
.codex-temp/
artifacts/
test_artifacts/
tmp/
```

Beyond that, do not commit local settings, life state, logs, IDE user configuration or test data containing personal paths.

## Release checks

`.github/workflows/verify.yml` runs the build, the headless tests, the privacy and credential scans, and the full packaging verification on Windows 2022; it does not upload or publish a release package. It uses [actions/checkout](https://github.com/actions/checkout) v6, [actions/setup-dotnet](https://github.com/actions/setup-dotnet) v5 and [actions/setup-python](https://github.com/actions/setup-python) v6 (all MIT), with the pinned commits in the workflow. The image tool is [Pillow 12.3.0](https://pypi.org/project/pillow/12.3.0/) (MIT-CMU) and the credential scan uses [Gitleaks 8.30.1](https://github.com/gitleaks/gitleaks/releases/tag/v8.30.1) (MIT, with the download checksum pinned in the workflow). These tools are unmodified locally and are not distributed with the pet. The licence texts keep their original bytes through `.gitattributes`, so that different Git newline settings cannot break the provenance hashes.

`package.ps1` stages the self-contained runtime directory and the ZIP in `dist/.package-<unique id>`, then calls `tools/verify_portable_package.ps1` to check the x64 GUI entry point, the launcher and main program versions, the bundled .NET/WPF, character frame completeness, the source asset hashes and the hash of every file in the ZIP. Only on success does it move the old official package into `dist/old-versions` and replace the official path. If compilation or verification fails, the original official package is left in place and the staging directory is kept for diagnosis. This flow serves only the current portable release, and the frame directory counts are maintained by the project maintainer alongside the character asset contract. The Behavior Control edition is updated only when its parameter is passed explicitly.

Before preparing a distributable version:

1. run the Release build and all the logic tests;
2. complete the offline checks silently in the background — real desktop, window and sound acceptance is performed by the user following `SELF_TEST.md`, with the scope of each verification recorded separately;
3. run `package.ps1` and extract into a brand-new directory;
4. deliver the root launcher and have the user start it themselves to confirm the real behaviour and feel;
5. check that the ZIP contains no logs, settings, life state or development machine paths;
6. if a code-signing certificate is available, sign the executables before compressing the ZIP;
7. record the version number, the commit SHA and the SHA-256 checksums.
