// §head page/pkg/tui/tui.go:214-229 Model.updatePrompt
// §sig func (m *Model) updatePrompt()
	icon := promptIcon(m.mode)
	if m.pane == 1 {
		icon = activeStyle.Render(icon)
	}
	prompt := icon + " "
	// Show the icon only on the first line; pad other lines to keep alignment.
	m.input.SetPromptFunc(2, func(lineIdx int) string {
		if lineIdx == 0 {
			return prompt
		}
		return "  "
	})
	m.input.SetPlaceholder(modePlaceholder(m.mode))
	m.input.SetWidth(m.input.ta.Width())
// §foot page/pkg/tui/tui.go Model.updatePrompt