// §head page/pkg/egress/http_test.go:241-271 newEchoListener
// §sig func newEchoListener(t *testing.T) net.Listener
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
			go func() {
				buf := make([]byte, 4096)
				for {
					n, err := c.Read(buf)
					if err != nil {
						c.Close()
						return
					}
					if _, err := c.Write(buf[:n]); err != nil {
						c.Close()
						return
					}
				}
			}()
		}
	}()
	return l
// §foot page/pkg/egress/http_test.go newEchoListener