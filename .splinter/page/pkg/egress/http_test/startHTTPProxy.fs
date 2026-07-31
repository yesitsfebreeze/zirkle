// §head page/pkg/egress/http_test.go:26-41 startHTTPProxy
// §sig func startHTTPProxy(t *testing.T, p *Policy) (*HTTPProxy, string)
	t.Helper()
	px := NewHTTPProxy(p)
	path := socketPath(t, "http-proxy")
	l, err := Listen(path)
	if err != nil {
		t.Fatalf("Listen: %v", err)
	}
	go func() {
		if err := px.Serve(l); err != nil && err != http.ErrServerClosed {
			t.Logf("Serve: %v", err)
		}
	}()
	t.Cleanup(func() { px.Close() })
	return px, path
// §foot page/pkg/egress/http_test.go startHTTPProxy