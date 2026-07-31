// §head page/pkg/egress/dial_test.go:56-64 TestDialDenied
// §sig func TestDialDenied(t *testing.T)
	srv := echoServer(t)
	p := &Policy{AllowedDomains: []string{"example.com"}}

	_, err := p.Dial(context.Background(), srv.Addr().String())
	if !errors.Is(err, ErrDenied) {
		t.Fatalf("Dial to a denied host: err = %v, want ErrDenied", err)
	}
// §foot page/pkg/egress/dial_test.go TestDialDenied