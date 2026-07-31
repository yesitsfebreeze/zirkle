// §head page/pkg/egress/dial_test.go:94-103 TestDialBadAddress
// §sig func TestDialBadAddress(t *testing.T)
	p := &Policy{AllowedDomains: []string{"example.com"}}
	for _, addr := range []string{"example.com", "", ":"} {
		if _, err := p.Dial(context.Background(), addr); err == nil {
			t.Errorf("Dial(%q) = nil error, want a parse failure", addr)
		} else if errors.Is(err, ErrDenied) && addr != ":" {
			t.Errorf("Dial(%q) reported a policy denial for a malformed address", addr)
		}
	}
// §foot page/pkg/egress/dial_test.go TestDialBadAddress