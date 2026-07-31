// §head page/pkg/tui/tui.go:626-628 tick
// §sig func tick() tea.Cmd
	return tea.Tick(time.Second, func(t time.Time) tea.Msg { return tickMsg(t) })
// §foot page/pkg/tui/tui.go tick