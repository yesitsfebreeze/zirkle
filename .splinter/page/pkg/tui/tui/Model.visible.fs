// §head page/pkg/tui/tui.go:731-752 Model.visible
// §sig func (m Model) visible() []int
	var out []int
	for i := range m.views {
		skip := false
		for j := i - 1; j >= 0; j-- {
			if m.views[j].Depth < m.views[i].Depth {
				if m.collapsed[j] {
					skip = true
				}
				break
			}
		}
		if skip {
			continue
		}
		if m.search && !m.matchSearch(i) {
			continue
		}
		out = append(out, i)
	}
	return out
// §foot page/pkg/tui/tui.go Model.visible