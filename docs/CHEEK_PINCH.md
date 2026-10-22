# Pinching cheeks with the mouse

Status, version and outstanding measurements are all kept in [current progress](CHARACTERS.md#current-progress-and-phase-exit-conditions). The two-pet "Feibi pinches Nuonuo" interaction is out of scope for this rework.

## Adopted behaviour

2026-09-29: both characters bind a continuous cheek outline, eye compression and a narrowed-eye expression, a brief open mouth and teeth, to a fixed idle frame 0. Once the left button hits a cheek, the cheek stretches with the mouse displacement and can also be pulled up or down. A maximum horizontal gesture of 105 source pixels maps to 64 pixels of outline extension, and a vertical gesture of 42 maps to 26 pixels of outline displacement. The skin gradient, the rounded grip point and the blush all follow the stretched outline. Holding a large outward pull for about 0.28 seconds starts a periodic open-and-close of the mouth, and the body tilts slightly around its original standing position (at most 3.5°) without changing the window size or the overall scale.

Releasing springs back from the current deformation; it does not switch to a second full-body animation and does not cross-fade two bodies. The spring converges within bounds at 30, 60 and 120 Hz. The original pixels of the head, hat, clothes and feet are preserved, face drawing is confined to a fixed canvas, and an offscreen bitmap is reused at runtime. Each cheek has its own anchor, so the character is not mirrored wholesale just because the right cheek was chosen.

The reference was a "sailor-uniform Yumi" video the user supplied, from which we took the sideways cheek stretch, the squint under tension, the sustained open mouth and the rebound timing. The implementation is a local vector rig over this project's fixed artwork, not the reference game's underlying implementation or arm skeleton; **it cannot be described as a one-to-one reproduction**, and how it actually feels and how cute it looks are for the user to accept. Because the effect is driven by continuous parameters, no new AI frame sequence was added, and no previously generated image was fed back into an image model.

## Interaction and maintenance

- A left-button hit on a cheek can interrupt idling, curiosity, sleep or ordinary running. The body and hat still follow the original drag flow, and a feeding target returns to the queue first. Mischief, climbing, falling and similar actions do not yield to it.
- Displacement is taken in window-local coordinates and converted for the current facing, so it does not drift with feedback from the face and body tilt. The gesture is released on loss of mouse capture, on a menu, on switching, on going home, and on size or display environment changes.
- `CheekPinchDynamics` maintains the spring and the mouth rhythm; `CheekPinchFrameRenderer` maintains the outline, expression masks and reuse cache for both pieces of artwork; and `MainWindow.CheekPinch.cs` handles the mouse and body pose. Maintenance is this project's responsibility, with no general animation framework and no new dependency.
- The base images come from each character's `runtime/idle/frame_00.png`. The old `runtime/pinch` and `pinch_right` sets and their source assets stay in the source tree and do not go into the release package. Later AI artwork still has to be generated completely afresh from each character's `original.png`, and a generated sheet must not be used as input.

## Acceptance evidence

Background checks cover hits on both sides, the body drag boundary, rebound at 30/60/120 Hz, canvas and non-face pixels being preserved, cache reuse, and all runtime assets. The preview is output by the actual C# renderer: [pull, hold, release](media/pinch-continuity-v131.gif). It shows the face drawing only; it does not simulate real input or desktop windows, and it did not start the pet, record the screen or play any sound.

Accept both characters' left and right cheeks, light and long pulls, vertical dragging, release, reversed facing and different DPI settings following [SELF_TEST.md](../SELF_TEST.md#v131-four-fixes-awaiting-user-testing).
