// §head page/pkg/tui/render.go:189-300 Model.View
// §sig func (m Model) View() string
	if m.wiz != nil {
		return m.wiz.View()
	}
	var b strings.Builder

	if m.ready {
		// Split panes: left = agent text + user chat, right = subpod + shell output.
		leftPane := m.vpChat.View()
		rightPane := m.vpTerminal.View()
		if m.pane != 1 {
			leftPane = mutedStyle.Render(leftPane)
			rightPane = mutedStyle.Render(rightPane)
		}
		// Panes sit side by side with a blank gap between them — no rule line.
		divW := 1
		split := lipgloss.JoinHorizontal(lipgloss.Top,
			lipgloss.NewStyle().Width(m.vpChat.Width).Render(leftPane),
			lipgloss.NewStyle().Width(divW).Render(""),
			lipgloss.NewStyle().Width(m.vpTerminal.Width).Render(rightPane),
		)
		b.WriteString(split)
		b.WriteByte('\n')

		// User statusline scripts above the input (from ~/.relay/statusline/above).
		// Priority = closeness to the input, so render ascending: lowest first at
		// the top, highest last — sitting just above the pane headers.
		for i := len(m.scriptLines) - 1; i >= 0; i-- {
			if m.scriptLines[i].Side == "above" {
				b.WriteString(m.scriptLines[i].Text)
				b.WriteByte('\n')
			}
		}

		// Column headers directly above the input: the two output panes are
		// labeled where the eye lands, so the input sits at the visual center
		// framed by what it feeds and what it reads. Built by hand (no
		// JoinHorizontal) to keep the per-keystroke render path cheap. Two-cell
		// padding left and right so neither label sticks to the edge or the
		// divider; muted so they read as chrome, not content.
		hdrLeft := "↑  Conversation"
		hdrRight := "↑  Shell output"
		lp := hdrLeft + strings.Repeat(" ", max(m.vpChat.Width-lipgloss.Width(hdrLeft)-2, 0))
		rp := hdrRight + strings.Repeat(" ", max(m.vpTerminal.Width-lipgloss.Width(hdrRight)-2, 0))
		paneHdr := mutedStyle.Render(lp) + " " + mutedStyle.Render(rp)
		b.WriteString(paneHdr)
		b.WriteByte('\n')

		// Input in center.
		in := m.renderInput()
		if m.pane != 1 && m.mode != modeSearch && !m.help {
			in = mutedStyle.Render(in)
		}
		b.WriteString(in)
		b.WriteByte('\n')

		// User statusline scripts below the input (from ~/.relay/statusline/below).
		// Priority = closeness to the input: render descending so highest sits
		// first, just under the input.
		for _, sl := range m.scriptLines {
			if sl.Side == "below" {
				b.WriteString(sl.Text)
				b.WriteByte('\n')
			}
		}

		// Divider bar: empty unless a broadcast marquee is scrolling.
		if d := m.renderDivider(); d != "" {
			b.WriteString(d)
			b.WriteByte('\n')
		}

		// Status line — pod metadata (version, counts, time, load). One of the
		// centered lines around the input; defaults to shown.
		devTag := ""
		if os.Getenv("RELAY_DEV") == "1" || os.Getenv("RELAY_DEV_CHILD") == "1" {
			devTag = " [DEV]"
		}
		left := fmt.Sprintf("  relay %s%s  %d pods", Version, devTag, len(m.views))
		right := fmt.Sprintf("%s  load %s  %d run  %d active", m.statTime, m.statLoad, m.runningCount(), m.activeCount())
		if m.busy {
			right += "  dispatching…"
		}
		leftW := lipgloss.Width(left)
		rightW := lipgloss.Width(right)
		pad := max(m.vpChat.Width+m.vpTerminal.Width+1-leftW-rightW-2, 1)
		headerLine := left + strings.Repeat(" ", pad) + right + "  "
		if m.pane == 0 {
			b.WriteString(activeStyle.Render(headerLine))
		} else {
			b.WriteString(headerStyle.Render(headerLine))
		}
		b.WriteByte('\n')

		// Pods list — the selectable pods, directly below the status line.
		bot := m.vp.View()
		if m.pane != 0 && m.mode != modeSearch && !m.help {
			bot = mutedStyle.Render(bot)
		}
		b.WriteString(bot)
	} else {
		b.WriteString(m.terminalContent())
		b.WriteByte('\n')
		in := m.renderInput()
		b.WriteString(in)
		b.WriteByte('\n')
		b.WriteString(separatorStyle.Render(strings.Repeat("─", 80)))
		b.WriteByte('\n')
		b.WriteString(m.renderTree())
	}
	return b.String()
// §foot page/pkg/tui/render.go Model.View