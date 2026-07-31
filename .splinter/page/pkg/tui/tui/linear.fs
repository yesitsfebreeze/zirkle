// §head page/pkg/tui/tui.go:1260-1266 linear
// §sig func linear(c uint8) float64
	s := float64(c) / 255.0
	if s <= 0.03928 {
		return s / 12.92
	}
	return math.Pow((s+0.055)/1.055, 2.4)
// §foot page/pkg/tui/tui.go linear