// §head page/pkg/egress/dial_test.go:15-32 echoServer
// §sig func echoServer(t *testing.T) net.Listener
	t.Helper()
	l, err := net.Listen("tcp", "127.0.0.1:0")
	if err != nil {
		t.Fatalf("listen: %v", err)
	}
	t.Cleanup(func() { l.Close() })
	go func() {
		for {
			c, err := l.Accept()
			if err != nil {
				return
			}
			go func() { io.Copy(c, c); c.Close() }()
		}
	}()
	return l
// §foot page/pkg/egress/dial_test.go echoServer