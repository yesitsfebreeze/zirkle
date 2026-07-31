// §head page/pkg/tui/render.go:305-324 Model.renderDivider
// §sig func (m Model) renderDivider() string
	w := m.vpChat.Width + m.vpTerminal.Width + 1 // +1 for the │ divider
	if w < 1 {
		w = 1
	}
	if m.bc == nil {
		return ""
	}
	runes := []rune(m.bc.text)
	var b strings.Builder
	for i := 0; i < w; i++ {
		ti := i - m.bc.pos
		if ti >= 0 && ti < len(runes) {
			b.WriteRune(runes[ti])
		} else {
			b.WriteByte(' ')
		}
	}
	return broadcastStyle.Render(b.String())
// §foot page/pkg/tui/render.go Model.renderDivider