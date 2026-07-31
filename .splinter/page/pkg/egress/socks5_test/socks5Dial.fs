// §head page/pkg/egress/socks5_test.go:29-75 socks5Dial
// §sig func socks5Dial(t *testing.T, conn net.Conn, host string, port uint16) byte
	t.Helper()

	// Auth negotiation.
	msg := []byte{socksVer5, 1, socksAuthNone}
	if _, err := conn.Write(msg); err != nil {
		t.Fatalf("socks5 auth write: %v", err)
	}
	rep := make([]byte, 2)
	if _, err := io.ReadFull(conn, rep); err != nil {
		t.Fatalf("socks5 auth read: %v", err)
	}
	if rep[0] != socksVer5 || rep[1] != socksAuthNone {
		conn.Close()
		return socksRepGeneralFailure
	}

	// CONNECT request.
	var atyp byte
	switch {
	case net.ParseIP(host).To4() != nil:
		atyp = socksAtypIPv4
		ip := net.ParseIP(host).To4()
		msg = []byte{socksVer5, socksCmdConnect, 0, atyp, ip[0], ip[1], ip[2], ip[3],
			byte(port >> 8), byte(port)}
	case net.ParseIP(host).To16() != nil:
		atyp = socksAtypIPv6
		ip := net.ParseIP(host).To16()
		msg = []byte{socksVer5, socksCmdConnect, 0, atyp}
		msg = append(msg, ip...)
		msg = append(msg, byte(port>>8), byte(port))
	default:
		atyp = socksAtypDomainName
		msg = []byte{socksVer5, socksCmdConnect, 0, atyp, byte(len(host))}
		msg = append(msg, []byte(host)...)
		msg = append(msg, byte(port>>8), byte(port))
	}
	if _, err := conn.Write(msg); err != nil {
		t.Fatalf("socks5 connect write: %v", err)
	}

	resp := make([]byte, 10)
	if _, err := io.ReadFull(conn, resp); err != nil {
		t.Fatalf("socks5 connect read: %v", err)
	}
	return resp[1]
// §foot page/pkg/egress/socks5_test.go socks5Dial