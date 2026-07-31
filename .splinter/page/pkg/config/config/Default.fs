// §head page/pkg/config/config.go:103-151 Default
// §sig func Default() Config
	return Config{
		Daemon: DaemonConfig{
			Port:   9842,
			Socket: "/tmp/relay.sock",
		},
		LLM: LLMConfig{
			Provider:  "ollama",
			Model:     "glm-5.2:cloud",
			MaxTokens: 100000,
		},
		Store: StoreConfig{
			Dir: "~/.relay",
		},
		Sched: SchedConfig{
			Interval: 30,
		},
		Sandbox: SandboxConfig{
			Mode:      "on",
			SizeMB:    256,
			Ephemeral: true,
		},
		Log: LogConfig{
			Level: "info",
			JSON:  true,
		},
		Theme: ThemeConfig{
			Custom: true,
			Colors: map[string]string{
				"foreground": "#FFFFFF",
				"primary":    "#BA8CFF",
				"attention":  "#2F80ED",
				"muted":      "#737575",
				"secondary":  "#A2B1B1",
				"failure":    "#FF3D81",
				"surface":    "#121212",
				"rule":       "#737575",
			},
		},
		Timeline: TimelineConfig{
			Enabled:    true,
			Frame:      "day",
			DayStart:   "00:00",
			ShowCount:  true,
			ShowStates: true,
			ShowSpan:   true,
		},
	}
// §foot page/pkg/config/config.go Default