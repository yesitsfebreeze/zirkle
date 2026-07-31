// §head page/pkg/tui/tui.go:874-880 selectedState
// §sig func selectedState(m Model) string
	idx := m.selectedIdx()
	if idx < 0 || idx >= len(m.views) {
		return ""
	}
	return m.views[idx].State
// §foot page/pkg/tui/tui.go selectedState