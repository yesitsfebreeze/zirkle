// §head page/pkg/egress/http.go:20-28 NewHTTPProxy
// §sig func NewHTTPProxy(p *Policy) *HTTPProxy
	px := &HTTPProxy{policy: p}
	px.server = &http.Server{
		Handler:     http.HandlerFunc(px.serveHTTP),
		IdleTimeout: 60 * time.Second,
		ReadTimeout: 30 * time.Second,
	}
	return px
// §foot page/pkg/egress/http.go NewHTTPProxy