// §head page/pkg/tui/list.go:48-53 Model.helpLabel
// §sig func (m Model) helpLabel(id string) string
	if k := m.km.Key(id); k != "" {
		return k
	}
	return m.km.Key("command") + m.km.Name(id)
// §foot page/pkg/tui/list.go Model.helpLabel