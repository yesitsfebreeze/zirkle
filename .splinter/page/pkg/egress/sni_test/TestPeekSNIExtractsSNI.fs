// §head page/pkg/egress/sni_test.go:127-163 TestPeekSNIExtractsSNI
// §sig func TestPeekSNIExtractsSNI(t *testing.T)
	ln, err := net.Listen("tcp", "127.0.0.1:0")
	if err != nil {
		t.Fatalf("listen: %v", err)
	}
	defer ln.Close()

	go func() {
		conn, err := tls.Dial("tcp", ln.Addr().String(), &tls.Config{
			ServerName:         "sni.test",
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

	peeked, sni, err := peekSNI(srv)
	if err != nil {
		t.Fatalf("peekSNI: %v", err)
	}
	if sni != "sni.test" {
		t.Errorf("SNI: got %q, want sni.test", sni)
	}
	if len(peeked) == 0 {
		t.Error("peeked bytes empty")
	}
	if peeked[0] != 0x16 {
		t.Errorf("first byte: 0x%02x, want 0x16", peeked[0])
	}
// §foot page/pkg/egress/sni_test.go TestPeekSNIExtractsSNI