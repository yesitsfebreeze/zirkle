// §head page/pkg/egress/policy_test.go:5-61 TestAllow
// §sig func TestAllow(t *testing.T)
	cases := []struct {
		name   string
		policy Policy
		host   string
		want   bool
	}{
		{"zero value denies", Policy{}, "example.com", false},
		{"empty allowlist denies", Policy{AllowedDomains: []string{}}, "example.com", false},
		{"exact match allows", Policy{AllowedDomains: []string{"example.com"}}, "example.com", true},
		{"exact match is exact", Policy{AllowedDomains: []string{"example.com"}}, "a.example.com", false},
		{"denied beats allowed", Policy{
			AllowedDomains: []string{"example.com"},
			DeniedDomains:  []string{"example.com"},
		}, "example.com", false},
		{"denied carves out a wildcard", Policy{
			AllowedDomains: []string{"*.example.com"},
			DeniedDomains:  []string{"secret.example.com"},
		}, "secret.example.com", false},
		{"wildcard matches subdomain", Policy{AllowedDomains: []string{"*.example.com"}}, "a.example.com", true},
		{"wildcard matches nested subdomain", Policy{AllowedDomains: []string{"*.example.com"}}, "a.b.example.com", true},
		{"wildcard does not match apex", Policy{AllowedDomains: []string{"*.example.com"}}, "example.com", false},
		{"wildcard is not a suffix test", Policy{AllowedDomains: []string{"*.example.com"}}, "example.com.evil.test", false},
		{"wildcard does not match an IP literal", Policy{AllowedDomains: []string{"*.1"}}, "192.168.0.1", false},
		{"IP literal allows", Policy{AllowedDomains: []string{"127.0.0.1"}}, "127.0.0.1", true},
		{"short IP form canonicalizes", Policy{AllowedDomains: []string{"127.0.0.1"}}, "127.1", true},
		{"octal IP form canonicalizes", Policy{AllowedDomains: []string{"127.0.0.1"}}, "0177.0.0.01", true},
		{"hex IP form canonicalizes", Policy{AllowedDomains: []string{"127.0.0.1"}}, "0x7f.0.0.1", true},
		{"one-part IP form canonicalizes", Policy{AllowedDomains: []string{"127.0.0.1"}}, "2130706433", true},
		{"short form of a denied host is denied", Policy{
			AllowedDomains: []string{"*.example.com"},
			DeniedDomains:  []string{"127.0.0.1"},
		}, "127.1", false},
		{"policy entry may use the short form", Policy{AllowedDomains: []string{"127.1"}}, "127.0.0.1", true},
		{"port is stripped", Policy{AllowedDomains: []string{"127.0.0.1"}}, "127.0.0.1:11434", true},
		{"bracketed IPv6 with port", Policy{AllowedDomains: []string{"::1"}}, "[::1]:11434", true},
		{"bare IPv6 keeps its colons", Policy{AllowedDomains: []string{"::1"}}, "::1", true},
		{"IPv6 spelling canonicalizes", Policy{AllowedDomains: []string{"::1"}}, "0:0:0:0:0:0:0:1", true},
		{"4-in-6 unmaps", Policy{AllowedDomains: []string{"127.0.0.1"}}, "::ffff:127.0.0.1", true},
		{"trailing dot is stripped", Policy{AllowedDomains: []string{"example.com"}}, "example.com.", true},
		{"case is folded", Policy{AllowedDomains: []string{"example.com"}}, "EXAMPLE.COM", true},
		{"empty host denied", Policy{AllowedDomains: []string{"example.com"}}, "", false},
		{"NUL byte denied", Policy{AllowedDomains: []string{"*.example.com"}}, "evil.test\x00.example.com", false},
		{"control character denied", Policy{AllowedDomains: []string{"*.example.com"}}, "evil.test\n.example.com", false},
		{"embedded space denied", Policy{AllowedDomains: []string{"*.example.com"}}, "evil test.example.com", false},
		{"path smuggling denied", Policy{AllowedDomains: []string{"*.example.com"}}, "evil.test/x.example.com", false},
		{"empty pattern matches nothing", Policy{AllowedDomains: []string{""}}, "example.com", false},
	}

	for _, c := range cases {
		t.Run(c.name, func(t *testing.T) {
			if got := c.policy.Allow(c.host); got != c.want {
				t.Errorf("Allow(%q) = %v, want %v", c.host, got, c.want)
			}
		})
	}
// §foot page/pkg/egress/policy_test.go TestAllow