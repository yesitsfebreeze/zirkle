// §head page/pkg/tui/list.go:59-85 Model.helpListItems
// §sig func (m Model) helpListItems() []ListItem
	acts := keymap.Actions()
	items := make([]ListItem, len(acts))
	for i, a := range acts {
		icon := " "
		detail := a.Manual
		if i == m.helpCur {
			switch m.helpEdit {
			case 1:
				icon = attentionStyle.Render("●")
				detail = "press the key to bind to " + a.ID + " — esc cancels"
			case 2:
				icon = attentionStyle.Render("●")
				detail = "new name for " + a.ID + ": " + m.helpBuf + "▏ — enter commits, esc cancels"
			default:
				detail = a.Manual + "\n\nr rebind this key · a rename this command"
			}
		}
		items[i] = ListItem{
			Icon:   icon,
			Title:  m.helpLabel(a.ID),
			State:  a.Desc,
			Detail: detail,
		}
	}
	return items
// §foot page/pkg/tui/list.go Model.helpListItems