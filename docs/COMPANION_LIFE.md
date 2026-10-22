# Two-pet daily interactions

## Current delivery and exit conditions

2026-09-28, adopted: added "Follow along", "Nuzzle in passing", "Play with the ball" and "Watch together", reusing the existing two-window coordination, running, hat removal and interruption cleanup. Nuonuo follows her companion, not the mouse; the nuzzle is triggered by meeting at close range; the ball is drawn separately and synchronised with the kick frames; and watching together only observes changes in window position, never reading content or operating the target window. Quiet mode never initiates any of these four. Eating icons, dragging and deliberate window mischief all take priority.

Exit conditions: the four manual entry points and the autonomous triggers are wired up; six character sequences generated straight from the originals have passed frame cutting and anchor verification; background silent verification covers the trajectories, cooldowns, interruption cleanup and assets; and the offscreen previews and the official portable package are produced. The real desktop impression is tested by the user, and the application is not opened automatically.

This serves only this project's two characters, is maintained by this project, and adds no general behaviour framework. Everything reused comes from the project's existing code and original assets, with no new external runtime dependency. Generated images must not be used as input to a later model; the chosen source sheets and their hashes continue to live in pair-sprite-manifest.json.

## Triggers and behaviour already wired up

| Interaction | Autonomous conditions (must also pass the shared busy / same-screen / same-ground checks) | Behaviour and cooldown |
| --- | --- | --- |
| Follow along | Feibi is walking normally and moving away from Nuonuo, with a horizontal foot distance of 1.3-4 times the character window width | Nuonuo sets off 0.8 seconds later and Feibi pauses midway to look back; about 8.2 seconds. If Feibi is already at an edge she stops and waits rather than walking on the spot; separate 65-second cooldown |
| Nuzzle in passing | At least one of them is walking normally, at a distance of 0.3-1.05 times the width | Keeps their original left-right order: approach, a gentle nuzzle with eyes closed, then back into position; 3.6 seconds once in place, 80-second cooldown |
| Play with the ball | The regular invitation is due, neither is very sleepy or very hungry, there is enough space, and the ball cooldown has elapsed | Hat off and ball out, six passes, ball away and hat back on; about 20.1 seconds once in place; 100-second cooldown. When the ball is unavailable the original interactions can still be invited |
| Watch together | A new window appears on the same screen, or window positions shift by at least 64 DIP horizontally and vertically in total between samples; the event must be within 8 seconds | They look towards the event, Feibi reacting first and Nuonuo 0.55 seconds later; about 4.2 seconds; 45-second cooldown |

The four share a minimum 18-second gap between scenes, and the separate cooldowns above are counted from the start. Quiet mode never initiates them. When either character's sleepiness reaches 70, or Nuonuo's hunger reaches 55 (outside fasting and infinite modes), the existing sleep and feeding invitations take priority. The regular invitation interval stays at 30-55 seconds and the ball participates in that path, while following, meeting and watching together each evaluate their context once per second. Windows that already exist when observation starts do not count as new events. An ordinary window and a maximized foreground window can both become something to watch, but nothing here moves, minimizes or captures them.

The manual entry points are not subject to the autonomous cooldowns, but they still respect the constraints of character availability, the same monitor, the same height and continuous support. A scene ends when the window ledge moves, closes or is minimized; crossing screens, size changes, locking, quitting, turning off coexistence, dragging, a deletion task or manual mischief all go through the unified cleanup. The ball uses a single prop window that is transparent, passes the mouse through and never steals focus, and nothing is left behind after cleanup. Failure reasons are recorded in PairLifeUnavailable (different heights, support not shared, not enough space, no visible window), and no window title, screenshot or file content is recorded.

## Art and verification maintenance

Notice, nuzzle and ball each have a 16-frame sequence for both characters, 96 frames in total, with the source sheets in assets/pair_interactions/sheets. Each set calibrates a single scale factor against the same character's neutral idle face width and aligns the soles, with transparency cleanup being nothing more than a deterministic crop; all six current source sheets were generated directly from the original artwork by the built-in imagegen. Running, hat removal and hat wearing reuse the project's chosen assets. The gentle nuzzle and the ball kick use dedicated poses, and the ball is a prop drawn natively by this project, not a generated character image that was then edited.

The ball trajectory and the action clock are maintained by this project's CompanionBallTimeline, used only for runtime and offscreen verification. The toe markers are 512-canvas coordinates from frame 7 of the chosen pair_ball sheet: Nuonuo (404,392), Feibi (100,384), with the ball centre offset outward by one ball radius. The hat opening continues to use the final hat_open frame at (235,424). Character mirroring, body size and DPI all transform the markers together, and the window centre must not be used in place of the toe. When passes loop, the animation timeline of the same name is explicitly reset, so the second kick does not start stuck on the last frame of the first.

Background tests cover the following distance and frame rate, the event validity window, ball trajectory continuity at five scale steps in two directions, six genuine animation restarts, and repeated cancellation. tools/companion_life_preview.py produces four silent GIFs from real runtime frames; the ball one reads the official timeline and real player frame numbers exported by the test program's `--companion-preview`, rather than writing a separate simulated player. The previews land in `dist/双宠日常互动预览` — the path that tool creates — and do not enter the official package. The official version and the release hashes are maintained only in the current progress section of CHARACTERS.md, and the on-device acceptance checklist is in SELF_TEST.md.

## Action prompts

The prompts below are the literal generation inputs and are kept verbatim as a provenance record.

### notice_feibi

Original input: `assets/characters/feibijiubi/original.png`

Create ONE professional 4 by 4 spritesheet containing exactly 16 sequential full-body animation poses of the supplied original character, row-major. Transparent background, no grid, labels, text, objects, companions or shadows. All cells equal size with wide transparent gutters; every hat, hair, hand and foot fully inside its own cell. Fixed orthographic camera, same chibi proportions, crisp smooth outlines and original colors. Fixed world-space foot baseline, identical neutral body size across frames; movement comes from articulated poses, never camera zoom. Frame 0 and frame 15 neutral standing with two visible feet. Playful, cute and expressive but anatomically consistent, no creepy stretched faces. High resolution square sheet.
Feibijiubi: energetic adorable blonde, original giant white hat with black/blue band ALWAYS on head, purple glossy eyes, blue cross clip, white/black/blue outfit, black gloves, short striped socks and small black shoes. Main facing three-quarter towards screen LEFT. Preserve original identity.
16-pose noticing animation: 0 neutral; 1-3 head perks up and eyes become attentive; 4-6 turn gaze sideways toward companion while hand lifts a little; 7-9 rise onto toes and lean forward to peek; 10-12 small delighted response with hands near chest; 13-15 settle gently back to neutral. Standing, head upright, subtle bounce not size changes.

### notice_nuonuo

Original input: `assets/characters/nuonuo/original.png`

Create ONE professional 4 by 4 spritesheet containing exactly 16 sequential full-body animation poses of the supplied original character, row-major. Transparent background, no grid, labels, text, objects, companions or shadows. All cells equal size with wide transparent gutters; every hat, hair, hand and foot fully inside its own cell. Fixed orthographic camera, same chibi proportions, crisp smooth outlines and original colors. Fixed world-space foot baseline, identical neutral body size across frames; movement comes from articulated poses, never camera zoom. Frame 0 and frame 15 neutral standing with two visible feet. Playful, cute and expressive but anatomically consistent, no creepy stretched faces. High resolution square sheet.
Nuonuo: soft sleepy cute grey-blue long-haired mochi girl, half-lidded FLAT eyes split pink above pale blue below, NO pupils or dark circles, original red/white outfit and red bow, small body and two little black shoes below red skirt. Complete original upper-body reference to full-body chibi. Main facing slightly towards screen RIGHT. Preserve flat original eye design.
16-pose noticing animation: 0 neutral; 1-3 head perks up and eyes become attentive; 4-6 turn gaze sideways toward companion while hand lifts a little; 7-9 rise onto toes and lean forward to peek; 10-12 small delighted response with hands near chest; 13-15 settle gently back to neutral. Standing, head upright, subtle bounce not size changes.

### nuzzle_feibi

Original input: `assets/characters/feibijiubi/original.png`

Create ONE professional 4 by 4 spritesheet containing exactly 16 sequential full-body animation poses of the supplied original character, row-major. Transparent background, no grid, labels, text, objects, companions or shadows. All cells equal size with wide transparent gutters; every hat, hair, hand and foot fully inside its own cell. Fixed orthographic camera, same chibi proportions, crisp smooth outlines and original colors. Fixed world-space foot baseline, identical neutral body size across frames; movement comes from articulated poses, never camera zoom. Frame 0 and frame 15 neutral standing with two visible feet. Playful, cute and expressive but anatomically consistent, no creepy stretched faces. High resolution square sheet.
Feibijiubi: energetic adorable blonde, original giant white hat with black/blue band ALWAYS on head, purple glossy eyes, blue cross clip, white/black/blue outfit, black gloves, short striped socks and small black shoes. Main facing three-quarter towards screen LEFT. Preserve original identity.
16-pose affectionate shoulder/cheek nuzzle: 0 neutral; 1-3 tiny side step closer with shy smile; 4-6 lean shoulder and cheek toward partner offscreen; 7-9 eyes close and cheek presses softly, hands tucked; 10-12 two tender side-to-side rubbing poses; 13-15 spring gently upright. NO partner drawn. No distorted mouth or giant hands. Lean LEFT for Feibi, RIGHT for Nuonuo.

### nuzzle_nuonuo

Original input: `assets/characters/nuonuo/original.png`

Create ONE professional 4 by 4 spritesheet containing exactly 16 sequential full-body animation poses of the supplied original character, row-major. Transparent background, no grid, labels, text, objects, companions or shadows. All cells equal size with wide transparent gutters; every hat, hair, hand and foot fully inside its own cell. Fixed orthographic camera, same chibi proportions, crisp smooth outlines and original colors. Fixed world-space foot baseline, identical neutral body size across frames; movement comes from articulated poses, never camera zoom. Frame 0 and frame 15 neutral standing with two visible feet. Playful, cute and expressive but anatomically consistent, no creepy stretched faces. High resolution square sheet.
Nuonuo: soft sleepy cute grey-blue long-haired mochi girl, half-lidded FLAT eyes split pink above pale blue below, NO pupils or dark circles, original red/white outfit and red bow, small body and two little black shoes below red skirt. Complete original upper-body reference to full-body chibi. Main facing slightly towards screen RIGHT. Preserve flat original eye design.
16-pose affectionate shoulder/cheek nuzzle: 0 neutral; 1-3 tiny side step closer with shy smile; 4-6 lean shoulder and cheek toward partner offscreen; 7-9 eyes close and cheek presses softly, hands tucked; 10-12 two tender side-to-side rubbing poses; 13-15 spring gently upright. NO partner drawn. No distorted mouth or giant hands. Lean LEFT for Feibi, RIGHT for Nuonuo.

### ball_feibi

Original input: `assets/characters/feibijiubi/original.png`

Create ONE professional 4 by 4 spritesheet containing exactly 16 sequential full-body animation poses of the supplied original character, row-major. Transparent background, no grid, labels, text, objects, companions or shadows. All cells equal size with wide transparent gutters; every hat, hair, hand and foot fully inside its own cell. Fixed orthographic camera, same chibi proportions, crisp smooth outlines and original colors. Fixed world-space foot baseline, identical neutral body size across frames; movement comes from articulated poses, never camera zoom. Frame 0 and frame 15 neutral standing with two visible feet. Playful, cute and expressive but anatomically consistent, no creepy stretched faces. High resolution square sheet.
Feibijiubi: energetic adorable blonde, original giant white hat with black/blue band ALWAYS on head, purple glossy eyes, blue cross clip, white/black/blue outfit, black gloves, short striped socks and small black shoes. Main facing three-quarter towards screen LEFT. Preserve original identity.
16-pose playing with a SMALL BALL THAT IS NOT DRAWN: 0 neutral; 1-3 notice ball approaching feet and crouch slightly; 4-6 lift forward foot; 7-8 gently kick with crisp visible foot contact pose; 9-11 follow through with arms balancing, delighted eyes; 12-15 recoil into soft bounce then neutral. Feet stay low, grounded body, not running. NO ball or effects. Kick LEFT for Feibi, RIGHT for Nuonuo.

### ball_nuonuo

Original input: `assets/characters/nuonuo/original.png`

Create ONE professional 4 by 4 spritesheet containing exactly 16 sequential full-body animation poses of the supplied original character, row-major. Transparent background, no grid, labels, text, objects, companions or shadows. All cells equal size with wide transparent gutters; every hat, hair, hand and foot fully inside its own cell. Fixed orthographic camera, same chibi proportions, crisp smooth outlines and original colors. Fixed world-space foot baseline, identical neutral body size across frames; movement comes from articulated poses, never camera zoom. Frame 0 and frame 15 neutral standing with two visible feet. Playful, cute and expressive but anatomically consistent, no creepy stretched faces. High resolution square sheet.
Nuonuo: soft sleepy cute grey-blue long-haired mochi girl, half-lidded FLAT eyes split pink above pale blue below, NO pupils or dark circles, original red/white outfit and red bow, small body and two little black shoes below red skirt. Complete original upper-body reference to full-body chibi. Main facing slightly towards screen RIGHT. Preserve flat original eye design.
16-pose playing with a SMALL BALL THAT IS NOT DRAWN: 0 neutral; 1-3 notice ball approaching feet and crouch slightly; 4-6 lift forward foot; 7-8 gently kick with crisp visible foot contact pose; 9-11 follow through with arms balancing, delighted eyes; 12-15 recoil into soft bounce then neutral. Feet stay low, grounded body, not running. NO ball or effects. Kick LEFT for Feibi, RIGHT for Nuonuo.
