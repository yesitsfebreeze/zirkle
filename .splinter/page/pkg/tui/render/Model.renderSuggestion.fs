// §head page/pkg/tui/render.go:344-354 Model.renderSuggestion
// §sig func (m Model) renderSuggestion() string
	if m.suggestion == "" {
		return ""
	}
	w := m.vpChat.Width + m.vpTerminal.Width
	if w < 8 {
		w = 8
	}
	val := m.input.Value()
	return trunc(val, w-2) + mutedStyle.Render(trunc(m.suggestion, w-2-lipgloss.Width(val)))
// §foot page/pkg/tui/render.go Model.renderSuggestion