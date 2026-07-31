# Zirkle — repo brief

**Stack:** single HTML file with inline WebGL2 + JS particle simulation.
**Source:** `page/index.html` — self-contained, no deps, no build.
**Font:** Space Grotesk (Google Fonts CDN).
**No git** — fresh repo, not yet initialized.

## Architecture

- 6000 CPU-sim particles rendered as `gl.POINTS` (additive blend, monochrome)
- Counter-rotating ring: parity-split CW/CCW streams at configurable `ROT` rad/s
- Boids flocking: arrival (homing spring), separation, alignment via spatial hash grid
- Cursor interaction: elastic bounce off a pulsing bubble sphere
- Fixed canvas size: `RADIUS_CSS = 240px × dpr`, never viewport-relative
- Living wiggle: sine harmonics on ring radius for organic deformation

## What this repo IS

An interactive WebGL art piece — the zirkle.ai landing page. A single circle of
drawn-from-particles that orbits, breathes, and reacts to cursor.

## What this repo is NOT

- Not an app with routes, backend, or state management
- Not a build system or framework project
- Not a multi-file codebase — changes are always to `page/index.html`

## Tuning knobs (in JS source)

| Constant | Current | Purpose |
| ---------- | --------- | --------- |
| `N` | 6000 | particle count |
| `RADIUS_CSS` | 240 | ring radius in CSS px |
| `ROT` | ~0.06 | counter-rotation speed |
| `SEP_R` | 3.5 × dpr | separation radius |
| Spring gain | ~20 | homing stiffness |
| Damping | ~6 | velocity decay |
| Bubble shove | ~20k | cursor repulsion strength |

## PRD / Specs

No PRD or specs exist yet. Create `docs/prd.md` to describe desired behavior,
then the reconcile watch (already armed) will derive specs and drive changes.
