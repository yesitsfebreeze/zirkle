// §head page/pkg/egress/sni_test.go:57-100 TestParseClientHelloSNI
// §sig func TestParseClientHelloSNI(t *testing.T)
	ln, err := net.Listen("tcp", "127.0.0.1:0")
	if err != nil {
		t.Fatalf("listen: %v", err)
	}
	defer ln.Close()

	go func() {
		conn, err := tls.Dial("tcp", ln.Addr().String(), &tls.Config{
			ServerName:         "allowed.test",
			InsecureSkipVerify: true,
		})
		if err == nil {
			conn.Close()
		}
	}()

	srv, err := ln.Accept()
	if err != nil {
		t.Fatalf("accept: %v", err)
	}
	defer srv.Close()

	hdr := make([]byte, 5)
	if _, err := io.ReadFull(srv, hdr); err != nil {
		t.Fatalf("read header: %v", err)
	}
	if hdr[0] != 0x16 {
		t.Fatalf("not a handshake: 0x%02x", hdr[0])
	}
	recLen := int(binary.BigEndian.Uint16(hdr[3:5]))
	body := make([]byte, recLen)
	if _, err := io.ReadFull(srv, body); err != nil {
		t.Fatalf("read body: %v", err)
	}

	sni, err := parseClientHelloSNI(body)
	if err != nil {
		t.Fatalf("parseClientHelloSNI: %v", err)
	}
	if sni != "allowed.test" {
		t.Errorf("SNI: got %q, want allowed.test", sni)
	}
// §foot page/pkg/egress/sni_test.go TestParseClientHelloSNI