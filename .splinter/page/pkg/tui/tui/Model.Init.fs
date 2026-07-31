// §head page/pkg/tui/tui.go:677-686 Model.Init
// §sig func (m Model) Init() tea.Cmd
	cmds := []tea.Cmd{m.load(), tick()}
	if m.broadcasts != nil {
		cmds = append(cmds, broadcastTick())
	}
	if m.hist != nil {
		cmds = append(cmds, m.loadPrompts())
	}
	return tea.Batch(cmds...)
// §foot page/pkg/tui/tui.go Model.Init