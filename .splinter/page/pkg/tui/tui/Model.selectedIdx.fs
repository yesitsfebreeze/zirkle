// §head page/pkg/tui/tui.go:765-778 Model.selectedIdx
// §sig func (m Model) selectedIdx() int
	vis := m.visible()
	if len(vis) == 0 {
		return -1
	}
	cursor := m.cursor
	if cursor >= len(vis) {
		cursor = len(vis) - 1
	}
	if cursor < 0 {
		cursor = 0
	}
	return vis[cursor]
// §foot page/pkg/tui/tui.go Model.selectedIdx