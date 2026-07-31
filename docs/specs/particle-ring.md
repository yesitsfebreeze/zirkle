# Particle ring — baseline spec

Inferred from code at `page/index.html` (2025-07-30, pre-PRD).

## Behavior

- 6000 particles form a ring, rendered as WebGL2 `gl.POINTS`
- Fixed radius: 240 CSS px × devicePixelRatio, never viewport-relative
- Counter-rotation: even indices rotate one direction, odd the other (ROT = 0.06 rad/s)
- Boids: arrival spring (k=20), alignment (2.0), separation (SEP_R=3.5×dpr, weight 80)
- Wiggle: 3 sine harmonics on ring radius (orders 2/3/5, slow drift)
- Breathe: slow radius scale modulation (0.5/0.8 Hz sines)
- Cursor bubble: pulsing sphere (110px base), positional shove (20k)
- Damping: exp(-dt * 6.0), near-critical at spring=20

## Visual

- Monochrome grayscale `vec3(0.95)`, additive blend
- Space Grotesk font overlay
- OS cursor hidden, custom circle div tracker
- Background: near-black
