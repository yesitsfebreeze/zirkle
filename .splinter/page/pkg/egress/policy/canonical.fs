// §head page/pkg/egress/policy.go:62-83 canonical
// §sig func canonical(host string) (string, bool)
	if host == "" {
		return "", false
	}
	for _, r := range host {
		if r < 0x20 || r == 0x7f {
			return "", false
		}
	}
	host = strings.TrimSuffix(stripPort(host), ".")
	if host == "" {
		return "", false
	}
	host = strings.ToLower(host)
	if ip, ok := parseIP(host); ok {
		return ip.Unmap().String(), true
	}
	if strings.ContainsAny(host, " \t*/@?#\\[]") {
		return "", false
	}
	return host, true
// §foot page/pkg/egress/policy.go canonical