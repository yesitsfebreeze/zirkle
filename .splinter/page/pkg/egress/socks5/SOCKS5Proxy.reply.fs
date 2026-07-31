// §head page/pkg/egress/socks5.go:200-202 SOCKS5Proxy.reply
// §sig func (px *SOCKS5Proxy) reply(w io.Writer, rep byte)
	w.Write([]byte{socksVer5, rep, 0, socksAtypIPv4, 0, 0, 0, 0, 0, 0})
// §foot page/pkg/egress/socks5.go SOCKS5Proxy.reply