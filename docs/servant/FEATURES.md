# FEATURES

- **Click anywhere** — canvas click randomizes params from any point, not gated to ring radius
- **White cursor flash** — click flashes the cursor ring white (force-reflow guaranteed); color lives only in the bezier link dots
- **Fixed end-circle radius** — RADIUS_CSS excluded from randomize pool, ring size never changes
- **Flat-line snap** — last 10% of cursor approach to center gates a 10x radial-pin force (cs^10), ring goes razor-thin only right at dead center
- **Global accent color** — one hue shared by all link dots and the soon™ sup, re-rolled on every click
- **Slogan** — "We come, full / zirkle(soon™)", soon™ superscript in the accent color, no big title
- **Colored links + riding orbs** — thinking-links tinted with the global accent hue; a glowing point-sprite rides its bezier at the link's own age/life progress
- **Dots only, never lines** — thinking-link bezier is a trail of point sprites (gl.POINTS), not a connected stroke
- **Velocity opacity** — dot alpha scales with speed (0.15 floor), still dots recede, moving dots come forward
- **Life as voids** — Conway alive cells render dim/empty against the filled grid, radial migration drifts the holes outward instead of bright blobs
- **Particle ring** — 6000 dots drawn as WebGL2 point sprites, additive glow
- **Counter-rotation** — parity-split CW/CCW orbital streams
- **Boids flocking** — separation, alignment, arrival spring via spatial hash
- **Living wiggle** — ring radius modulated by noise + sine harmonics
- **Cursor bounce** — elastic repulsion bubble, particles ricochet
- **Fixed circle** — 240 CSS px radius, pixel-space, aspect-independent
- **Monochrome** — pure grayscale, near-black background
- **Space Grotesk** — modern sans-serif overlay typography
