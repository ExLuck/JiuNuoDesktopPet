# Contributing guide

The current noncommercial licence, the asset rights and the state of public-release preparation are in [release readiness](docs/RELEASE_READINESS.md). Before contributing, confirm you hold the relevant rights and agree that your code is distributed under this project's licence; do not submit private credentials, raw logs or assets you have no permission for.

Thank you for your interest in the pet. The project depends heavily on the Windows desktop environment, and clear reproduction conditions usually help locate a problem far more than "dragging doesn't work".

## Reporting bugs

When opening an issue, please include as much of the following as you can:

- the Windows version and system architecture;
- the number of monitors, how they are arranged, their resolutions and scale factors;
- which side the taskbar is on and whether it auto-hides;
- repeatable steps, the expected result and the actual result;
- roughly when the problem happened;
- a sanitized `%LOCALAPPDATA%\SoftMochiPet\activity.log`.

Since v1.3.2 the paths, display names and window titles in the log use session-scoped correlation IDs and raw exception messages are not recorded; an older log may still contain personal content. Logs, screenshots and screen recordings should still be checked before being uploaded publicly, especially for third-party window content. The rules are in [privacy and the public boundary](docs/PRIVACY.md).

## Submitting code

1. Create a single-purpose branch from the latest `main`.
2. Keep the scope of a change clear, and do not mix in build artifacts or unrelated files.
3. Add regression tests for logic that can be verified deterministically.
4. Run the Release build and the logic tests.
5. For changes involving windows, the mouse, monitors or Explorer, complete the relevant on-device steps in `SELF_TEST.md`.
6. In the pull request, explain the reason, the approach, the environment you verified in, and the remaining risks.

Recommended commands:

```powershell
.\build.ps1
dotnet run --project .\tests\SoftMochiPet.LogicTests\SoftMochiPet.LogicTests.csproj -c Release
```

## Code conventions

- Keep nullable reference type checking enabled.
- Prefer putting pure computation in `Core` or in a testable service.
- Distinguish physical pixels from WPF DIPs explicitly in Win32 coordinates.
- Add structured diagnostic events when adding a state transition.
- Do not silently swallow an exception that affects what the user is doing.
- Do not read file content, and do not add network access or telemetry that has not been explicitly documented.

## Do not commit

- `bin`, `obj`, `dist` and temporary test directories.
- Local settings, life state and logs.
- Test samples containing personal paths, user names or private data.
- Third-party content whose permissions are unconfirmed.
- IDE user configuration that is only valid on your own machine.

## Licence notes

The code is under [PolyForm Noncommercial 1.0.0](LICENSE), with the scope in [NOTICE](NOTICE). When contributing you should hold the relevant rights and agree that your contributed code is distributed under this project's licence; third-party content has to keep its original licence and provenance. The character assets are covered separately in [ASSET_RIGHTS.md](ASSET_RIGHTS.md).
