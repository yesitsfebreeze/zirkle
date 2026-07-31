// §head page/pkg/egress/policy.go:119-150 parseInetAton
// §sig func parseInetAton(host string) (netip.Addr, bool)
	parts := strings.Split(host, ".")
	if len(parts) > 4 {
		return netip.Addr{}, false
	}
	vals := make([]uint64, 0, 4)
	for _, part := range parts {
		v, ok := parseIPPart(part)
		if !ok {
			return netip.Addr{}, false
		}
		vals = append(vals, v)
	}
	n := len(vals)
	var addr uint64
	for i, v := range vals {
		if i == n-1 {
			if v >= 1<<(32-8*uint(i)) {
				return netip.Addr{}, false
			}
			addr |= v
			break
		}
		if v > 0xff {
			return netip.Addr{}, false
		}
		addr |= v << (32 - 8*uint(i+1))
	}
	return netip.AddrFrom4([4]byte{
		byte(addr >> 24), byte(addr >> 16), byte(addr >> 8), byte(addr),
	}), true
// §foot page/pkg/egress/policy.go parseInetAton