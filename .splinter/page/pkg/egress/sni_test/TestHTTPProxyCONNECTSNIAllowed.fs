// §head page/pkg/egress/sni_test.go:167-201 TestHTTPProxyCONNECTSNIAllowed
// §sig func TestHTTPProxyCONNECTSNIAllowed(t *testing.T)
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

	// Send non-TLS data (echo listener echoes it back).  SNI peeking
	// sees non-TLS, relays without validation.
	for _, msg := range []string{"hello sni", "more data"} {
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
// §foot page/pkg/egress/sni_test.go TestHTTPProxyCONNECTSNIAllowed