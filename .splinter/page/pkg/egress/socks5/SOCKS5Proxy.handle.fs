// §head page/pkg/egress/socks5.go:63-110 SOCKS5Proxy.handle
// §sig func (px *SOCKS5Proxy) handle(conn net.Conn)
	defer conn.Close()

	if err := conn.SetDeadline(time.Now().Add(handshakeDeadline)); err != nil {
		return
	}

	if !px.authNegotiate(conn) {
		return
	}

	addr, rep := px.command(conn)
	if rep != socksRepSuccess {
		conn.SetDeadline(time.Time{})
		px.reply(conn, rep)
		return
	}

	if err := conn.SetDeadline(time.Time{}); err != nil {
		return
	}

	// Reply success so the client begins TLS (or sends tunnel data).
	// The proxy then peeks at the first bytes for a TLS ClientHello and
	// validates its SNI before opening any upstream connection.
	if err := px.replyBound(conn, socksRepSuccess, "0.0.0.0", 0); err != nil {
		return
	}

	peeked, sni, err := peekSNI(conn)
	if err != nil && !errors.Is(err, ErrNotTLS) && !errors.Is(err, ErrNoSNI) {
		return
	}
	if sni != "" && !px.policy.Allow(sni) {
		return
	}

	upstream, err := px.policy.Dial(context.Background(), addr)
	if err != nil {
		return
	}
	defer upstream.Close()

	if len(peeked) > 0 {
		upstream.Write(peeked)
	}
	Relay(conn, upstream)
// §foot page/pkg/egress/socks5.go SOCKS5Proxy.handle