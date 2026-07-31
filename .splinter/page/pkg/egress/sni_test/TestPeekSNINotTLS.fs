// §head page/pkg/egress/sni_test.go:104-123 TestPeekSNINotTLS
// §sig func TestPeekSNINotTLS(t *testing.T)
	a, b := net.Pipe()
	defer a.Close()
	defer b.Close()

	go func() {
		b.Write([]byte("GET / HTTP/1.1\r\n"))
	}()

	peeked, sni, err := peekSNI(a)
	if !errors.Is(err, ErrNotTLS) {
		t.Errorf("err: got %v, want ErrNotTLS", err)
	}
	if sni != "" {
		t.Errorf("sni: got %q, want empty", sni)
	}
	if string(peeked) != "GET /" {
		t.Errorf("peeked: got %q, want 'GET /'", peeked)
	}
// §foot page/pkg/egress/sni_test.go TestPeekSNINotTLS