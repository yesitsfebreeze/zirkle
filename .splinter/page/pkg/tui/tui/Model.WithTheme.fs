// §head page/pkg/tui/tui.go:1355-1363 Model.WithTheme
// §sig func (m Model) WithTheme(colors map[string]string) Model
	if len(colors) == 0 {
		return m
	}
	h, a := applyThemeColors(colors)
	m.highlightColor = h
	m.attentionColor = a
	return m
// §foot page/pkg/tui/tui.go Model.WithTheme