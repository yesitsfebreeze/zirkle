// §head page/cmd/relay/main.go:882-888 truncStr
// §sig func truncStr(s string, n int) string
	r := []rune(s)
	if len(r) <= n {
		return s
	}
	return string(r[:n]) + "…"
// §foot page/cmd/relay/main.go truncStr