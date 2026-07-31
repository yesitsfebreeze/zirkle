// §head page/pkg/egress/http_test.go:195-236 TestHTTPProxyStripsHopByHop
// §sig func TestHTTPProxyStripsHopByHop(t *testing.T)
	// A handler that echoes the request headers back.
	upstream := httptest.NewServer(http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		for k, vs := range r.Header {
			for _, v := range vs {
				fmt.Fprintf(w, "%s: %s\n", k, v)
			}
		}
	}))
	defer upstream.Close()

	_, path := startHTTPProxy(t, &Policy{AllowedDomains: []string{"127.0.0.1"}})

	conn, err := net.DialTimeout("unix", path, time.Second)
	if err != nil {
		t.Fatalf("dial proxy: %v", err)
	}
	defer conn.Close()

	addr := upstream.Listener.Addr().String()
	req := fmt.Sprintf(
		"GET http://%s/ HTTP/1.1\r\nHost: %s\r\nConnection: X-Dummy\r\nX-Dummy: should-not-appear\r\nProxy-Connection: keep-alive\r\nKeep-Alive: 300\r\n\r\n",
		addr, addr,
	)
	if _, err := fmt.Fprint(conn, req); err != nil {
		t.Fatalf("write request: %v", err)
	}

	resp, err := http.ReadResponse(bufio.NewReader(conn), nil)
	if err != nil {
		t.Fatalf("read response: %v", err)
	}
	defer resp.Body.Close()
	body, _ := io.ReadAll(resp.Body)

	if strings.Contains(string(body), "should-not-appear") {
		t.Error("hop-by-hop header X-Dummy (via Connection) was forwarded")
	}
	if strings.Contains(string(body), "Proxy-Connection") {
		t.Error("Proxy-Connection header was forwarded")
	}
// §foot page/pkg/egress/http_test.go TestHTTPProxyStripsHopByHop