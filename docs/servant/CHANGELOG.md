# CHANGELOG

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
