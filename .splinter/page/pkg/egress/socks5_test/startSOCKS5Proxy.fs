// §head page/pkg/egress/socks5_test.go:11-24 startSOCKS5Proxy
// §sig func startSOCKS5Proxy(t *testing.T, p *Policy) (*SOCKS5Proxy, string)
	t.Helper()
	px := NewSOCKS5Proxy(p)
	path := socketPath(t, "socks5")
	l, err := Listen(path)
	if err != nil {
		t.Fatalf("Listen: %v", err)
	}
	go func() {
		px.Serve(l)
	}()
	t.Cleanup(func() { l.Close() })
	return px, path
// §foot page/pkg/egress/socks5_test.go startSOCKS5Proxy