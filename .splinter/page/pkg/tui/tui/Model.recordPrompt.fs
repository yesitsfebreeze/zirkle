// §head page/pkg/tui/tui.go:333-341 Model.recordPrompt
// §sig func (m Model) recordPrompt(prompt string) tea.Cmd
	h := m.hist
	return func() tea.Msg {
		if err := h.RecordPrompt(prompt); err != nil {
			return errMsg(err.Error())
		}
		return nil
	}
// §foot page/pkg/tui/tui.go Model.recordPrompt