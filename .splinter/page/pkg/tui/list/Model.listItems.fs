// §head page/pkg/tui/list.go:88-118 Model.listItems
// §sig func (m Model) listItems() []ListItem
	if m.help {
		return m.helpListItems()
	}
	// Normal: convert visible pods to list items.
	vis := m.visible()
	items := make([]ListItem, len(vis))
	for i, idx := range vis {
		v := m.views[idx]
		indent := strings.Repeat("  ", v.Depth)
		toggle := " "
		if v.HasChildren {
			if m.collapsed[idx] {
				toggle = "▶"
			} else {
				toggle = "▼"
			}
		}
		marker := " "
		if v.HasQuestions {
			marker = attentionStyle.Render("?")
		}
		items[i] = ListItem{
			Icon:   marker,
			Title:  indent + toggle + " " + v.ID,
			State:  v.State,
			Detail: v.Recap,
		}
	}
	return items
// §foot page/pkg/tui/list.go Model.listItems