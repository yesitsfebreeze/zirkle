// §head page/pkg/egress/http_test.go:136-171 TestHTTPProxyCONNECTDeniedNeverConnects
// §sig func TestHTTPProxyCONNECTDeniedNeverConnects(t *testing.T)
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

	_, path := startHTTPProxy(t, &Policy{})

	conn, err := net.DialTimeout("unix", path, time.Second)
	if err != nil {
		t.Fatalf("dial proxy: %v", err)
	}
	defer conn.Close()

	addr := l.Addr().String()
	fmt.Fprintf(conn, "CONNECT %s HTTP/1.1\r\nHost: %s\r\n\r\n", addr, addr)
	resp, _ := http.ReadResponse(bufio.NewReader(conn), nil)
	if resp.StatusCode != http.StatusForbidden {
		t.Errorf("status %d, want 403", resp.StatusCode)
	}

	select {
	case <-accepted:
		t.Error("denied CONNECT opened an upstream connection")
	default:
	}
// §foot page/pkg/egress/http_test.go TestHTTPProxyCONNECTDeniedNeverConnects