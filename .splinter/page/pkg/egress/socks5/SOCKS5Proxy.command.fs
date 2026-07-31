// §head page/pkg/egress/socks5.go:129-145 SOCKS5Proxy.command
// §sig func (px *SOCKS5Proxy) command(r io.Reader) (string, byte)
	ver, cmd, err := readByte(r, 3)
	if err != nil || ver != socksVer5 {
		return "", socksRepGeneralFailure
	}
	if cmd != socksCmdConnect {
		return "", socksRepCommandNotSupported
	}
	host, port, err := px.readAddr(r)
	if errors.Is(err, ErrDenied) {
		return "", socksRepConnectionNotAllowed
	}
	if err != nil {
		return "", socksRepGeneralFailure
	}
	return net.JoinHostPort(host, fmt.Sprint(port)), socksRepSuccess
// §foot page/pkg/egress/socks5.go SOCKS5Proxy.command