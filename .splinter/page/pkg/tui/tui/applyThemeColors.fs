// §head page/pkg/tui/tui.go:1311-1353 applyThemeColors
// §sig func applyThemeColors(colors map[string]string) (h, a string)
	if len(colors) == 0 {
		return ansiMagenta, ansiBlue
	}
	if c, ok := colors["foreground"]; ok {
		fg = lipgloss.Color(c)
	}
	if c, ok := colors["muted"]; ok {
		muted = lipgloss.Color(c)
	}
	if c, ok := colors["secondary"]; ok {
		secondary = lipgloss.Color(c)
	}
	if c, ok := colors["failure"]; ok {
		failure = lipgloss.Color(c)
	}
	if c, ok := colors["surface"]; ok {
		surface = lipgloss.Color(c)
	}
	if c, ok := colors["rule"]; ok {
		rule = lipgloss.Color(c)
	}
	h, a = ansiMagenta, ansiBlue
	if c, ok := colors["primary"]; ok {
		h = c
	}
	if c, ok := colors["attention"]; ok {
		a = c
	}
	amber = lipgloss.Color(h)
	attention = lipgloss.Color(a)
	onPrimary = readableOn(h)
	if c, ok := colors["on_primary"]; ok {
		onPrimary = lipgloss.Color(c)
	}
	selectedStyle = lipgloss.NewStyle().Bold(true).Foreground(onPrimary).Background(amber)
	activeStyle = lipgloss.NewStyle().Bold(true).Foreground(amber)
	broadcastStyle = lipgloss.NewStyle().Bold(true).Foreground(onPrimary).Background(amber)
	attentionStyle = lipgloss.NewStyle().Bold(true).Foreground(attention)
	errorStyle = lipgloss.NewStyle().Bold(true).Foreground(failure)
	rebuildBaseStyles()
	return h, a
// §foot page/pkg/tui/tui.go applyThemeColors