// §head page/pkg/comp/parser_test.go:154-161 containsStr
// §sig func containsStr(s, sub string) bool
	for i := 0; i <= len(s)-len(sub); i++ {
		if s[i:i+len(sub)] == sub {
			return true
		}
	}
	return false
// §foot page/pkg/comp/parser_test.go containsStr