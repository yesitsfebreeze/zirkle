// §head page/pkg/egress/http_test.go:43-75 TestHTTPProxyPlain
// §sig func TestHTTPProxyPlain(t *testing.T)
	upstream := httptest.NewServer(http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		fmt.Fprint(w, "hello from upstream")
	}))
	defer upstream.Close()

	_, path := startHTTPProxy(t, &Policy{AllowedDomains: []string{"127.0.0.1"}})

	conn, err := net.DialTimeout("unix", path, time.Second)
	if err != nil {
		t.Fatalf("dial proxy: %v", err)
	}
	defer conn.Close()

	addr := upstream.Listener.Addr().String()
	req := fmt.Sprintf("GET http://%s/ HTTP/1.1\r\nHost: %s\r\n\r\n", addr, addr)
	if _, err := fmt.Fprint(conn, req); err != nil {
		t.Fatalf("write request: %v", err)
	}

	resp, err := http.ReadResponse(bufio.NewReader(conn), nil)
	if err != nil {
		t.Fatalf("read response: %v", err)
	}
	defer resp.Body.Close()
	body, _ := io.ReadAll(resp.Body)
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("status %d: %s", resp.StatusCode, body)
	}
	if !bytes.Contains(body, []byte("hello from upstream")) {
		t.Errorf("body: got %q, want hello from upstream", body)
	}
// §foot page/pkg/egress/http_test.go TestHTTPProxyPlain