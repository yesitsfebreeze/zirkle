// §head page/pkg/tui/tui.go:294-302 Model.WithKeymap
// §sig func (m Model) WithKeymap(km keymap.Map, path string) Model
	m.km = km
	m.kmPath = path
	if !km.Done() {
		w := NewWizard(km, path)
		m.wiz = &w
	}
	return m
// §foot page/pkg/tui/tui.go Model.WithKeymap