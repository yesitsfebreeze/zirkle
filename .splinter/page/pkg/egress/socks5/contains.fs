// §head page/pkg/egress/socks5.go:237-244 contains
// §sig func contains(bs []byte, v byte) bool
	for _, b := range bs {
		if b == v {
			return true
		}
	}
	return false
// §foot page/pkg/egress/socks5.go contains