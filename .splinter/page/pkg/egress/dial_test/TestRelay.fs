// §head page/pkg/egress/dial_test.go:105-127 TestRelay
// §sig func TestRelay(t *testing.T)
	srv := echoServer(t)
	p := &Policy{AllowedDomains: []string{"127.0.0.1"}}

	front, back := net.Pipe()
	upstream, err := p.Dial(context.Background(), srv.Addr().String())
	if err != nil {
		t.Fatalf("Dial: %v", err)
	}
	go Relay(back, upstream)

	if _, err := front.Write([]byte("through")); err != nil {
		t.Fatalf("write: %v", err)
	}
	buf := make([]byte, 7)
	if _, err := io.ReadFull(front, buf); err != nil {
		t.Fatalf("read: %v", err)
	}
	if string(buf) != "through" {
		t.Errorf("got %q, want %q", buf, "through")
	}
	front.Close()
// §foot page/pkg/egress/dial_test.go TestRelay