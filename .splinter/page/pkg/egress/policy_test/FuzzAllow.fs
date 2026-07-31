// §head page/pkg/egress/policy_test.go:79-99 FuzzAllow
// §sig func FuzzAllow(f *testing.F)
	for _, seed := range []string{
		"example.com", "127.1", "[::1]:80", "evil\x00.example.com",
		"0x7f.0.0.1", "2130706433", "....", "*.example.com",
	} {
		f.Add(seed)
	}
	p := Policy{
		AllowedDomains: []string{"*.example.com", "127.0.0.1"},
		DeniedDomains:  []string{"secret.example.com"},
	}
	f.Fuzz(func(t *testing.T, host string) {
		// The filter must never panic on client-supplied input, and a
		// denied host must stay denied however it is spelled.
		if p.Allow(host) {
			if h, ok := canonical(host); !ok || h == "secret.example.com" {
				t.Errorf("Allow(%q) allowed a denied or uncanonical host", host)
			}
		}
	})
// §foot page/pkg/egress/policy_test.go FuzzAllow