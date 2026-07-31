// §head page/pkg/tui/render.go:356-362 trunc
// §sig func trunc(s string, n int) string
	r := []rune(s)
	if len(r) <= n {
		return s
	}
	return string(r[:n-1]) + "…"
// §foot page/pkg/tui/render.go trunc