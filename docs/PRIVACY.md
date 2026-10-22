# Privacy and the public boundary

The current noncommercial licence, the asset rights and the state of public-release preparation are in [release readiness](RELEASE_READINESS.md). Before contributing, confirm you hold the relevant rights and agree that your code is distributed under this project's licence; do not submit private credentials, raw logs or assets you have no permission for.

## Adopted rules

2026-09-30, applying to the source, the production records, the runtime assets and the official release packages: this project's maintainer is credited publicly only as **桃天帝不差 / nicedoctors**. Commits use a GitHub noreply address and no private email is published; that address is not a contact address. Third-party licences, author credits and provenance statements are never removed by a maintainer privacy cleanup.

Private disk locations, account credentials, debug logs and user configuration are not public material. Asset records use relative paths and checksums. Public images have their descriptions, generation-source wrappers and comments removed while pixels, transparency, colour and animation data are preserved; recordings have descriptive container fields removed while the PCM, duration and original loudness compensation are preserved. Original material that may need restoring stays in a restricted backup outside the project and must not be uploaded or placed in a shared package.

The released program version number does not automatically carry the local Git commit ID, so build artifacts do not expose history object identifiers. The source corresponding to a version is maintained through the Release tags.

Paths, file display names and window titles in logs are replaced with session-scoped HMAC correlation IDs, and the random key is not persisted. Events, state, window handles, coordinates, DPI, error types, error codes and call sites without source paths are retained, so troubleshooting still works. Raw exception messages are not written to the log. This rule does not alter the user settings needed to run, nor the user's real file names.

## Verification and maintenance

From 2026-10-05, test screenshots of a real desktop, along with their crops and composites, do not enter the public source or the release material; they all go to the ignored `test_artifacts/` directory. The paths of the removed historical screenshots were added to the ignore rules at the same time. Public demonstrations use an isolated test background, and the visible content of every added or modified piece of media is checked by hand before release, with animations checked across the whole animation. Metadata and text scanning is no substitute for that check.

`python -X utf8 tools/public_privacy.py` checks every Git-managed, non-ignored file that is about to be made public. The report lists only relative file names and issue categories, and never echoes the matched content. The tool is a release check for the media formats this project already has, not a general secret detector, and it cannot recognise the content of an image, the content of a recording, or every form of credential.

`python -X utf8 -m unittest discover -s tests -p test_public_privacy.py` checks that metadata removal does not change pixels, transparency or audio data. Production asset cleanup compares decoded pixels or audio data image by image and updates the corresponding file hashes; the programs involved are maintained by the project maintainer.

History rewriting uses [git-filter-repo](https://github.com/newren/git-filter-repo) 2.47.0 under its MIT licence; the tool is used as-is and is not distributed with the pet. The process follows [GitHub's guidance on removing sensitive data](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/removing-sensitive-data-from-a-repository). Back up before rewriting, process all public branches and tags, and use lease checks with specified old values when updating the remote, to avoid overwriting a parallel update.

A rewrite cannot recall someone else's clone, an already downloaded file, a third-party cache or a platform's internal copy. Existing collaborators should re-clone, and must not merge or push the old history. Where a server's old commit caches and read-only references are still reachable, the repository owner needs to contact GitHub Support about them; no absolute anonymity is promised on that basis.

## 2026-09-30 cleanup record

The record below covers only the branches, tags and release packages handled at the time. A later review found that old PR references and hosted caches can still retain pre-rewrite objects, so it cannot be taken as establishing that the original repository is safe to make public. The current decisions and progress are in [release readiness](RELEASE_READINESS.md).

- Metadata cleanup of the current source and cover output is complete: 77 images and 46 recordings, each compared by decoded pixels or audio data and found unchanged, with file hashes updated to match.
- A basic scan of the files awaiting publication matched none of the personal paths, private emails or common credential patterns checked for.
- A zero-warning build, 217 headless logic regressions and 2 metadata tests all completed; the pet was not started and no sound was played.
- At the user's authorization, the history of all remote branches and existing tags was rewritten and the remote references were checked. The maintainer's commit credit is `nicedoctors` with a GitHub noreply address. The repository remained private and no visibility change was made.
- The [v1.3.2 release package](https://github.com/nicedoctors/NuonuoDesktopPet/releases/tag/v1.3.2) was published as Latest, with the server-side size and SHA-256 matching the local copy. The ZIP is 175,374,749 bytes with SHA-256 `e6473c2cf88bba2f87efa127a2e420fc9743329b9f65edfb0035ffe8b49ccdaa`.
- The old v1.0.0 and v1.3.1 download attachments were withdrawn after the restricted backup was verified, with the release notes kept and pointed at the new package. The Behavior Control edition is not updated. Old local packages, historical production caches and old Git metadata have been moved to a restricted backup outside the project and are not distributed with the repository or the new package. The project's `dist` keeps only the current official directory and ZIP.
- For the repository owner to handle: on 2026-09-30 it was confirmed that old commits are still reachable on GitHub through their original commit IDs. Rewriting a branch cannot delete the old objects the platform retains; a sensitive-data cache cleanup has to be requested from GitHub Support, and whether it is accepted and how much it clears is for the platform to decide. This rewrite must not be treated as having physically erased every copy on the platform.
