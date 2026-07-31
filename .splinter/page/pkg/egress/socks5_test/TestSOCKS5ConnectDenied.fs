// §head page/pkg/egress/socks5_test.go:108-121 TestSOCKS5ConnectDenied
// §sig func TestSOCKS5ConnectDenied(t *testing.T)
	_, path := startSOCKS5Proxy(t, &Policy{})

	conn, err := net.DialTimeout("unix", path, time.Second)
	if err != nil {
		t.Fatalf("dial proxy: %v", err)
	}
	defer conn.Close()

	code := socks5Dial(t, conn, "example.com", 80)
	if code != socksRepConnectionNotAllowed {
		t.Errorf("denied domain: reply %d, want %d", code, socksRepConnectionNotAllowed)
	}
// §foot page/pkg/egress/socks5_test.go TestSOCKS5ConnectDenied