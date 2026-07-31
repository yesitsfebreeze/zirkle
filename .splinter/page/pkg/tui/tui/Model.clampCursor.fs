// §head page/pkg/tui/tui.go:881-889 Model.clampCursor
// §sig func (m *Model) clampCursor()
	n := len(m.visible())
	if m.cursor >= n {
		m.cursor = n - 1
	}
	if m.cursor < 0 {
		m.cursor = 0
	}
// §foot page/pkg/tui/tui.go Model.clampCursor