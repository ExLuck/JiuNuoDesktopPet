# Release record and follow-up items

This file is where the v1.3.3 release record, the long-term decisions and the follow-up items are kept together. Character feature acceptance is still maintained in `CHARACTERS.md`, and asset rights are maintained solely in [ASSET_RIGHTS.md](../ASSET_RIGHTS.md).

## Adopted decisions

- The source is published under PolyForm Noncommercial 1.0.0, to meet the maintainer's requirement that commercial use be prohibited. Third-party code keeps its original licences and the character assets are managed separately; no claim is made that this meets the OSI definition of open source.
- The public repository is [nicedoctors/JiuNuoDesktopPet](https://github.com/nicedoctors/JiuNuoDesktopPet), created as an independent Git root history from a snapshot of the current source. The original `NuonuoDesktopPet` repository stays private, and its old PR references and hosted caches do not carry over into the new one.
- On 2026-10-01, having understood that the asset licensing is still unconfirmed and that the original repository's history carries risk, the maintainer explicitly asked for it to be uploaded and made public. The source and the v1.3.3 portable package are released on that basis. That decision does not supply the missing third-party asset permissions, and it does not change the boundary between managing the code and managing the assets.
- The .NET and Windows Desktop runtimes for the self-contained package are pinned to 8.0.31, with the authoritative value kept in `Directory.Build.props`. That version's licence text and source hashes are distributed with the package. Upgrading the runtime requires updating the notices and the verification record at the same time.
- The launcher prefers the v143 toolchain from Visual Studio 2022. Where that toolchain is absent, `LLVM_MINGW_ROOT` can be set explicitly to use LLVM-MinGW; the verified release is 20260616 UCRT x64. That fallback path serves only the current native launcher, and its compile flags and distribution notices are maintained by the project maintainer.
- The build scripts can be pointed at local tools with `DOTNET_EXE`, and the privacy check with `PYTHON_EXE`. Both default to PATH and depend on no private directory of the maintainer's. The Python image check requires Pillow.
- Asset privacy and notice integrity are checked before packaging, and the version, bundled runtime, licences, source assets and ZIP hashes are checked afterwards. The automated checks only run headless background tests; they never start the pet or play sound.

## Verification and current delivery

Local verification results from 2026-10-01:

- Built Release with .NET SDK 8.0.201: 0 warnings, 0 errors, and 217 headless logic tests passed. The CI configuration uses 8.0.425, and continuous check results are at [GitHub Actions](https://github.com/nicedoctors/JiuNuoDesktopPet/actions).
- 7 Python privacy and licence-integrity tests passed, including compatibility with temp-directory path normalization. No residual descriptive metadata was found in the source images or audio, and the text scan found no private maintainer information and none of the credential patterns covered.
- The ordinary portable package contains 1,402 files, of which 1,128 runtime asset files each hash-match the source. The launcher and the main program are both version 1.3.3.0, both bundled frameworks are 8.0.31, and the ZIP matches the extracted directory file by file.
- Two deliberately broken packages — one missing licences, one carrying an old runtime — were both rejected by the verifier. The launcher is compiled with LLVM-MinGW and its import table depends only on Windows system components.
- Gitleaks 8.30.1 found no credentials when scanning the staging directory. The independent Git root history holds only this complete source snapshot, with no history objects or PR references from the original repository. A scan can only cover its existing rules and the content it can reach, so it is no absolute guarantee against any secret.

The source is in this repository and the portable package is distributed through the [v1.3.3 Release](https://github.com/nicedoctors/JiuNuoDesktopPet/releases/tag/v1.3.3); the local packaging path was `dist/啾糯桌宠-免安装版.zip`. The package ships with this file and the asset rights record, and the attachment SHA-256 values are recorded in the release notes and the checksum file.

## Follow-up items

A 2026-10-05 review found 11 historical desktop test screenshots, along with their crops and composites, mixed into the source; those files do not belong in the portable package. They were deleted and all 3 commits of the public repository were rewritten, with both `main` and the v1.3.3 tag checked to confirm the images are gone. Both references were updated atomically using old-value comparison through the GitHub API. The rules for checking media content and for where test screenshots are kept are maintained in [Privacy and the public boundary](PRIVACY.md). Image links on the current branch now return 404, but the image objects remain reachable through the old commit IDs and need further cleanup from GitHub Support; no support request has been filed yet. The repository was checked and has no PRs, forks or Actions attachments. The 7 Python checks pass, and a privacy scan of the current files found none of the issues it covers. The portable package checksums are unchanged.

1. Complete the licensing review in the [asset rights record](../ASSET_RIGHTS.md). Crediting a source, producing the frame sequences oneself and prohibiting commercial use are none of them a substitute for upstream permission.
2. The original repository stays private. If that address is ever made public, the old PR references, caches and objects that an ordinary push cannot remove should be handled through GitHub Support first, and the externally visible content reviewed again afterwards. No message has been sent to GitHub Support or to the artists.
3. The user accepts the real desktop interaction following `SELF_TEST.md`. This round of maintenance does not change character behaviour, and automated verification is not a substitute for human visual and audio acceptance.
4. .NET 8 reaches end of support on 2026-11-10, and the maintainer has to plan a migration to a supported runtime before then. Source: [Microsoft support policy](https://dotnet.microsoft.com/en-us/platform/support/policy).

Acceptance conditions for this release: the standalone source, licence and documentation are consistent; the background tests and the full packaging verification pass; the remote source and attachment checksums match; and the original repository stays private. The asset licensing review stays in the follow-up items and is not marked as complete.
