// §head page/pkg/tui/render.go:332-338 Model.acceptSuggestion
// §sig func (m *Model) acceptSuggestion()
	val := m.input.Value() + m.suggestion
	m.input.SetValue(val)
	m.input.SetCursor(len([]rune(val)))
	m.syncInput() // search mode re-filters the list on accept
	m.cursor = 0
// §foot page/pkg/tui/render.go Model.acceptSuggestion