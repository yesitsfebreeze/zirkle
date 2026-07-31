// §head page/cmd/relay/main.go:344-450 runDaemon
// §sig func runDaemon(socketPath, whSecret string, whPort int, provider, model string)
	dir, _ := os.UserHomeDir()
	dataDir := filepath.Join(dir, ".relay")
	os.MkdirAll(dataDir, 0o755)
	s, err := store.Open(filepath.Join(dataDir, "relay.db"))
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
	defer s.Close()

	l, err := llm.New(provider, model)
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
	log.Printf("starting relay — store at %s", filepath.Join(dataDir, "relay.db"))

	// Remove stale socket file then listen
	os.Remove(socketPath)
	lis, err := net.Listen("unix", socketPath)
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: socket: %v\n", err)
		os.Exit(1)
	}
	defer lis.Close()
	defer os.Remove(socketPath)

	go func() {
		defer fault.Guard(s, "", "daemon.serve")
		serveDaemon(lis, s, l)
	}()

	cmdr := &podCommander{store: s, llm: l}

	cfgPath, err := config.EnsureDefault()
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: config: %v\n", err)
		os.Exit(1)
	}
	cfg, err := config.Load(cfgPath)
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: config: %v\n", err)
		os.Exit(1)
	}
	var themeColors map[string]string
	if cfg.Theme.Custom {
		themeColors = cfg.Theme.Colors
	}
	timeline := tui.TimelineConfig{
		Enabled:    cfg.Timeline.Enabled,
		Frame:      cfg.Timeline.Frame,
		DayStart:   cfg.Timeline.DayStart,
		ShowCount:  cfg.Timeline.ShowCount,
		ShowStates: cfg.Timeline.ShowStates,
		ShowSpan:   cfg.Timeline.ShowSpan,
	}
	// A tick in the settings screen lands in the user's config file, so the
	// choice survives the next start.
	saveTimeline := func(tl tui.TimelineConfig) error {
		return config.SaveTimeline(cfgPath, config.TimelineConfig{
			Enabled:    tl.Enabled,
			Frame:      tl.Frame,
			DayStart:   tl.DayStart,
			ShowCount:  tl.ShowCount,
			ShowStates: tl.ShowStates,
			ShowSpan:   tl.ShowSpan,
		})
	}

	// Keymap: an absent keys.toml means the tour has never run, and the
	// dashboard opens on it.
	kmPath, err := keymap.DefaultPath()
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: keymap: %v\n", err)
		os.Exit(1)
	}
	km, err := keymap.Load(kmPath)
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}

	ctx, stop := context.WithCancel(context.Background())
	defer stop()
	go func() {
		defer fault.Guard(s, "", "daemon.webhook")
		wh := webhook.New(whSecret, whPort)
		wh.Faults = s
		if err := wh.Run(ctx, func(in adapter.InMessage) {
			_, err := cmdr.Run(ctx, in.Prompt)
			fault.Record(s, "", "webhook.pod", err)
		}); err != nil {
			fault.Record(s, "", "webhook.run", err)
		}
	}()

	// Bubble Tea recovers TUI panics itself and returns them as an error, so
	// fault.Guard never sees them; record the error before exiting or a TUI
	// crash leaves no row behind.
	if err := tui.Run(&podSource{store: s}, cmdr, nil, s, themeColors, km, kmPath, timeline, saveTimeline); err != nil {
		fault.Record(s, "", "daemon.tui", err)
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		s.Close()
		os.Exit(1)
	}
// §foot page/cmd/relay/main.go runDaemon