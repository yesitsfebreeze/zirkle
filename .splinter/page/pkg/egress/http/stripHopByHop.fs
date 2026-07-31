// §head page/pkg/egress/http.go:134-147 stripHopByHop
// §sig func stripHopByHop(h http.Header)
	connHeaders := h["Connection"]
	for _, key := range []string{
		"Proxy-Connection", "Keep-Alive", "Transfer-Encoding",
		"TE", "Connection", "Trailer", "Upgrade",
	} {
		h.Del(key)
	}
	for _, conn := range connHeaders {
		for _, name := range strings.Split(conn, ",") {
			h.Del(strings.TrimSpace(name))
		}
	}
// §foot page/pkg/egress/http.go stripHopByHop