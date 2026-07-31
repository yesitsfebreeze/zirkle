// §head page/pkg/egress/dial_test.go:34-54 TestDialAllowed
// §sig func TestDialAllowed(t *testing.T)
	srv := echoServer(t)
	p := &Policy{AllowedDomains: []string{"127.0.0.1"}}

	conn, err := p.Dial(context.Background(), srv.Addr().String())
	if err != nil {
		t.Fatalf("Dial: %v", err)
	}
	defer conn.Close()

	if _, err := conn.Write([]byte("ping")); err != nil {
		t.Fatalf("write: %v", err)
	}
	buf := make([]byte, 4)
	if _, err := io.ReadFull(conn, buf); err != nil {
		t.Fatalf("read: %v", err)
	}
	if string(buf) != "ping" {
		t.Errorf("got %q, want %q", buf, "ping")
	}
// §foot page/pkg/egress/dial_test.go TestDialAllowed