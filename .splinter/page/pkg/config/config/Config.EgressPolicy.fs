// §head page/pkg/config/config.go:256-261 Config.EgressPolicy
// §sig func (c *Config) EgressPolicy() *egress.Policy
	return &egress.Policy{
		AllowedDomains: c.Sandbox.AllowedDomains,
		DeniedDomains:  c.Sandbox.DeniedDomains,
	}
// §foot page/pkg/config/config.go Config.EgressPolicy