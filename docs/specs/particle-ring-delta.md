# Particle ring — spec delta (2025-07-30)

## Changed

| Parameter | Before | After | Reason |
| ----------- | -------- | ------- | -------- |
| `ROT` | 0.06 rad/s | 0.9 rad/s | Fast gravitational orbit (~7s/lap) |
| Spring gain | 20 | 140 | Stiff binding, stable at high speed |
| Damping | 6.0 | 10.0 | Critical damping at spring=140 |
| `SEP_R` | 3.5 × dpr | 6.0 × dpr | Wider avoidance, faster traffic |
| Separation weight | 80 | 180 | Stronger evasion for head-on passes |
| Bubble type | Soft radial shove (20k) | Hard elastic bounce boundary | Particles ricochet, cannot enter |
| Bubble radius | Fixed 110px + pulse | Log-scaled: `60 + 80·log(1 + dist/R)` | Grows with cursor distance |
| Cursor visual | At mouse position | Halfway between mouse and center | Pulled toward ring center |
