// §head page/pkg/tui/timeline.go:138-162 TimelineHeader.Line
// §sig func (h TimelineHeader) Line(width int, c TimelineConfig) string
	parts := []string{h.Label}
	if c.ShowCount {
		parts = append(parts, fmt.Sprintf("%d pods", h.Total))
	}
	if c.ShowStates {
		var tally []string
		for _, sym := range symbolOrder {
			if n := h.Symbols[sym]; n > 0 {
				tally = append(tally, fmt.Sprintf("%d%s", n, sym))
			}
		}
		if len(tally) > 0 {
			parts = append(parts, strings.Join(tally, " "))
		}
	}
	if c.ShowSpan && h.Span > 0 {
		parts = append(parts, "span "+shortDur(h.Span))
	}
	text := "  ── " + strings.Join(parts, " · ") + " "
	if pad := width - len([]rune(text)) - 2; pad > 0 {
		text += strings.Repeat("─", pad)
	}
	return mutedStyle.Render(text)
// §foot page/pkg/tui/timeline.go TimelineHeader.Line