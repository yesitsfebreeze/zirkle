// §head page/pkg/egress/socks5.go:229-235 readBytes
// §sig func readBytes(r io.Reader, n int) ([]byte, error)
	buf := make([]byte, n)
	if _, err := io.ReadFull(r, buf); err != nil {
		return nil, errShortRead
	}
	return buf, nil
// §foot page/pkg/egress/socks5.go readBytes