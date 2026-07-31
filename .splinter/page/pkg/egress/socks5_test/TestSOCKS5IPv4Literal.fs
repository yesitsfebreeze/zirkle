// §head page/pkg/egress/socks5_test.go:162-180 TestSOCKS5IPv4Literal
// §sig func TestSOCKS5IPv4Literal(t *testing.T)
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
		t.Fatalf("IPv4 literal: reply %d, want 0", code)
	}
// §foot page/pkg/egress/socks5_test.go TestSOCKS5IPv4Literal