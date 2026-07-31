// §head page/pkg/hotreload/hotreload.go:151-259 Supervise
// §sig func Supervise(args []string) error
	goBin := findGoBinary()

	tmpDir, err := os.MkdirTemp("", "relay-dev-*")
	if err != nil {
		return fmt.Errorf("dev hotreload: mkdir temp: %w", err)
	}
	defer os.RemoveAll(tmpDir)

	binaryPath := filepath.Join(tmpDir, "relay-dev")
	if runtime.GOOS == "windows" {
		binaryPath += ".exe"
	}

	fmt.Printf("[DEV HOT RELOAD] Supervisor starting — watching .go files...\n")

	if err := buildBinary(goBin, binaryPath); err != nil {
		fmt.Fprintf(os.Stderr, "[DEV HOT RELOAD] Initial build failed: %v\n", err)
	}

	cfg := Config{RootDir: "."}
	watcher := NewWatcher(cfg)
	events := make(chan struct{}, 1)

	ctx, cancel := context.WithCancel(context.Background())
	defer cancel()

	go watcher.Watch(ctx, events)

	sigCh := make(chan os.Signal, 1)
	signal.Notify(sigCh, os.Interrupt, syscall.SIGTERM)

	var childCmd *exec.Cmd
	var childMu sync.Mutex

	startChild := func() {
		childMu.Lock()
		defer childMu.Unlock()

		if _, err := os.Stat(binaryPath); err != nil {
			fmt.Fprintf(os.Stderr, "[DEV HOT RELOAD] Binary not found, waiting for valid build...\n")
			return
		}

		cmd := exec.Command(binaryPath, args...)
		cmd.Stdin = os.Stdin
		cmd.Stdout = os.Stdout
		cmd.Stderr = os.Stderr
		cmd.Env = append(os.Environ(), "RELAY_DEV_CHILD=1")

		if err := cmd.Start(); err != nil {
			fmt.Fprintf(os.Stderr, "[DEV HOT RELOAD] Failed to start child: %v\n", err)
			return
		}
		childCmd = cmd
		fmt.Printf("[DEV HOT RELOAD] Started child process (PID %d)\n", cmd.Process.Pid)
	}

	stopChild := func() {
		childMu.Lock()
		cmd := childCmd
		childCmd = nil
		childMu.Unlock()

		if cmd != nil && cmd.Process != nil {
			_ = cmd.Process.Signal(os.Interrupt)

			done := make(chan struct{})
			go func() {
				_ = cmd.Wait()
				close(done)
			}()

			select {
			case <-done:
			case <-time.After(2 * time.Second):
				_ = cmd.Process.Kill()
			}
		}
	}

	startChild()

	for {
		select {
		case sig := <-sigCh:
			fmt.Printf("\n[DEV HOT RELOAD] Received signal %v, shutting down...\n", sig)
			stopChild()
			return nil

		case <-events:
			time.Sleep(250 * time.Millisecond)
			for len(events) > 0 {
				<-events
			}

			fmt.Println("[DEV HOT RELOAD] File change detected, rebuilding...")
			if err := buildBinary(goBin, binaryPath); err != nil {
				fmt.Fprintf(os.Stderr, "[DEV HOT RELOAD] Build failed:\n%v\n", err)
				fmt.Println("[DEV HOT RELOAD] Keeping existing process running. Fix build to reload.")
				continue
			}

			fmt.Println("[DEV HOT RELOAD] Build successful! Restarting process...")
			stopChild()
			startChild()
		}
	}
// §foot page/pkg/hotreload/hotreload.go Supervise