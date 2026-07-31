// §head page/pkg/egress/socks5_test.go:123-160 TestSOCKS5ConnectDeniedNeverConnects
// §sig func TestSOCKS5ConnectDeniedNeverConnects(t *testing.T)
	l, err := net.Listen("tcp", "127.0.0.1:0")
	if err != nil {
		t.Fatalf("listen: %v", err)
	}
	defer l.Close()

	accepted := make(chan struct{}, 1)
	go func() {
		if c, err := l.Accept(); err == nil {
			accepted <- struct{}{}
			c.Close()
		}
	}()

	_, path := startSOCKS5Proxy(t, &Policy{})

	conn, err := net.DialTimeout("unix", path, time.Second)
	if err != nil {
		t.Fatalf("dial proxy: %v", err)
	}
	defer conn.Close()

	host, portStr, _ := net.SplitHostPort(l.Addr().String())
	port := uint16(0)
	fmt.Sscanf(portStr, "%d", &port)

	code := socks5Dial(t, conn, host, port)
	if code != socksRepConnectionNotAllowed {
		t.Errorf("reply %d, want %d", code, socksRepConnectionNotAllowed)
	}

	select {
	case <-accepted:
		t.Error("denied CONNECT opened an upstream connection")
	default:
	}
// §foot page/pkg/egress/socks5_test.go TestSOCKS5ConnectDeniedNeverConnects