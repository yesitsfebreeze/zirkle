// §head page/pkg/tui/tui.go:1365-1389 Model.cycleColor
// §sig func (m *Model) cycleColor(setting int, dir int)
	if setting == 0 {
		presets := highlightPresets
		idx := 0
		for i, p := range presets {
			if strings.EqualFold(p, m.highlightColor) {
				idx = i
				break
			}
		}
		idx = (idx + dir + len(presets)) % len(presets)
		m.updateColors(presets[idx], m.attentionColor)
	} else if setting == 1 {
		presets := attentionPresets
		idx := 0
		for i, p := range presets {
			if strings.EqualFold(p, m.attentionColor) {
				idx = i
				break
			}
		}
		idx = (idx + dir + len(presets)) % len(presets)
		m.updateColors(m.highlightColor, presets[idx])
	}
// §foot page/pkg/tui/tui.go Model.cycleColor