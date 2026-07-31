// §head page/pkg/egress/socks5_test.go:182-208 TestSOCKS5UnsupportedCommand
// §sig func TestSOCKS5UnsupportedCommand(t *testing.T)
	_, path := startSOCKS5Proxy(t, &Policy{AllowedDomains: []string{"127.0.0.1"}})

	conn, err := net.DialTimeout("unix", path, time.Second)
	if err != nil {
		t.Fatalf("dial proxy: %v", err)
	}
	defer conn.Close()

	// Auth step.
	conn.Write([]byte{socksVer5, 1, socksAuthNone})
	rep := make([]byte, 2)
	io.ReadFull(conn, rep)

	// BIND command (0x02) — should be rejected.
	msg := []byte{socksVer5, socksCmdBind, 0, socksAtypIPv4, 127, 0, 0, 1, 0, 80}
	if _, err := conn.Write(msg); err != nil {
		t.Fatalf("write BIND: %v", err)
	}
	resp := make([]byte, 10)
	if _, err := io.ReadFull(conn, resp); err != nil {
		t.Fatalf("read BIND response: %v", err)
	}
	if resp[1] != socksRepCommandNotSupported {
		t.Errorf("unsupported command: reply %d, want %d", resp[1], socksRepCommandNotSupported)
	}
// §foot page/pkg/egress/socks5_test.go TestSOCKS5UnsupportedCommand