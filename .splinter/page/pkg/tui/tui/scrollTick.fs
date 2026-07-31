// §head page/pkg/tui/tui.go:640-642 scrollTick
// §sig func scrollTick() tea.Cmd
	return tea.Tick(broadcastScrollStep, func(time.Time) tea.Msg { return scrollTickMsg{} })
// §foot page/pkg/tui/tui.go scrollTick