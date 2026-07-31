// §head page/pkg/tui/tui.go:782-791 Model.selectedID
// §sig func (m Model) selectedID() string
	idx := m.selectedIdx()
	if idx < 0 || idx >= len(m.views) {
		return ""
	}
	if m.views[idx].ID == "+ new" {
		return ""
	}
	return m.views[idx].ID
// §foot page/pkg/tui/tui.go Model.selectedID