// §head page/pkg/subagent/executor.go:109-142 Pod.Run
// §sig func (o Pod) Run(ctx context.Context, cfg Config) (*Result, error)
	if o.Host == "" {
		return nil, errors.New("pod: no host")
	}
	cfg = cfg.withDefaults()

	ctx, cancel := context.WithTimeout(ctx, cfg.Timeout)
	defer cancel()

	args := append([]string{}, o.Args...)
	args = append(args, o.Host, o.remoteCommand(cfg))

	cmd := exec.CommandContext(ctx, o.command(), args...)
	cmd.Stderr = os.Stderr
	// Killing the transport does not close stdout if a grandchild inherited
	// it, and Output would block on that read forever.  WaitDelay force-closes
	// the pipes shortly after the deadline so a hung pod cannot pin the parent.
	cmd.WaitDelay = time.Second

	out, err := cmd.Output()
	if ctx.Err() != nil {
		return timedOut(cfg), nil
	}

	// A failing subagent exits 1 but still writes its Result, so decode
	// before treating a non-zero exit as a transport error.
	if res, derr := decodeResult(out); derr == nil {
		return res, nil
	} else if err != nil {
		return nil, fmt.Errorf("pod %s: %w", o.Host, err)
	} else {
		return nil, fmt.Errorf("pod %s: %w", o.Host, derr)
	}
// §foot page/pkg/subagent/executor.go Pod.Run