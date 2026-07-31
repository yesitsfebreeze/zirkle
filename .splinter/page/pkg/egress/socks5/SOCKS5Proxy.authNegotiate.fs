// §head page/pkg/egress/socks5.go:112-127 SOCKS5Proxy.authNegotiate
// §sig func (px *SOCKS5Proxy) authNegotiate(rw io.ReadWriter) bool
	ver, nmethods, err := readByte(rw, 2)
	if err != nil || ver != socksVer5 || nmethods == 0 {
		return false
	}
	methods, err := readBytes(rw, int(nmethods))
	if err != nil {
		return false
	}
	if !contains(methods, socksAuthNone) {
		rw.Write([]byte{socksVer5, 0xFF})
		return false
	}
	rw.Write([]byte{socksVer5, socksAuthNone})
	return true
// §foot page/pkg/egress/socks5.go SOCKS5Proxy.authNegotiate