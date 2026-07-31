// §head page/pkg/tui/tui.go:204-211 Model.syncInput
// §sig func (m *Model) syncInput()
	if m.mode == modeSearch {
		m.searchQ = m.input.Value()
	} else {
		m.searchQ = ""
	}
	m.relayout()
// §foot page/pkg/tui/tui.go Model.syncInput