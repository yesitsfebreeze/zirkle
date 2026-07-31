// §head page/pkg/egress/sni_test.go:342-400 TestSOCKS5CONNECTSNIDenied
// §sig func TestSOCKS5CONNECTSNIDenied(t *testing.T)
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

	_, path := startSOCKS5Proxy(t, &Policy{AllowedDomains: []string{"127.0.0.1"}})

	conn, err := net.DialTimeout("unix", path, time.Second)
	if err != nil {
		t.Fatalf("dial proxy: %v", err)
	}
	defer conn.Close()

	host, portStr, _ := net.SplitHostPort(l.Addr().String())
	port := uint16(0)
	fmt.Sscanf(portStr, "%d", &port)

	code := socks5Dial(t, conn, host, port)
	if code != socksRepSuccess {
		t.Fatalf("reply %d, want 0", code)
	}

	// TLS handshake with SNI=evil.test (denied).
	tlsConn := tls.Client(conn, &tls.Config{
		ServerName:         "evil.test",
		InsecureSkipVerify: true,
	})
	defer tlsConn.Close()

	handshakeErr := make(chan error, 1)
	go func() {
		handshakeErr <- tlsConn.Handshake()
	}()

	select {
	case err := <-handshakeErr:
		if err == nil {
			t.Error("TLS handshake succeeded — SNI was not blocked")
		}
	case <-time.After(2 * time.Second):
		t.Error("TLS handshake timed out — proxy did not close the tunnel")
	}

	select {
	case <-accepted:
		t.Error("denied SNI opened an upstream connection")
	case <-time.After(100 * time.Millisecond):
	}
// §foot page/pkg/egress/sni_test.go TestSOCKS5CONNECTSNIDenied