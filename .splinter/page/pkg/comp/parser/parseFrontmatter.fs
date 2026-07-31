// §head page/pkg/comp/parser.go:66-82 parseFrontmatter
// §sig func parseFrontmatter(fm string) map[string]string
	m := make(map[string]string)
	for _, line := range strings.Split(fm, "\n") {
		line = strings.TrimSpace(line)
		if line == "" || strings.HasPrefix(line, "#") {
			continue
		}
		colon := strings.IndexByte(line, ':')
		if colon < 0 {
			continue
		}
		key := strings.TrimSpace(line[:colon])
		val := strings.TrimSpace(line[colon+1:])
		m[key] = val
	}
	return m
// §foot page/pkg/comp/parser.go parseFrontmatter