// §head page/pkg/tui/tui.go:1291-1297 rebuildBaseStyles
// §sig func rebuildBaseStyles()
	headerStyle = lipgloss.NewStyle().Bold(true).Foreground(fg)
	footerStyle = lipgloss.NewStyle().Foreground(muted)
	mutedStyle = lipgloss.NewStyle().Foreground(muted)
	separatorStyle = lipgloss.NewStyle().Foreground(rule)
	detailStyle = lipgloss.NewStyle().Foreground(secondary)
// §foot page/pkg/tui/tui.go rebuildBaseStyles