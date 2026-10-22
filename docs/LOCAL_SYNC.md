# Local version sync record

This page preserves the version and release paths as they were at the time of the sync. For the current release name, paths and compatibility decisions for "JiuNuo Desktop Pet", see [the official rename](../README.md#official-rename-and-compatibility-with-older-versions).

## 2026-09-07

- Source: the `main` branch of the GitHub repository `nicedoctors/NuonuoDesktopPet`.
- Version: `df33a256da1175ab76db15501a3669f1bb60f6ab` (the commit after the privacy cleanup), committed 2026-09-05.
- Decision and scope: the local source and the Windows x64 release artifacts are based on that remote version. Local `main` tracks `origin/main`, and the remote fetch rules cover all branches.
- Rationale: that version provides "Fasting mode (never hungry)", which hides the hunger bar and holds hunger at 0 when enabled, and it also includes the size slider, quiet mode and the new idle artwork.
- How local modifications were preserved: tracked modifications and untracked files from before the sync were archived to a restricted private backup outside the project, and are not distributed with the public history. The current source uses the remote implementation; the backup is there for the maintainer to consult as needed.
- Release scope: `dist/糯糯桌宠-免安装版`, the matching ZIP, and `dist/糯糯桌宠` as used by `run.ps1`.
- Verification status: the Release build passed, and 65 headless logic tests passed, covering fasting mode and the hunger display policy. Desktop interaction is accepted manually by the user following `SELF_TEST.md`.
- Maintenance: later syncs are checked against the remote commit and the target feature by the project maintainer, who also updates the version rationale in this record.
