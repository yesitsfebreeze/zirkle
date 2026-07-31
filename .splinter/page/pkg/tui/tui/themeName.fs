// §head page/pkg/tui/tui.go:1197-1204 themeName
// §sig func themeName(c string) string
	for _, s := range Theme {
		if s.Index == c || strings.EqualFold(s.Hex, c) {
			return s.Name
		}
	}
	return "custom"
// §foot page/pkg/tui/tui.go themeName