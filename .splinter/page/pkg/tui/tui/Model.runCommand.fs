// §head page/pkg/tui/tui.go:512-555 Model.runCommand
// §sig func (m Model) runCommand(fields []string) (tea.Model, tea.Cmd, bool)
	if len(fields) == 0 {
		return m, nil, false
	}
	id, ok := m.km.ResolveName(fields[0])
	if !ok && fields[0] == "settings" {
		id, ok = "settings", true // long-standing second name for the config screen
	}
	if !ok {
		return m, nil, false
	}
	switch id {
	case "settings":
		if len(fields) >= 2 {
			a := m.attentionColor
			if len(fields) >= 3 {
				a = fields[2]
			}
			m.updateColors(fields[1], a)
		}
		m.config = true
		m.configCur = 0
		mdl, cmd := m.afterCursorMove()
		return mdl, cmd, true
	case "tour":
		w := NewWizard(m.km, m.kmPath)
		m.wiz = &w
		mdl, cmd := m.afterCursorMove()
		return mdl, cmd, true
	case "bind", "rename":
		if len(fields) < 3 {
			m.err = "usage: " + fields[0] + " <action> <" + map[string]string{"bind": "key", "rename": "name"}[id] + ">"
			mdl, cmd := m.afterCursorMove()
			return mdl, cmd, true
		}
		key, name := fields[2], ""
		if id == "rename" {
			key, name = "", fields[2]
		}
		mdl, cmd := m.applyBinding(fields[1], key, name)
		return mdl, cmd, true
	}
	return m, nil, false
// §foot page/pkg/tui/tui.go Model.runCommand