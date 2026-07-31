// §head page/pkg/egress/socks5.go:147-198 SOCKS5Proxy.readAddr
// §sig func (px *SOCKS5Proxy) readAddr(r io.Reader) (string, uint16, error)
	buf := make([]byte, 1)
	if _, err := io.ReadFull(r, buf); err != nil {
		return "", 0, errShortRead
	}
	atyp := buf[0]

	switch atyp {
	case socksAtypIPv4:
		raw := make([]byte, 4+2)
		if _, err := io.ReadFull(r, raw); err != nil {
			return "", 0, errShortRead
		}
		host := net.IP(raw[:4]).String()
		port := binary.BigEndian.Uint16(raw[4:])
		if !px.policy.Allow(host) {
			return "", 0, ErrDenied
		}
		return host, port, nil

	case socksAtypDomainName:
		if _, err := io.ReadFull(r, buf); err != nil {
			return "", 0, errShortRead
		}
		n := int(buf[0])
		raw := make([]byte, n+2)
		if _, err := io.ReadFull(r, raw); err != nil {
			return "", 0, errShortRead
		}
		host := string(raw[:n])
		port := binary.BigEndian.Uint16(raw[n:])
		if !px.policy.Allow(host) {
			return "", 0, ErrDenied
		}
		return host, port, nil

	case socksAtypIPv6:
		raw := make([]byte, 16+2)
		if _, err := io.ReadFull(r, raw); err != nil {
			return "", 0, errShortRead
		}
		host := net.IP(raw[:16]).String()
		port := binary.BigEndian.Uint16(raw[16:])
		if !px.policy.Allow(host) {
			return "", 0, ErrDenied
		}
		return host, port, nil

	default:
		return "", 0, errUnsupportedAddrType
	}
// §foot page/pkg/egress/socks5.go SOCKS5Proxy.readAddr