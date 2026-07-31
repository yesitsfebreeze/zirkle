// §head page/pkg/config/config.go:227-233 expandHome
// §sig func expandHome(c *Config)
	home, err := os.UserHomeDir()
	if err != nil {
		return
	}
	c.Store.Dir = expandTilde(c.Store.Dir, home)
// §foot page/pkg/config/config.go expandHome