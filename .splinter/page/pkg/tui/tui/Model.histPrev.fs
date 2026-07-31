// §head page/pkg/tui/tui.go:442-448 Model.histPrev
// §sig func (m *Model) histPrev()
	if m.histIdx < len(m.history)-1 {
		m.histIdx++
		m.setInput(m.history[m.histIdx])
	}
	m.syncInput()
// §foot page/pkg/tui/tui.go Model.histPrev