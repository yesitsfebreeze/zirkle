# CHANGELOG

Decided by: user, 2026-07-31 — overlay font switches to Cardo

- 2026-07-31: font stack Monda → Cardo (verified HTTP 200, serves 400/700 + 400 italic); fallbacks changed sans → Georgia/Times/serif to match. Wordmark letter-spacing -0.015em → 0: the negative tracking was chosen for a sans display face and collides serifs at that size. Font lineage is now TT Bluescreens → Barlow Condensed → Monda → Cardo.

Decided by: user, 2026-07-31 — overlay reduced to the wordmark plus a very small soon™ beneath it, all glow removed

- 2026-07-31: dropped the "We come, full" prefix line entirely. soon™ moves from a trailing absolute superscript to its own centred line below the mark (p.line3, clamp 0.6–0.78rem, 0.4em tracking with a matching negative right margin for optical centring). .stack becomes a plain flex column — nothing is out of flow now, so the pair centres as one block and the absolute-positioning trick from a7dfe56 is no longer needed.
- 2026-07-31: removed BOTH glow drivers. The CSS --glow text-shadow added in fcf9362, and an older inline `overlay.style.textShadow` written every frame from closeness+vm that predated it. The second one is why fcf9362's "gap closed" claim was wrong: a distance-driven glow already existed, but a grep for the CSS spelling `text-shadow` never matched the JS property `textShadow`. Both gone; the now-unused `overlay` handle deleted too.

Decided by: user, 2026-07-31 — bezier removed entirely; links are a straight chord between two live dots, re-solved every frame, at 0.35 opacity

- 2026-07-31: dropped the quadratic bezier, its elbow control point, the BEZ_SEG=12 tessellation loop and the clearR center-avoidance bow. Orb position is now `lerp(A, B, p)` and the line is one segment endpoint-to-endpoint. Because both endpoints are live dots the sim keeps moving, the chord is re-solved each frame and the orb's traced path through space still comes out curved — the curvature now emerges from the endpoints rather than from a control point.
- 2026-07-31: LINE_A 0.055 → 0.35 as specified; the vm fade (×(1 - vm*0.6)) is kept. lineData shrinks from MAX_LINKS*BEZ_SEG*2*3 to MAX_LINKS*2*3 floats (11520 → 960 at the 160-link ceiling, 12x); 12x fewer segments submitted, and per link this also drops one hypot, one atan2, one cos, one sin and one divide.
- Verified removing clearR does NOT let lines cut the circle: respawnLink already caps pair distance at 240px, and a 240px chord between two ring dots stays 318px from centre = 94% of R. The bow was redundant given that cap.
- Closes the msg-130 "dotted bezier lines on the closed circle" item as moot — there is no bezier left to dot.

Decided by: user, 2026-07-31 — full-history reconcile across all 10 sessions (258 user messages, oldest→newest); two genuine gaps closed, one flagged as superseded

- 2026-07-31: audited every instruction in the project's whole message history against the code. 39/39 surviving feature requirements verified present; 7 deliberately-removed subsystems (FBO recursion, mirrored mini-ring, 3D shapes, spark stragglers, chromatic aberration, hue/colour, gaussian dof) confirmed absent.
- 2026-07-31: GAP CLOSED — font. Requests ran TT Bluescreens → Barlow Condensed → Monda; Monda was the newest and had never been applied. Swapped the Google Fonts link and stack to Monda (verified HTTP 200, serves 400–700), and p font-weight 300→400 since Monda has no 300.
- 2026-07-31: GAP CLOSED — logo glow. "increase the box shadow of the circle logo, like it glows a bit more, the closer we get" was only ever a static text-shadow, and that was dropped entirely in a512ea9's typography reset. Now driven from the frame loop: `--glow` = vm*0.55 + cs*0.45, quantised to 20 steps so the DOM is touched only when the change would be visible; text-shadow blur 30→120px and alpha 0.05→0.45 across the range.
- 2026-07-31: GAP FLAGGED, not implemented — "transform the solid Bezier lines to dotted Bezier lines when we are on the closed circle". Guide lines currently fade with vm instead. Superseded in spirit by the much newer "very thin, very faint lines" instruction; left alone pending a call.

Decided by: user, 2026-07-31 — constant 2x2px dots, whole page simplified for low-end hardware, typography reset, and the one overlooked request from this session implemented

- 2026-07-31: DOT_PX = 2 — every point sprite is a constant 2 CSS px. This RETIRES the entire dof()/gaussian/grain model (dofFn, dofSize, hash12, DOF_* constants, uT/uTO uniforms, aBlur/vBlur varyings all deleted): a 2px sprite has no room for a circle of confusion. Explicitly supersedes the earlier velocity-blur and depth-of-field requests — brightness is now the only channel that varies. Fragment fill drops from 1.09M to 0.026M per frame (42x) and each fragment goes from length+mix+2 exp+divide+hash to one clamp and one madd. All three fragment stages moved highp → mediump.
- 2026-07-31: per-dot ring geometry (1 sin + 1 cos + 1 sin, plus tr/wAng) now sits behind `if (vm > 0.001)` — with the vortex disengaged none of it reached the output anyway. Measured 230us → 9us per frame at 6400 dots, a 25x saving on the idle path. `tr` is carried to the radial pin via a new trBuf. Home-glide sqrt now gated on a squared compare so it only runs for dots actually in transit.
- 2026-07-31: typography — prefix set as small wide-tracked uppercase (clamp 0.68–0.95rem, 0.44em tracking, 0.34 value) against a tighter, slightly smaller wordmark (clamp 2.6–5.5rem, -0.015em tracking, 0.96 value); soon™ drops to 0.24em uppercase at 0.42 value. Negative right margin on the prefix cancels the trailing letter-space so the line is optically centred rather than mathematically centred.
- 2026-07-31: implemented the session's one genuinely missed request — per-dot random direction reversal on an independent 3–14s interval, re-rolled on each flip (flipT/FLIP_MIN/FLIP_MAX). Measured over 3 simulated minutes: global CW/CCW balance holds at 48–52%, and the longest same-direction run stays at 13 slots of 3108, so no large single-direction arc forms. Strict neighbour alternation does decay from 100% to ~50% — the arc pattern becomes random rather than clumped, which is the unavoidable cost of combining this with the interleaved-by-arc-rank rule.

Decided by: user, 2026-07-31 — vortex convergence moves back in toward the centre but stops short of it, and "zirkle" is put on the true viewport centre

- 2026-07-31: added VM_INNER = R*0.2 (68px at R=340); vmTarget denominator changes from `VM_OUTER - R` to `VM_OUTER - VM_INNER`, so the circle completes near the middle rather than at the ring radius. VM_INNER sits 3.3x outside DEAD_R and inside RT, so vm still has travel left when cs begins moving — vm now reads 0.60 at the circle radius, 0.93 at RT, 1.00 at 68px.
- 2026-07-31: overlay restructured — line1 and the sup are both taken out of flow (`position: absolute`) inside a new `.stack` wrapper whose box is therefore exactly line2's box. Previously "soon™" widened the line and pushed "zirkle" left of centre, and the prefix paragraph pushed it below centre; now the word itself lands on the viewport centre in both axes.

Decided by: user, 2026-07-31 — hairline guide lines under the bezier orbs, and the orbs come off dof() entirely (defocus read as mush at that size). Reverses the earlier "dots only, never lines" rule.

- 2026-07-31: new lineProg (gl.LINES) draws each active bezier tessellated into BEZ_SEG=12 segments at LINE_A=0.055 alpha, fading to 0.022 at full vortex — roughly 1:18 to 1:45 against the orb core under additive blend, so it reads as a hint of the path rather than a stroke. lineData sized MAX_LINKS*BEZ_SEG*2*3 floats; verified exact fit with no overflow at the randomize ceiling of 160 links (11519 highest index vs 11520 capacity).
- 2026-07-31: orb shaders no longer include dofFn — no gaussian, no grain. Crisp `smoothstep(0.5, 0.12, d)` point; the aBlur attribute is retained but now drives only a size swell (7→13.3px) and a 1.8x brightness lift over the last quarter of the ride, preserving the arrival signal without the defocus. Grid dots still own the single dof() model.

Decided by: user, 2026-07-31 — one defocus model for the whole page: every blurred thing takes a single 0..1 amount and calls the same dof()

- 2026-07-31: `bokehFn` replaced by `dofFn`, now injected into all four shaders (particle vert+frag, orb vert+frag) instead of only the two fragment shaders. Blur parameters collapse to four named constants — DOF_SIGMA_SHARP 0.085, DOF_SIGMA_BLUR 0.30, DOF_GRAIN 0.55, DOF_GROW 4.6 — and both the sprite growth curve (`dofSize`) and the gaussian+grain (`dof`) read from them, so size and falloff can no longer drift apart. Callers pass only an amount: grid dots use `1.0 - clamp(speed)`, orbs use arrival progress.
- Behaviour change: the orb's in-flight sigma was 0.16 against the grid's 0.085, and its grain peaked at 0.5 against 0.55 — two separate curves. Unified, so a travelling orb is now rendered slightly tighter than before; its 7px base size still distinguishes it from a 3.2px grid dot. Verified all four assembled shaders keep `#version 300 es` first and define dofSize/hash12/dof/main exactly once each.

Decided by: user, 2026-07-31 — blur means depth of field: a true gaussian circle of confusion with sensor grain, not a widened smoothstep edge

- 2026-07-31: added a shared `bokehFn` GLSL snippet (hash12 + bokeh) interpolated into both the grid-dot and orb fragment shaders. `bokeh()` is a real gaussian `exp(-d²/2σ²)`, shifted by its value at the sprite edge and rescaled so it reaches exactly 0 at d=0.5 while peaking at 1 — no normalise-by-centre hack needed, a gaussian peaks at 1 by construction. Grain is multiplied in via a hash12 dither seeded on `gl_FragCoord.xy + uT`, so out-of-focus dots break into animated sensor noise; new uT uniform on both programs, fed `(t*60) % 1024`.
- 2026-07-31: grid dots — sprite mix 2.6→4.6 (14.7px at rest vs 3.2px at speed), σ mix(0.30, 0.085), grain mix(0.55, 0). Measured profile: a resting dot holds alpha 0.46 at 25% radius and 0.26 at 35%, against 0.01/0.00 for a moving one — a genuinely broad spread rather than a soft edge. Peak alpha still respects the 0.75 floor.
- 2026-07-31: orb arrival bloom — sprite growth 2.4→3.6 (32.2px at t=1), σ mix(0.16, 0.34), grain up to 0.5. Fill cost at rest measured at 1.09M fragments for 6400 dots, ~0.5% of a 1080p frame.

Decided by: user, 2026-07-31 — link orbs always finish the curve they started, and blur-flash on arrival

- 2026-07-31: removed the mid-flight vanish — `a = s & 1 ? 1 - vm : 1` with `if (a <= 0.0015) continue` made odd-slot orbs fade to nothing partway along their bezier as the vortex engaged, so half the pool disappeared instead of animating to the end. Pool thinning now happens at respawn instead: odd slots get `linkAge -= vm * LINK_DELAY_MAX * 2` (up to 8s extra wait at full vortex), lowering the duty cycle while every orb still completes its curve.
- 2026-07-31: arrival flash driven by the bezier t value — orbData's third float switches from alpha to blur, `max(0, (p - 0.75) / 0.25)`, so it is flat for the first three quarters of the ride then blooms. Orb sprite grows `uSize * (1 + aBlur * 2.4)` (7px → 23.8px) and the fragment falloff spreads to `mix(0.0, -0.45, vBlur)`, normalised by its centre value so the bloom widens without dimming the core (verified core alpha holds 1.00 throughout, divisor bounded 0.54–1.00). Attribute 1 renamed aAlpha → aBlur; stride stays 3 floats.

Decided by: user, 2026-07-31 — slow dots genuinely blur (sprite grows so the falloff has room to read), and the full vortex rotates at half speed

- 2026-07-31: gl_PointSize is now velocity-driven — `uSize * mix(2.6, 1.0, sharp)`, so a still dot renders 8.3px against a moving dot's 3.2px. The defocus added in aa32749 was invisible because a 3.2px sprite has no room to spread a soft edge into; widening the smoothstep alone could not fix that. Inner edge also extended to `mix(-0.15, 0.42, sharp)` and the result normalised by `smoothstep(0.5, e1, 0.0)` so the softer falloff does not erode the 75% opacity floor — verified peak alpha is exactly 0.75 at rest and rises to 1.00 at speed, with the divisor bounded 0.86–1.00 so it can never be zero.
- 2026-07-31: tspd 950–1070 → 475–535 px/s, exactly half. Feeds both wAng (slot rotation) and sv (tangential tracking), so the whole vortex halves proportionally; spinS's cs boost is unchanged and still multiplies on top.

Decided by: user, 2026-07-31 — vortex collapse point is the circle's own radius rather than proximity to the centre, plus hot-loop performance work

- 2026-07-31: vmTarget remapped from `(R - md)/(R - RT)` to `(VM_OUTER - md)/(VM_OUTER - R)` with VM_OUTER = R*2.2 set in layout — the full fast circle is now drawn the moment the cursor reaches R (was only at 0.35R, deep inside); past R, depth drives cs alone and vm stays pinned at 1. RT keeps its old value but is now purely the cs depth reference.
- 2026-07-31: per-dot loop perf — replaced all six hot Math.hypot calls with `Math.sqrt(x*x+y*y)` (measured 6.3x faster; 299us -> 48us per frame at 6400 dots, saving ~251us/frame) and hoisted `ka = 1 - Math.exp(-dt*arrRate)` out of the loop (arrRate carries no per-dot term, so it was N redundant exp() calls per frame). Verified equivalent: hypot vs sqrt agree to 3.9e-16 relative over 400k samples in +/-2000px, and the hoisted ka is bit-identical. Remaining Math.hypot calls are all cold paths (layout, once-per-frame, or the <=160-iteration link loop).

Decided by: user, 2026-07-31 — sparks deleted outright rather than damped, and rotation direction is assigned from arc rank so the two streams interleave evenly around the whole circumference

- 2026-07-31: removed the escape/spark system entirely — esc/escAcc arrays, the inward-kick spawner, the interior-noise-field force branch and the `ka *= 0.1` free-flight case are all gone; every dot is now permanently on the ring track, so the radial pin and slot tracking apply without exception. dirn assignment moved into the ring-slot rank loop: `dirn[order[r]] = r & 1 ? 1 : -1`. Measured: every 45° sector is 50/50 CW/CCW and the longest same-direction run of adjacent slots is 1, against 1554 under the contiguous-halves rule — no bearing is dominated by one direction and there is no seam where counter-rotating arcs pull apart. alignK reverted to `vm * 13` (the cs boost added in 0887670 only made sense for contiguous halves; with neighbours counter-rotating a high gain just cancels).
- Correction to 0887670's rationale: the `homeY < CY` split was NOT interleaved on the ring. Sorting slots by bearing partitions by the sign of atan2(homeY-CY, …), so the grid-based rule and an arc-half rule produce the same contiguous 180° arcs — that change was behaviourally a no-op, not the fix it was described as.

Decided by: user, 2026-07-31 — no stragglers at full depth: sparks decay toward the centre instead of multiplying, outliers are pinned on radius error, and the flock is two coherent counter-rotating halves rather than a checkerboard

- 2026-07-31: spark spawn rate `26 + cs*44` → `26 * (1 - cs)` (was 70/s at dead centre, now 0/s — the rate climbing with depth was the straggler source); spark timers decay `dt*(1 + cs*6)` so existing sparks are reeled in ~7x sooner at depth. Radial pin gain gains a radius-error term: `min(1, (csGate*8.5 + err*3.0)*(vm-0.5)/0.5)` with `err = min(1, |tr-rl|/(R*0.25))` — a dot 30px off the line now pins at gain 1.0 regardless of csGate, where it previously needed cs>=0.81; still clamped at 1 so dc64734's divergence fix holds. Boid alignK `vm*13` → `vm*(13 + cs*22)` and sepK depth falloff `1-cs*0.92` → `1-cs*0.45` (separation was dropping to 8% at centre, so collision avoidance stopped working exactly where it was needed). dirn split changed from `(i+j)&1` checkerboard to `homeY < CY` — same 50/50 but contiguous, since alignment cannot read as unison when neighbours counter-rotate

Decided by: user, 2026-07-31 — root cause of the near-centre collapse: the radial pin was a positional correction with gain 8.5, which overshoots instead of converging

- 2026-07-31: radial pin gain clamped to 1 — `corr = (tr - rl) * min(1, csGate*8.5*(vm-0.5)/0.5)`. As a positional correction any gain above 1 throws the dot past its target radius and further out each frame (measured: 7.5x growth per frame, divergence in ~10 frames from a dot 5px off centre); at 1 it is an exact lerp onto tr and cannot diverge. Latent since the term was written but masked — csGate previously peaked near 0.006 close to centre, so effective gain was ~0.05; the DEAD_R remap in 3c87283 let csGate reach 1 and exposed it. Audited the other position writes: arrival ka is 1-exp(-dt·rate) so bounded below 1, de-overlap push maxes at 0.25, and velocity is clamped to vmax — the pin was the only unbounded one

Decided by: user, 2026-07-31 — page is fully monochrome; click inverts everything (supersedes the global accent hue introduced earlier today)

- 2026-07-31: removed the last colour — hueFn/hue2rgb, globalHue, rollGlobalHue, linkHue and the aHue/vHue orb varying all deleted; orbs render white (`vec3(glow * vAlpha * 1.6)`) and orbData stride drops 4→3 floats (vertexAttribPointer stride 16→12, attribute 2 no longer bound or enabled anywhere). soon™ sup gets a static rgba(255,255,255,0.55) and loses its colour transition. Click handler keeps randomizeParams + the body.invert toggle

Decided by: user, 2026-07-31 — dead band at the centre pins the field at its maximum instead of letting a near-centre cursor drive it past breaking, and the cursor ring shrinks on the same ramp

- 2026-07-31: hoisted RT (R·0.35) and new DEAD_R (R·0.06) onto layout so the frame loop and the cursor handler share them; `md` floor changed from 0.001 to DEAD_R, and ccRaw remapped from `1 - md/RT` to `(RT - md)/(RT - DEAD_R)` so cs reaches exactly 1 at the band edge rather than asymptotically chasing it at md=0. Inside the band vm/cs/csGate are all frozen — verified identical at d=20.4/10/2/0. Cursor ring scales 1.0→0.25 on the same RT→DEAD_R ramp, held at 0.25 inside the band

Decided by: user, 2026-07-31 — click toggles a whole-page invert filter; every dot visible at a 75% opacity floor and defocused in inverse proportion to its velocity

- 2026-07-31: `body.invert { filter: invert(1) }` toggled on canvas click alongside the existing randomize + hue re-roll. Particle shader: opacity floor 0.5→0.75 and brightness folded into one `lift` term (0.75 + 0.25·sharp) so a still dot sits at exactly 0.75 peak alpha; smoothstep inner edge now `mix(0.0, 0.42, sharp)` — at rest the falloff starts at the sprite centre (soft blob), at speed it snaps to a hard-edged disc. Blur spreads energy, so mean sprite alpha runs 0.225 at rest vs 0.848 at full speed even though peak alpha is floored at 0.75

Decided by: user, 2026-07-31 — grid dots go monochrome: 75% white base, no chromatic split

- 2026-07-31: particle fragment shader collapsed from three offset RGB lobes to a single grey disc — the neon came from chromatic aberration, not a colour uniform; base `b` 0.3+0.8·vSpeed → 0.75+0.25·vSpeed clamped to 1, so a still dot is 75% white and motion carries it to full. uAb uniform and its getUniformLocation/uniform1f plumbing deleted. Link orbs and soon™ keep the global accent hue (explicit earlier request, left untouched)

Decided by: user, 2026-07-31 — outer dots lead the grid→ring morph so the circle closes from outside in, instead of the whole grid shrinking as a rectangle

- 2026-07-31: per-dot morph clock — morphLead (0 at grid center, 1 at corners, baked in layout) offsets each dot's start by (1-lead)·MORPH_SPREAD and rescales by 1/(1-MORPH_SPREAD); shape-key lerp uses vmi instead of the global vm. MORPH_SPREAD=0.5, so corners sit on the ring at vm=0.50 exactly as center dots begin. Endpoints verified unchanged for every lead: vm=0 is still the full grid, vm=1 still the identical ring

Decided by: user, 2026-07-31 — residual pulse traced to lifeMigrate teleporting home slots on the tick boundary; migration now glides per-dot at its own rate

- 2026-07-31: lifeMigrate writes homeTX/homeTY targets instead of assigning homeX/homeY — homeX walks toward the target at Math.hypot(cellW,cellH)/LIFE_STEP_T scaled by a per-dot homeDur (0.6–2.2), clamped so it never overshoots. The teleport moved a full cell diagonal in one frame (~2206px/s) for every migrating dot at the same instant, and the shader's opacity rides velocity, so the whole grid flashed together each generation; glide peak is 14–51px/s spread over 0.6–2.2 generations with per-dot scatter

Decided by: user, 2026-07-31 — conway comes off the timer entirely: generations are pre-recorded onto a tape and played back one step behind the leading edge, so cells interpolate toward a known future instead of chasing a board that flips under them

- 2026-07-31: LIFE_TAPE=5 ring of Uint8Array generations replaces lifeAlive/lifeNext swap; primeLifeTape() records 4 generations up front, advanceLifeTape() reuses the slot the playhead vacates to record a new leading-edge gen (lookahead stays constant, ring never grows); playhead lifePlay slides dt/LIFE_STEP_T and dots lerp lifeTape[cur]→lifeTape[cur+1] — lifeMix chase, lifeAcc, lifeStep, lifeAliveIdx/Count, seedRadialLife all deleted; stagnation reseed now lands on the leading edge, 4 generations before it is visible

Decided by: user, 2026-07-31 — cursor-to-center distance gets a 0.001 floor so a dead-center cursor can never collapse the grid

- 2026-07-31: `md` (mouse→center distance) wrapped in Math.max(0.001, …) — the only unguarded center radius; every other one (rr, rE, r, rl) already carried a +1e-3 epsilon

Decided by: user, 2026-07-31 — Conway slower again and no pulsing: cells crossfade linearly over a whole tick instead of snapping, so the grid is never holding a frame

- 2026-07-31: added per-dot lifeMix Float32Array walked linearly at dt/LIFE_STEP_T toward the 0/1 board state — replaces the hard `lifeAlive[i] ? 0.85 : 0` opacity switch that caused the pulse; LIFE_STEP_T 0.55→1.2s, randomize 0.4–0.9→0.9–1.8; link board decoupled onto its own LINK_STEP_T (0.55s, randomize 0.4–0.9) so slowing conway no longer starves link re-pairing

Decided by: user, 2026-07-31 — a link is one solid dot walking its bezier, not a trail: each orb waits its own random delay before departing, Conway ticks ~3.4x slower, and no dot ever falls below 0.5 opacity

- 2026-07-31: bezier trail removed entirely — lineProg/lineVertSrc/lineFragSrc/lineData/lineVbo/BEZ_SEG deleted, orb is the only per-link geometry; orb alpha is flat 1.0 (bell envelope dropped), odd slots still fade out with the vortex; linkAge starts negative (−LINK_DELAY_MAX·rand) so each orb holds before departing; LIFE_STEP_T 0.16→0.55s, randomize range 0.08–0.3→0.4–0.9; particle shader velocity-opacity floor 0.15→0.5

Decided by: user, 2026-07-31 — slogan hierarchy: zirkle big and centered, "We come, full" muted prefix, soon™ bare superscript (parens were only markup notation)

- 2026-07-31: dropped parens around soon™; line2 zirkle clamp(3rem,9vw,7rem) at 0.92 alpha, prefix line 0.4 alpha, sup 0.3em
Decided by: user, 2026-07-31 — slogan replaces the big title: "We come, full / zirkle(soon™)", soon™ superscript; one global accent hue shared by link dots and soon™, re-rolled per click (recovered from a compacted prior session)

- 2026-07-31: h1 removed, two-line slogan with sup soon™; global hue (rollGlobalHue) replaces per-link random hue — click re-rolls it, fills linkHue, recolors soon™
Decided by: user, 2026-07-31 — dot opacity scales with velocity: still dots recede, motion carries the eye

- 2026-07-31: particle fragment shader alpha × (0.15 + 0.85·clamp(vSpeed)) — opacity rides velocity with a floor so the idle grid stays readable
Decided by: user, 2026-07-31 — cursor flash is always white (color belongs to the bezier dots alone), and link dots glide 2.5x slower so trails stop smearing into line-like streaks

- 2026-07-31: cursor click flash back to white (random hue stays on thinking-link dots only); linkLife 1.2–2.6s → 3.0–6.5s — orb rides p=age/life so longer life = slower glide, kills the ghost-trail line smear
Decided by: user, 2026-07-31 — Conway life reads better as empty space moving through a filled grid than as filled space in an empty grid

- 2026-07-31: inverted lifeAlive render toggle — alive cells dim to near-empty, dead cells stay the visible dot grid; existing radial migration now drifts voids outward instead of bright blobs
Decided by: user, 2026-07-31 — no lines anywhere, thinking-link trail becomes a dot chain (gl.POINTS), fixed a stale stride*3 subarray bug in the same pass

- 2026-07-31: thinking-link bezier now gl.POINTS trail instead of gl.LINES stroke — BEZ_SEG+1 point sprites per link, alternate-dot fade replaces alternate-segment fade; fixed lineData.subarray(0, lineVerts*3) left over from the stride-4 hue change, was truncating/misreading the buffer
Decided by: user, 2026-07-31 — thinking-links get random hue per respawn, glowing orb rides each bezier at the link's own age/life progress

- 2026-07-31: thinking-link random hue (aHue attribute, hue2rgb in shader) + point-sprite orb riding each bezier at linkAge/linkLife progress, additive glow blend
Decided by: user, 2026-07-31 — reconcile the click-anywhere/random-flash/fixed-radius/flat-line-force to-do's raised this session, mark complete

- 2026-07-31: click-anywhere randomize, random-hue cursor flash (force-reflow, no more coalesced rAF), fixed end-circle radius, 10x flat-line force gated to last 10% of center approach — synced page/index.html to root index.html
- 2026-07-31: fix Pages build (remove broken page gitlink, untrack .splinter), deploy latest particle-field index.html

## 2025-07-30 — fast orbital + hard bubble

- ROT 0.06→0.9 rad/s: fast gravitational orbit, ~7s per lap
- Spring 20→140: stiff binding, circle stays tight at speed
- Damping 6→10: critical at high spring, no ringing
- SEP_R 3.5→6.0, weight 80→180: wider evasion at high traffic
- Bubble: hard elastic bounce (particles ricochet, cannot enter)
- Bubble radius: log-scaled (`60+80·log(1+dist/R)`), grows with cursor distance
- Cursor circle: positioned halfway between mouse and ring center

## 2025-07-30 — autopilot bootstrap

- Created docs/ structure: prd/, specs/, autopilot/, servant/
- Wrote VISION, FEATURES, CHANGELOG
- Armed PRD reconcile watch on docs/prd.md + docs/prd/
