// §head page/pkg/egress/socks5.go:204-219 SOCKS5Proxy.replyBound
// §sig func (px *SOCKS5Proxy) replyBound(w io.Writer, rep byte, host string, port uint16) error
	bnd := net.ParseIP(host).To4()
	if bnd == nil {
		_, err := w.Write([]byte{socksVer5, rep, 0, socksAtypIPv4, 0, 0, 0, 0, 0, 0})
		return err
	}
	buf := make([]byte, 10)
	buf[0] = socksVer5
	buf[1] = rep
	buf[2] = 0
	buf[3] = socksAtypIPv4
	copy(buf[4:8], bnd)
	binary.BigEndian.PutUint16(buf[8:10], port)
	_, err := w.Write(buf)
	return err
// §foot page/pkg/egress/socks5.go SOCKS5Proxy.replyBound