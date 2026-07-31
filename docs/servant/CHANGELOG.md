# CHANGELOG

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
