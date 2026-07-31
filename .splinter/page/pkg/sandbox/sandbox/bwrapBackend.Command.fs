// §head page/pkg/sandbox/sandbox.go:160-187 bwrapBackend.Command
// §sig func (bwrapBackend) Command(ctx context.Context, s Spec, argv ...string) (*exec.Cmd, error)
	if len(argv) == 0 {
		return nil, errors.New("sandbox: empty argv")
	}
	if s.Dir == "" {
		return nil, errors.New("sandbox: no Dir")
	}
	abs, err := filepath.Abs(s.Dir)
	if err != nil {
		return nil, fmt.Errorf("sandbox: %w", err)
	}
	if err := os.MkdirAll(abs, 0o700); err != nil {
		return nil, fmt.Errorf("sandbox: %w", err)
	}

	args, err := s.bwrapArgs(abs)
	if err != nil {
		return nil, err
	}
	args = append(args, "--")
	args = append(args, argv...)

	cmd := exec.CommandContext(ctx, "bwrap", args...)
	// bwrap holds the sandbox's stdio; a grandchild inside can keep those
	// pipes open past a kill, so bound the wait rather than hang on it.
	cmd.WaitDelay = waitDelay
	return cmd, nil
// §foot page/pkg/sandbox/sandbox.go bwrapBackend.Command