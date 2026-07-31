// §head page/pkg/tui/tui.go:1178-1186 normalizeColor
// §sig func normalizeColor(c string) string
	if isANSIIndex(c) {
		return c
	}
	if !strings.HasPrefix(c, "#") {
		return "#" + c
	}
	return c
// §foot page/pkg/tui/tui.go normalizeColor