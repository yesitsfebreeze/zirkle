// §head page/pkg/egress/policy.go:37-53 Policy.Allow
// §sig func (p *Policy) Allow(host string) bool
	h, ok := canonical(host)
	if !ok {
		return false
	}
	for _, pattern := range p.DeniedDomains {
		if match(pattern, h) {
			return false
		}
	}
	for _, pattern := range p.AllowedDomains {
		if match(pattern, h) {
			return true
		}
	}
	return false
// §foot page/pkg/egress/policy.go Policy.Allow