// §head page/pkg/egress/sni_test.go:206-269 TestHTTPProxyCONNECTSNIDenied
// §sig func TestHTTPProxyCONNECTSNIDenied(t *testing.T)
	// Upstream listener — must never receive a connection.
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

	// Allow the CONNECT host (127.0.0.1) but NOT the SNI host (evil.test).
	_, path := startHTTPProxy(t, &Policy{AllowedDomains: []string{"127.0.0.1"}})

	conn, err := net.DialTimeout("unix", path, time.Second)
	if err != nil {
		t.Fatalf("dial proxy: %v", err)
	}
	defer conn.Close()

	addr := l.Addr().String()
	fmt.Fprintf(conn, "CONNECT %s HTTP/1.1\r\nHost: %s\r\n\r\n", addr, addr)
	resp, err := http.ReadResponse(bufio.NewReader(conn), nil)
	if err != nil {
		t.Fatalf("read response: %v", err)
	}
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("CONNECT status %d, want 200", resp.StatusCode)
	}

	// Start a TLS handshake with SNI=evil.test (denied by policy).
	// The proxy should close the connection, causing the TLS dial to fail.
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

	// The upstream must never have accepted a connection.
	select {
	case <-accepted:
		t.Error("denied SNI opened an upstream connection")
	case <-time.After(100 * time.Millisecond):
	}
// §foot page/pkg/egress/sni_test.go TestHTTPProxyCONNECTSNIDenied