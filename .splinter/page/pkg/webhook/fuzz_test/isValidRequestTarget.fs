// §head page/pkg/webhook/fuzz_test.go:54-65 isValidRequestTarget
// §sig func isValidRequestTarget(p string) bool
	if p == "" || p[0] != '/' {
		return false
	}
	for _, r := range p {
		if r <= 0x20 || r == 0x7f || r > 0x7e {
			return false
		}
	}
	_, err := url.ParseRequestURI(p)
	return err == nil
// §foot page/pkg/webhook/fuzz_test.go isValidRequestTarget