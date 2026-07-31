# FEATURES

- **Click anywhere** — canvas click randomizes params from any point, not gated to ring radius
- **Random-hue cursor flash** — click flashes the cursor ring a random color, force-reflow guaranteed
- **Fixed end-circle radius** — RADIUS_CSS excluded from randomize pool, ring size never changes
- **Flat-line snap** — last 10% of cursor approach to center gates a 10x radial-pin force (cs^10), ring goes razor-thin only right at dead center
- **Colored links + riding orbs** — each thinking-link gets a random hue on respawn; a glowing point-sprite rides its bezier at the link's own age/life progress
- **Particle ring** — 6000 dots drawn as WebGL2 point sprites, additive glow
- **Counter-rotation** — parity-split CW/CCW orbital streams
- **Boids flocking** — separation, alignment, arrival spring via spatial hash
- **Living wiggle** — ring radius modulated by noise + sine harmonics
- **Cursor bounce** — elastic repulsion bubble, particles ricochet
- **Fixed circle** — 240 CSS px radius, pixel-space, aspect-independent
- **Monochrome** — pure grayscale, near-black background
- **Space Grotesk** — modern sans-serif overlay typography
