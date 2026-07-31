// §head page/pkg/egress/policy.go:171-187 match
// §sig func match(pattern, host string) bool
	pattern = strings.ToLower(strings.TrimSpace(pattern))
	if pattern == "" {
		return false
	}
	if suffix, wild := strings.CutPrefix(pattern, "*."); wild {
		// A wildcard never matches an IP literal.  "*.1" swallowing
		// 192.168.0.1 would turn one allowlist entry into the LAN.
		if _, isIP := parseIP(host); isIP {
			return false
		}
		suffix, ok := canonical(suffix)
		return ok && strings.HasSuffix(host, "."+suffix)
	}
	p, ok := canonical(pattern)
	return ok && p == host
// §foot page/pkg/egress/policy.go match