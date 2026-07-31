// §head page/pkg/egress/dial.go:66-71 copyOneWay
// §sig func copyOneWay(dst, src net.Conn)
	io.Copy(dst, src)
	if cw, ok := dst.(interface{ CloseWrite() error }); ok {
		cw.CloseWrite()
	}
// §foot page/pkg/egress/dial.go copyOneWay