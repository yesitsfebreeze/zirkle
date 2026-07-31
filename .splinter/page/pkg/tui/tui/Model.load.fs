// §head page/pkg/tui/tui.go:357-365 Model.load
// §sig func (m Model) load() tea.Cmd
	return func() tea.Msg {
		vs, err := m.src.List()
		if err != nil {
			return errMsg(err.Error())
		}
		return refreshMsg(vs)
	}
// §foot page/pkg/tui/tui.go Model.load