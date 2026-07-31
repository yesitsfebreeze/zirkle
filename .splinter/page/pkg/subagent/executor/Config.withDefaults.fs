// §head page/pkg/subagent/executor.go:388-393 Config.withDefaults
// §sig func (c Config) withDefaults() Config
	if c.Timeout == 0 {
		c.Timeout = 60 * time.Second
	}
	return c
// §foot page/pkg/subagent/executor.go Config.withDefaults