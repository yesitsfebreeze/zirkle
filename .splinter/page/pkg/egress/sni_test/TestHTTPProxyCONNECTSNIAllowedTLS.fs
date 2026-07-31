// §head page/pkg/egress/sni_test.go:273-338 TestHTTPProxyCONNECTSNIAllowedTLS
// §sig func TestHTTPProxyCONNECTSNIAllowedTLS(t *testing.T)
	cert := generateSelfSignedCert(t)
	tlsLn, err := tls.Listen("tcp", "127.0.0.1:0", &tls.Config{
		Certificates: []tls.Certificate{cert},
	})
	if err != nil {
		t.Fatalf("tls listen: %v", err)
	}
	defer tlsLn.Close()

	go func() {
		c, err := tlsLn.Accept()
		if err != nil {
			return
		}
		defer c.Close()
		buf := make([]byte, 256)
		for {
			n, err := c.Read(buf)
			if err != nil {
				return
			}
			c.Write(buf[:n])
		}
	}()

	_, path := startHTTPProxy(t, &Policy{AllowedDomains: []string{"127.0.0.1"}})

	conn, err := net.DialTimeout("unix", path, time.Second)
	if err != nil {
		t.Fatalf("dial proxy: %v", err)
	}
	defer conn.Close()

	addr := tlsLn.Addr().String()
	fmt.Fprintf(conn, "CONNECT %s HTTP/1.1\r\nHost: %s\r\n\r\n", addr, addr)
	resp, err := http.ReadResponse(bufio.NewReader(conn), nil)
	if err != nil {
		t.Fatalf("read response: %v", err)
	}
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("status %d, want 200", resp.StatusCode)
	}

	tlsConn := tls.Client(conn, &tls.Config{
		ServerName: "127.0.0.1",
		RootCAs:    certPool(cert),
	})
	defer tlsConn.Close()

	if err := tlsConn.Handshake(); err != nil {
		t.Fatalf("TLS handshake: %v", err)
	}

	msg := "hello through TLS"
	if _, err := tlsConn.Write([]byte(msg)); err != nil {
		t.Fatalf("write: %v", err)
	}
	buf := make([]byte, len(msg))
	if _, err := io.ReadFull(tlsConn, buf); err != nil {
		t.Fatalf("read: %v", err)
	}
	if string(buf) != msg {
		t.Errorf("got %q, want %q", buf, msg)
	}
// §foot page/pkg/egress/sni_test.go TestHTTPProxyCONNECTSNIAllowedTLS