// §head page/pkg/tui/render.go:41-98 Model.chatContent
// §sig func (m Model) chatContent() string
	// PlanCommander pods render their plan conversation instead of the chat log.
	if s, ok := m.planContent(); ok {
		return s
	}

	var b strings.Builder
	idx := m.selectedIdx()

	// Header on the left pane for the selected pod. The "+ new" row is a tree
	// affordance, not a real pod — no header. With no pod selected but an
	// in-flight prompt, the conversation still renders below.
	if idx >= 0 && idx < len(m.views) && m.views[idx].ID != "+ new" {
		v := m.views[idx]
		b.WriteString(fmt.Sprintf(" %s · %s", v.ID, v.State))
		b.WriteByte('\n')
	}
	if len(m.chat) == 0 {
		return b.String()
	}

	width := m.vpChat.Width
	if width < 1 {
		width = 76
	}

	for i, msg := range m.chat {
		if msg.Role == "user" {
			label := selectedStyle.Render(" user ")
			b.WriteString(label)
			b.WriteByte('\n')
			for _, line := range strings.Split(msg.Content, "\n") {
				b.WriteString(" " + trunc(line, width-2))
				b.WriteByte('\n')
			}
			if i+1 < len(m.chat) && m.chat[i+1].Role == "agent" {
				b.WriteString(mutedStyle.Render(" " + strings.Repeat("┄", width-2)))
				b.WriteByte('\n')
			}
		} else {
			for _, line := range strings.Split(msg.Content, "\n") {
				b.WriteString(strings.Repeat(" ", 4) + trunc(line, width-6))
				b.WriteByte('\n')
			}
			if i+1 >= len(m.chat) || m.chat[i+1].Role == "user" {
				b.WriteString(mutedStyle.Render(" " + strings.Repeat("─", width-2)))
				b.WriteByte('\n')
			}
		}
	}

	if m.busy {
		b.WriteString(activeStyle.Render(" agent working…"))
		b.WriteByte('\n')
	}

	return b.String()
// §foot page/pkg/tui/render.go Model.chatContent