// §head page/pkg/tui/tui.go:451-461 Model.histNext
// §sig func (m *Model) histNext()
	switch {
	case m.histIdx > 0:
		m.histIdx--
		m.setInput(m.history[m.histIdx])
	case m.histIdx == 0:
		m.histIdx = -1
		m.setInput("")
	}
	m.syncInput()
// §foot page/pkg/tui/tui.go Model.histNext