// §head page/pkg/comp/parser_test.go:150-152 contains
// §sig func contains(s, sub string) bool
	return len(s) >= len(sub) && (s == sub || len(s) > 0 && containsStr(s, sub))
// §foot page/pkg/comp/parser_test.go contains