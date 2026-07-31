// §head page/pkg/tui/tui.go:656-664 Model.runningCount
// §sig func (m Model) runningCount() int
	n := 0
	for _, v := range m.views {
		if v.State == "running" {
			n++
		}
	}
	return n
// §foot page/pkg/tui/tui.go Model.runningCount