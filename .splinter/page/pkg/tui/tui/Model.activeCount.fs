// §head page/pkg/tui/tui.go:666-674 Model.activeCount
// §sig func (m Model) activeCount() int
	n := 0
	for _, v := range m.views {
		if v.State != "done" && v.State != "failed" {
			n++
		}
	}
	return n
// §foot page/pkg/tui/tui.go Model.activeCount