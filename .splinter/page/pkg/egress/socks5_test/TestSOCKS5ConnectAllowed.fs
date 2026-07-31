// §head page/pkg/egress/socks5_test.go:77-106 TestSOCKS5ConnectAllowed
// §sig func TestSOCKS5ConnectAllowed(t *testing.T)
	srv := newEchoListener(t)
	_, path := startSOCKS5Proxy(t, &Policy{AllowedDomains: []string{"127.0.0.1"}})

	conn, err := net.DialTimeout("unix", path, time.Second)
	if err != nil {
		t.Fatalf("dial proxy: %v", err)
	}
	defer conn.Close()

	host, portStr, _ := net.SplitHostPort(srv.Addr().String())
	port := uint16(0)
	fmt.Sscanf(portStr, "%d", &port)

	code := socks5Dial(t, conn, host, port)
	if code != socksRepSuccess {
		t.Fatalf("reply %d, want 0", code)
	}

	if _, err := fmt.Fprint(conn, "socks5 ping"); err != nil {
		t.Fatalf("write: %v", err)
	}
	buf := make([]byte, 11)
	if _, err := io.ReadFull(conn, buf); err != nil {
		t.Fatalf("read: %v", err)
	}
	if string(buf) != "socks5 ping" {
		t.Errorf("got %q, want %q", buf, "socks5 ping")
	}
// §foot page/pkg/egress/socks5_test.go TestSOCKS5ConnectAllowed