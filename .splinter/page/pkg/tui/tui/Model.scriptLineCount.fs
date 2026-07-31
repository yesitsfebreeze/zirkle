// §head page/pkg/tui/tui.go:857-865 Model.scriptLineCount
// §sig func (m Model) scriptLineCount() int
	n := 0
	for _, sl := range m.scriptLines {
		if sl.Side == "above" || sl.Side == "below" {
			n++
		}
	}
	return n
// §foot page/pkg/tui/tui.go Model.scriptLineCount