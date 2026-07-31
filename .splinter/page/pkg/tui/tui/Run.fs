// §head page/pkg/tui/tui.go:311-316 Run
// §sig func Run(src Source, cmdr Commander, broadcasts <-chan string, hist PromptHistory, themeColors map[string]string, km keymap.Map, kmPath string, tl TimelineConfig, tlSave func(TimelineConfig) error) error
	m := New(src, cmdr, broadcasts).WithHistory(hist).WithTheme(themeColors).WithKeymap(km, kmPath).WithTimeline(tl).WithTimelineSave(tlSave)
	p := tea.NewProgram(m, tea.WithAltScreen())
	_, err := p.Run()
	return err
// §foot page/pkg/tui/tui.go Run