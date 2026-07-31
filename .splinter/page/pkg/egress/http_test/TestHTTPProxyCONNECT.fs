// §head page/pkg/egress/http_test.go:97-132 TestHTTPProxyCONNECT
// §sig func TestHTTPProxyCONNECT(t *testing.T)
	upstream := newEchoListener(t)
	_, path := startHTTPProxy(t, &Policy{AllowedDomains: []string{"127.0.0.1"}})

	conn, err := net.DialTimeout("unix", path, time.Second)
	if err != nil {
		t.Fatalf("dial proxy: %v", err)
	}
	defer conn.Close()

	addr := upstream.Addr().String()
	fmt.Fprintf(conn, "CONNECT %s HTTP/1.1\r\nHost: %s\r\n\r\n", addr, addr)
	resp, err := http.ReadResponse(bufio.NewReader(conn), nil)
	if err != nil {
		t.Fatalf("read response: %v", err)
	}
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("status %d, want 200", resp.StatusCode)
	}

	// Bidirectional exchange: write, read the echo, write again, read
	// again.  Neither side half-closes — the exchange tests that the
	// relay survives two round-trips without EOF on either side.
	for _, msg := range []string{"hello tunnel", "more data"} {
		if _, err := fmt.Fprint(conn, msg); err != nil {
			t.Fatalf("write %q: %v", msg, err)
		}
		buf := make([]byte, len(msg))
		if _, err := io.ReadFull(conn, buf); err != nil {
			t.Fatalf("read after %q: %v", msg, err)
		}
		if string(buf) != msg {
			t.Errorf("got %q, want %q", buf, msg)
		}
	}
// §foot page/pkg/egress/http_test.go TestHTTPProxyCONNECT