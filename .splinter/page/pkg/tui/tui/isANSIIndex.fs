// §head page/pkg/tui/tui.go:1188-1193 isANSIIndex
// §sig func isANSIIndex(c string) bool
	if len(c) != 1 {
		return false
	}
	return c[0] >= '0' && c[0] <= '8'
// §foot page/pkg/tui/tui.go isANSIIndex