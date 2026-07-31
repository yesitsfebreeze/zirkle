// §head page/pkg/egress/socks5_test.go:266-293 TestSOCKS5AddressTypeIsBounded
// §sig func TestSOCKS5AddressTypeIsBounded(t *testing.T)
	_, path := startSOCKS5Proxy(t, &Policy{})

	conn, err := net.DialTimeout("unix", path, time.Second)
	if err != nil {
		t.Fatalf("dial: %v", err)
	}
	defer conn.Close()

	// Auth step.
	conn.Write([]byte{socksVer5, 1, socksAuthNone})
	rep := make([]byte, 2)
	io.ReadFull(conn, rep)

	// A domain name of length 0 (empty) — should be handled without panicking
	// (it will fail to resolve, but the proxy's readAddr should handle it).
	// A domain length of 0 is technically valid per RFC 1928 (an empty
	// hostname), but it's a degenerate case. Let's test that it doesn't panic.
	// Actually, the real issue is that the client could claim length 255 and
	// the proxy would allocate 255 bytes. That's fine — bounded. More
	// interesting: what if the client claims a specific length and sends less?
	msg := []byte{socksVer5, socksCmdConnect, 0, socksAtypDomainName, 0, 0, 80}
	if _, err := conn.Write(msg); err != nil {
		t.Fatalf("write domain: %v", err)
	}
	// The proxy tries to read 0+2 bytes — succeeds (empty host) and then
	// Policy.Allow("") returns false, so it denies.  That is fine.
// §foot page/pkg/egress/socks5_test.go TestSOCKS5AddressTypeIsBounded