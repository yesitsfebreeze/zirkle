// §head page/pkg/tui/render.go:134-184 Model.renderTree
// §sig func (m Model) renderTree() string
	var b strings.Builder
	if m.err != "" {
		b.WriteString(errorStyle.Render(m.err))
		b.WriteByte('\n')
	}
	if di := m.detailIdx(); di >= 0 {
		v := m.views[di]
		b.WriteString(detailStyle.Render(fmt.Sprintf("  %s %s %s · %s", symbolForState(v.State), v.ID, v.State, trunc(v.Recap, 80))))
		if v.Prompt != "" {
			b.WriteByte('\n')
			b.WriteString(mutedStyle.Render("  " + trunc(v.Prompt, 80)))
		}
		b.WriteByte('\n')
	}
	vis := m.visible()
	headers := timelineHeaders(m.views, vis, m.tl, time.Now())
	for vi, idx := range vis {
		if h, ok := headers[vi]; ok {
			b.WriteString(h.Line(m.vp.Width, m.tl))
			b.WriteByte('\n')
		}
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
		sym := symbolForState(v.State)
		line := fmt.Sprintf("  %s%s%s %-16s %s %-8s %s",
			indent, marker, toggle, trunc(v.ID, 14), sym, v.State, trunc(v.Recap, 35))
		if vi == m.cursor {
			line = selectedStyle.Width(m.vp.Width - 2).Render(line)
		}
		b.WriteString(line)
		b.WriteByte('\n')
	}
	if len(vis) == 0 {
		b.WriteString(mutedStyle.Render("  no pods"))
		b.WriteByte('\n')
	}
	return b.String()
// §foot page/pkg/tui/render.go Model.renderTree