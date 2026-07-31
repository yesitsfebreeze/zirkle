// §head page/pkg/egress/policy.go:109-114 parseIP
// §sig func parseIP(host string) (netip.Addr, bool)
	if addr, err := netip.ParseAddr(host); err == nil {
		return addr, true
	}
	return parseInetAton(host)
// §foot page/pkg/egress/policy.go parseIP