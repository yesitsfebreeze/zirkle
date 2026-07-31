// §head page/pkg/egress/socks5_test.go:210-231 TestSOCKS5NoCommonAuth
// §sig func TestSOCKS5NoCommonAuth(t *testing.T)
	_, path := startSOCKS5Proxy(t, &Policy{})

	conn, err := net.DialTimeout("unix", path, time.Second)
	if err != nil {
		t.Fatalf("dial proxy: %v", err)
	}
	defer conn.Close()

	// Offer only auth method 0x02 (username/password), which the proxy
	// does not support (it only does 0x00).
	conn.Write([]byte{socksVer5, 1, 2})
	rep := make([]byte, 2)
	if _, err := io.ReadFull(conn, rep); err != nil {
		t.Fatalf("read auth reply: %v", err)
	}
	// RFC 1928: if none of the offered methods are acceptable, the
	// server replies with 0xFF and closes.
	if rep[1] != 0xFF {
		t.Errorf("no-common-auth: method byte %d, want 0xFF", rep[1])
	}
// §foot page/pkg/egress/socks5_test.go TestSOCKS5NoCommonAuth