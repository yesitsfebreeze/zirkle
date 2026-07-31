// §head page/pkg/egress/http.go:127-132 hostOnly
// §sig func hostOnly(host string) string
	if h, _, err := net.SplitHostPort(host); err == nil {
		return h
	}
	return host
// §foot page/pkg/egress/http.go hostOnly