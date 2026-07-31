// §head page/pkg/egress/dial.go:32-43 Policy.Dial
// §sig func (p *Policy) Dial(ctx context.Context, addr string) (net.Conn, error)
	host, port, err := net.SplitHostPort(addr)
	if err != nil {
		return nil, fmt.Errorf("egress: bad address %q: %w", addr, err)
	}
	if !p.Allow(host) {
		return nil, fmt.Errorf("%w: %s", ErrDenied, host)
	}
	var d net.Dialer
	d.Timeout = dialTimeout
	return d.DialContext(ctx, "tcp", net.JoinHostPort(host, port))
// §foot page/pkg/egress/dial.go Policy.Dial