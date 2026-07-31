// §head page/pkg/tui/tui.go:633-635 broadcastTick
// §sig func broadcastTick() tea.Cmd
	return tea.Tick(broadcastInterval, func(t time.Time) tea.Msg { return broadcastTickMsg(t) })
// §foot page/pkg/tui/tui.go broadcastTick