// §head page/pkg/tui/tui.go:1209-1216 themeHex
// §sig func themeHex(c string) string
	for _, s := range Theme {
		if s.Index == c {
			return s.Hex
		}
	}
	return c
// §foot page/pkg/tui/tui.go themeHex