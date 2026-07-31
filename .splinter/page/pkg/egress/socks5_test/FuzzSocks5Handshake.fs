// §head page/pkg/egress/socks5_test.go:295-345 FuzzSocks5Handshake
// §sig func FuzzSocks5Handshake(f *testing.F)
	seeds := [][]byte{
		{socksVer5, 1, socksAuthNone},
		{socksVer5, 1, socksAuthNone, socksVer5, socksCmdConnect, 0, socksAtypDomainName, 3, 'a', 'b', 'c', 0, 80},
		{socksVer5, 2, socksAuthNone, 0x02},
		{0xFF, 0x00},
		{},
		{socksVer5, 0},
		{socksVer5, 1, socksAuthNone, socksVer5, socksCmdConnect, 0, socksAtypIPv4, 127, 0, 0, 1, 0, 80},
	}
	for _, s := range seeds {
		f.Add(s)
	}

	p := &Policy{AllowedDomains: []string{"127.0.0.1", "localhost"}}
	px := NewSOCKS5Proxy(p)

	f.Fuzz(func(t *testing.T, input []byte) {
		if len(input) == 0 {
			return
		}
		a, b := net.Pipe()
		defer a.Close()
		defer b.Close()

		done := make(chan struct{})
		// Pipe ignores deadlines, so a watchdog kills the proxy
		// goroutine if the handshake stalls on incomplete input.
		go func() {
			select {
			case <-time.After(100 * time.Millisecond):
				a.Close()
			case <-done:
			}
		}()
		go func() {
			px.handle(a)
			close(done)
		}()

		if _, err := b.Write(input); err == nil {
			// Read whatever the proxy replies within a deadline.
			// The watchdog will close a if the handshake stalls,
			// unblocking b's read with EOF.
			buf := make([]byte, 20)
			b.SetReadDeadline(time.Now().Add(200 * time.Millisecond))
			io.ReadFull(b, buf)
		}
		<-done
	})
// §foot page/pkg/egress/socks5_test.go FuzzSocks5Handshake