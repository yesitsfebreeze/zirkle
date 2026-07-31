// §head page/pkg/sandbox/seatbelt_darwin.go:32-55 seatbeltBackend.Probe
// §sig func (seatbeltBackend) Probe() error
	if _, err := exec.LookPath("sandbox-exec"); err != nil {
		return fmt.Errorf("%w: sandbox-exec not found — ships with macOS, should be in /usr/bin", ErrUnavailable)
	}

	dir, err := os.MkdirTemp("", "relay-probe-")
	if err != nil {
		return fmt.Errorf("%w: %v", ErrUnavailable, err)
	}
	defer os.RemoveAll(dir)

	ctx, cancel := context.WithTimeout(context.Background(), 10*time.Second)
	defer cancel()

	cmd, err := Spec{Dir: dir}.Command(ctx, "/usr/bin/true")
	if err != nil {
		return fmt.Errorf("%w: %v", ErrUnavailable, err)
	}
	if out, err := cmd.CombinedOutput(); err != nil {
		return fmt.Errorf("%w: sandbox-exec failed (%v): %s",
			ErrUnavailable, err, strings.TrimSpace(string(out)))
	}
	return nil
// §foot page/pkg/sandbox/seatbelt_darwin.go seatbeltBackend.Probe