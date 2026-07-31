// §head page/pkg/comp/parser.go:108-114 extractJustBlock
// §sig func extractJustBlock(body string) string
	m := justBlockRe.FindStringSubmatch(body)
	if m == nil {
		return ""
	}
	return strings.TrimSpace(m[1])
// §foot page/pkg/comp/parser.go extractJustBlock