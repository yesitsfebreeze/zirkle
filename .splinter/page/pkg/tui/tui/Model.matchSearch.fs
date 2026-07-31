// §head page/pkg/tui/tui.go:754-761 Model.matchSearch
// §sig func (m Model) matchSearch(idx int) bool
	q := strings.ToLower(m.searchQ)
	v := m.views[idx]
	return strings.Contains(strings.ToLower(v.ID), q) ||
		strings.Contains(strings.ToLower(v.Prompt), q) ||
		strings.Contains(strings.ToLower(v.State), q) ||
		strings.Contains(strings.ToLower(v.Recap), q)
// §foot page/pkg/tui/tui.go Model.matchSearch