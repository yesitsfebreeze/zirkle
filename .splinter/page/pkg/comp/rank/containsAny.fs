// §head page/pkg/comp/rank.go:31-39 containsAny
// §sig func containsAny(s string, terms []string) bool
	s = strings.ToLower(s)
	for _, t := range terms {
		if strings.Contains(s, t) {
			return true
		}
	}
	return false
// §foot page/pkg/comp/rank.go containsAny