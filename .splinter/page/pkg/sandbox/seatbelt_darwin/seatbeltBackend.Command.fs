// §head page/pkg/sandbox/seatbelt_darwin.go:57-100 seatbeltBackend.Command
// §sig func (seatbeltBackend) Command(ctx context.Context, s Spec, argv ...string) (*exec.Cmd, error)
	if len(argv) == 0 {
		return nil, errors.New("sandbox: empty argv")
	}
	if s.Dir == "" {
		return nil, errors.New("sandbox: no Dir")
	}

	absDir, err := filepath.Abs(s.Dir)
	if err != nil {
		return nil, fmt.Errorf("sandbox: %w", err)
	}
	if err := os.MkdirAll(absDir, 0o700); err != nil {
		return nil, fmt.Errorf("sandbox: %w", err)
	}
	s.Dir = absDir

	for i, p := range s.RW {
		abs, err := filepath.Abs(p)
		if err != nil {
			return nil, fmt.Errorf("sandbox: rw path %q: %w", p, err)
		}
		s.RW[i] = abs
	}

	profile := GenerateSBPL(s)

	// sandbox-exec -p /dev/stdin reads the profile from stdin; -- separates
	// sandbox-exec args from the confined command.
	args := append([]string{"-p", "/dev/stdin", "--"}, argv...)
	cmd := exec.CommandContext(ctx, "sandbox-exec", args...)
	cmd.Stdin = strings.NewReader(profile)
	cmd.WaitDelay = waitDelay

	// Clear the parent env: bwrap uses --clearenv; sandbox-exec has no
	// equivalent, so setting cmd.Env replaces the child's full environment.
	env := s.Env
	if env == nil {
		env = DefaultEnv
	}
	cmd.Env = env

	return cmd, nil
// §foot page/pkg/sandbox/seatbelt_darwin.go seatbeltBackend.Command