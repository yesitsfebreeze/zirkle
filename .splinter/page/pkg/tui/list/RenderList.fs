// §head page/pkg/tui/list.go:129-152 RenderList
// §sig func RenderList(items []ListItem, cursor, detail int, width int) string
	var b strings.Builder
	if detail >= 0 && detail < len(items) {
		b.WriteString(detailStyle.Render(fmt.Sprintf(" %s — %s", items[detail].Title, items[detail].State)))
		b.WriteByte('\n')
		if items[detail].Detail != "" {
			for _, line := range strings.Split(items[detail].Detail, "\n") {
				b.WriteString(mutedStyle.Render(" " + trunc(line, width-2)))
				b.WriteByte('\n')
			}
		}
	}

	for i, item := range items {
		line := fmt.Sprintf(" %s %-20s %-10s %s", item.Icon, item.Title, item.State, item.Detail)
		if i == cursor {
			line = selectedStyle.Width(width).Render(line)
		}
		b.WriteString(line)
		b.WriteByte('\n')
	}

	return b.String()
// §foot page/pkg/tui/list.go RenderList