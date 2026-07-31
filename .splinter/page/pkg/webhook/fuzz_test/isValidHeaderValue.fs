// §head page/pkg/webhook/fuzz_test.go:67-74 isValidHeaderValue
// §sig func isValidHeaderValue(s string) bool
	for _, r := range s {
		if r < 0x20 || r == 0x7f || r > 0x7e {
			return false
		}
	}
	return true
// §foot page/pkg/webhook/fuzz_test.go isValidHeaderValue