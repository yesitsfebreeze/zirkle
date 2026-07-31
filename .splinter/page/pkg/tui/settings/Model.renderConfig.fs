// §head page/pkg/tui/settings.go:91-126 Model.renderConfig
// §sig func (m Model) renderConfig() string
	var b strings.Builder
	b.WriteString(headerStyle.Render(" Settings / Configuration (:config)"))
	b.WriteString("\n\n")

	rows := m.settingRows()
	width := m.vpChat.Width + m.vpTerminal.Width
	for i, row := range rows {
		var val string
		switch row.kind {
		case settingColor:
			style := lipgloss.NewStyle().Foreground(lipgloss.Color(row.color))
			val = style.Render("████ " + row.color + " " + themeName(row.color))
		case settingToggle:
			box := "[ ]"
			if row.on {
				box = "[x]"
			}
			val = box
		case settingChoice:
			val = "< " + row.value + " >"
		}
		line := fmt.Sprintf("  %-18s  %s", row.label+":", val)
		if i == m.configCur {
			line = selectedStyle.Width(width).Render(">" + line[1:])
		}
		b.WriteString(line)
		b.WriteByte('\n')
	}

	b.WriteByte('\n')
	b.WriteString(mutedStyle.Render(" ↑/↓ select · Space toggle · ←/→ cycle · Esc exit"))
	b.WriteByte('\n')

	return b.String()
// §foot page/pkg/tui/settings.go Model.renderConfig