// §head page/pkg/tui/tui.go:1256-1258 relativeLuminance
// §sig func relativeLuminance(r, g, b uint8) float64
	return 0.2126*linear(r) + 0.7152*linear(g) + 0.0722*linear(b)
// §foot page/pkg/tui/tui.go relativeLuminance