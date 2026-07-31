// §head page/pkg/tui/render.go:28-39 Model.thoughtsContent
// §sig func (m Model) thoughtsContent() string
	if m.streaming && m.thoughts != "" {
		var b strings.Builder
		if idx := m.selectedIdx(); idx >= 0 && idx < len(m.views) && m.views[idx].ID != "+ new" {
			v := m.views[idx]
			b.WriteString(fmt.Sprintf(" %s · %s\n", v.ID, v.State))
		}
		b.WriteString(m.thoughts)
		return b.String()
	}
	return m.chatContent()
// §foot page/pkg/tui/render.go Model.thoughtsContent