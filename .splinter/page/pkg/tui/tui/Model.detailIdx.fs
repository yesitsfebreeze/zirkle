// §head page/pkg/tui/tui.go:797-807 Model.detailIdx
// §sig func (m Model) detailIdx() int
	if m.detail == "" {
		return -1
	}
	for i, v := range m.views {
		if v.ID == m.detail {
			return i
		}
	}
	return -1
// §foot page/pkg/tui/tui.go Model.detailIdx