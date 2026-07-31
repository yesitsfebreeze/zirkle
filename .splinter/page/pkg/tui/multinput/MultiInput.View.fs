// §head page/pkg/tui/multinput.go:70-107 MultiInput.View
// §sig func (mi MultiInput) View() string
	view := mi.ta.View()
	total := mi.ta.LineCount()
	vis := mi.ta.Height()

	// Inject ghost text into the last line before scrollbar rendering.
	if mi.ghost != "" {
		lines := strings.Split(view, "\n")
		lines[len(lines)-1] += ghostStyle.Render(mi.ghost)
		view = strings.Join(lines, "\n")
	}

	if total <= vis {
		return view
	}
	cur := mi.ta.Line()
	yOff := 0
	if cur >= vis {
		yOff = cur - vis/2
	}
	if yOff > total-vis {
		yOff = total - vis
	}
	if yOff < 0 {
		yOff = 0
	}
	thumb := max(1, vis*vis/total)
	thumbStart := yOff * vis / total
	lines := strings.Split(view, "\n")
	for i, line := range lines {
		ch := "░"
		if i >= thumbStart && i < thumbStart+thumb {
			ch = "█"
		}
		lines[i] = line + ch
	}
	return strings.Join(lines, "\n")
// §foot page/pkg/tui/multinput.go MultiInput.View