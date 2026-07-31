// §head page/pkg/tui/tui.go:322-330 Model.loadPrompts
// §sig func (m Model) loadPrompts() tea.Cmd
	return func() tea.Msg {
		ps, err := m.hist.RecentPrompts(historyLimit)
		if err != nil {
			return errMsg(err.Error())
		}
		return promptsMsg(ps)
	}
// §foot page/pkg/tui/tui.go Model.loadPrompts