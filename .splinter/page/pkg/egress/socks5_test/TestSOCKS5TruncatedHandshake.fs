// §head page/pkg/egress/socks5_test.go:235-262 TestSOCKS5TruncatedHandshake
// §sig func TestSOCKS5TruncatedHandshake(t *testing.T)
	_, path := startSOCKS5Proxy(t, &Policy{})

	for _, fragment := range [][]byte{
		{},
		{socksVer5},
		{socksVer5, 1},
		{socksVer5, 1, socksAuthNone},
		{socksVer5, 1, socksAuthNone, socksVer5}, // partial CONNECT
		{0xFF, 0x00, 0x00},
	} {
		t.Run(fmt.Sprintf("%x", fragment), func(t *testing.T) {
			conn, err := net.DialTimeout("unix", path, time.Second)
			if err != nil {
				t.Fatalf("dial: %v", err)
			}
			defer conn.Close()
			if len(fragment) > 0 {
				conn.Write(fragment)
			}
			// Read what we can, then close — the proxy must not
			// panic regardless of the partial input.
			buf := make([]byte, 10)
			conn.SetReadDeadline(time.Now().Add(50 * time.Millisecond))
			io.ReadFull(conn, buf)
		})
	}
// §foot page/pkg/egress/socks5_test.go TestSOCKS5TruncatedHandshake