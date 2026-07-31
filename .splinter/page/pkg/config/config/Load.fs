// §head page/pkg/config/config.go:190-205 Load
// §sig func Load(path string) (Config, error)
	c := Default()

	// Config file — missing file is fine, a parse error is not.
	if path != "" {
		if _, err := os.Stat(path); err == nil {
			if _, err := toml.DecodeFile(path, &c); err != nil {
				return Config{}, fmt.Errorf("config: %w", err)
			}
		}
	}

	applyEnv(&c)
	expandHome(&c)
	return c, nil
// §foot page/pkg/config/config.go Load