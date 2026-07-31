// §head page/pkg/egress/dial.go:53-61 Relay
// §sig func Relay(a, b net.Conn)
	done := make(chan struct{}, 2)
	go func() { copyOneWay(a, b); done <- struct{}{} }()
	go func() { copyOneWay(b, a); done <- struct{}{} }()
	<-done
	a.Close()
	b.Close()
	<-done
// §foot page/pkg/egress/dial.go Relay