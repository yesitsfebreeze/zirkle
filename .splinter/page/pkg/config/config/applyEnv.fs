// §head page/pkg/config/config.go:208-223 applyEnv
// §sig func applyEnv(c *Config)
	envMap := map[string]func(string){
		"RELAY_LLM_PROVIDER":   func(v string) { c.LLM.Provider = v },
		"RELAY_LLM_API_KEY":    func(v string) { c.LLM.APIKey = v },
		"RELAY_MODEL":          func(v string) { c.LLM.Model = v },
		"RELAY_WEBHOOK_SECRET": func(v string) { c.Webhook.Secret = v },
		"RELAY_WEBHOOK_PORT":   func(v string) { c.Daemon.Port = atoiOr(v, c.Daemon.Port) },
		"RELAY_STORE_DIR":      func(v string) { c.Store.Dir = v },
		"RELAY_SANDBOX":        func(v string) { c.Sandbox.Mode = v },
	}
	for key, set := range envMap {
		if v, ok := os.LookupEnv(key); ok && v != "" {
			set(v)
		}
	}
// §foot page/pkg/config/config.go applyEnv