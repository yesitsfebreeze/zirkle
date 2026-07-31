// §head page/pkg/comp/parser.go:120-132 scanLinks
// §sig func scanLinks(body string) []string
	matches := linkRe.FindAllStringSubmatch(body, -1)
	seen := make(map[string]bool)
	var out []string
	for _, m := range matches {
		target := m[0] // includes @ prefix
		if !seen[target] {
			seen[target] = true
			out = append(out, target)
		}
	}
	return out
// §foot page/pkg/comp/parser.go scanLinks