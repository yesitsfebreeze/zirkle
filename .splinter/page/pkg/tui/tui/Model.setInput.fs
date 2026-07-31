// §head page/pkg/tui/tui.go:464-467 Model.setInput
// §sig func (m *Model) setInput(val string)
	m.input.SetValue(val)
	m.input.SetCursor(len([]rune(val)))
// §foot page/pkg/tui/tui.go Model.setInput