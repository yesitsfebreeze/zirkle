// §head page/pkg/egress/policy.go:88-103 stripPort
// §sig func stripPort(host string) string
	if strings.HasPrefix(host, "[") {
		if end := strings.Index(host, "]"); end >= 0 {
			return host[1:end]
		}
		return host
	}
	i := strings.LastIndex(host, ":")
	if i < 0 || strings.Contains(host[:i], ":") {
		return host
	}
	if _, err := strconv.ParseUint(host[i+1:], 10, 16); err != nil {
		return host
	}
	return host[:i]
// §foot page/pkg/egress/policy.go stripPort