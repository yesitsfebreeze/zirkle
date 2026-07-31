// §head page/pkg/egress/http_test.go:173-191 TestHTTPProxyCONNECTDenied
// §sig func TestHTTPProxyCONNECTDenied(t *testing.T)
	_, path := startHTTPProxy(t, &Policy{AllowedDomains: []string{"example.com"}})

	conn, err := net.DialTimeout("unix", path, time.Second)
	if err != nil {
		t.Fatalf("dial proxy: %v", err)
	}
	defer conn.Close()

	addr := "127.0.0.1:19999"
	fmt.Fprintf(conn, "CONNECT %s HTTP/1.1\r\nHost: %s\r\n\r\n", addr, addr)
	resp, err := http.ReadResponse(bufio.NewReader(conn), nil)
	if err != nil {
		t.Fatalf("read response: %v", err)
	}
	if resp.StatusCode != http.StatusForbidden {
		t.Errorf("denied host: status %d, want %d", resp.StatusCode, http.StatusForbidden)
	}
// §foot page/pkg/egress/http_test.go TestHTTPProxyCONNECTDenied