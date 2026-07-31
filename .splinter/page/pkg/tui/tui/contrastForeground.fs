// §head page/pkg/tui/tui.go:1224-1234 contrastForeground
// §sig func contrastForeground(bg string) string
	hex := themeHex(bg)
	r, g, b, ok := parseHex(hex)
	if !ok {
		return colorWhite
	}
	if relativeLuminance(r, g, b) > 0.179 {
		return colorBlack
	}
	return colorWhite
// §foot page/pkg/tui/tui.go contrastForeground