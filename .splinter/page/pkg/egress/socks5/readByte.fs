// §head page/pkg/egress/socks5.go:221-227 readByte
// §sig func readByte(r io.Reader, n int) (byte, byte, error)
	buf := make([]byte, n)
	if _, err := io.ReadFull(r, buf); err != nil {
		return 0, 0, errShortRead
	}
	return buf[0], buf[1], nil
// §foot page/pkg/egress/socks5.go readByte