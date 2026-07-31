// §head page/pkg/egress/dial_test.go:68-92 TestDialDeniedNeverConnects
// §sig func TestDialDeniedNeverConnects(t *testing.T)
	l, err := net.Listen("tcp", "127.0.0.1:0")
	if err != nil {
		t.Fatalf("listen: %v", err)
	}
	defer l.Close()

	accepted := make(chan struct{}, 1)
	go func() {
		if c, err := l.Accept(); err == nil {
			accepted <- struct{}{}
			c.Close()
		}
	}()

	p := &Policy{}
	if _, err := p.Dial(context.Background(), l.Addr().String()); !errors.Is(err, ErrDenied) {
		t.Fatalf("err = %v, want ErrDenied", err)
	}
	select {
	case <-accepted:
		t.Error("denied host accepted a connection")
	default:
	}
// §foot page/pkg/egress/dial_test.go TestDialDeniedNeverConnects