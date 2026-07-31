// §head page/pkg/comp/rank.go:21-29 tokenize
// §sig func tokenize(query string) []string
	var out []string
	for _, w := range strings.Fields(strings.ToLower(query)) {
		if !stopwords[w] {
			out = append(out, w)
		}
	}
	return out
// §foot page/pkg/comp/rank.go tokenize