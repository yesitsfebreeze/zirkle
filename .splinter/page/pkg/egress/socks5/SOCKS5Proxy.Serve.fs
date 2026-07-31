// §head page/pkg/egress/socks5.go:53-61 SOCKS5Proxy.Serve
// §sig func (px *SOCKS5Proxy) Serve(l net.Listener) error
	for {
		conn, err := l.Accept()
		if err != nil {
			return err
		}
		go px.handle(conn)
	}
// §foot page/pkg/egress/socks5.go SOCKS5Proxy.Serve