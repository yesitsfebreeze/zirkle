// §head page/pkg/tui/tui.go:1268-1286 Model.updateColors
// §sig func (m *Model) updateColors(h, a string)
	if h != "" {
		h = normalizeColor(h)
		m.highlightColor = h
		amber = lipgloss.Color(h)
		onPrimary = readableOn(h)
	}
	if a != "" {
		a = normalizeColor(a)
		m.attentionColor = a
		attention = lipgloss.Color(a)
	}
	selectedStyle = lipgloss.NewStyle().Bold(true).Foreground(onPrimary).Background(amber)
	activeStyle = lipgloss.NewStyle().Bold(true).Foreground(amber)
	broadcastStyle = lipgloss.NewStyle().Bold(true).Foreground(onPrimary).Background(amber)
	attentionStyle = lipgloss.NewStyle().Bold(true).Foreground(attention)
	errorStyle = lipgloss.NewStyle().Bold(true).Foreground(failure)
	rebuildBaseStyles()
// §foot page/pkg/tui/tui.go Model.updateColors