# CHANGELOG

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
