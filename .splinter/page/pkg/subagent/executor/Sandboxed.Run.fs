// §head page/pkg/subagent/executor.go:157-239 Sandboxed.Run
// §sig func (s Sandboxed) Run(ctx context.Context, cfg Config) (*Result, error)
	if err := sandbox.Probe(); err != nil {
		return nil, err
	}
	cfg = cfg.withDefaults()

	binary, cleanupBinary, err := resolveBinary(s.Binary)
	if err != nil {
		return nil, fmt.Errorf("sandboxed: %w", err)
	}
	defer cleanupBinary()

	spec := s.Spec

	// Start egress proxies when the spec carries an egress policy.  Must
	// happen before tools/env setup so the socket dir is bind-mounted.
	var egressCleanup func()
	if spec.Egress != nil {
		var err error
		egressCleanup, err = sandbox.StartEgress(&spec, spec.Egress)
		if err != nil {
			return nil, fmt.Errorf("sandboxed: %w", err)
		}
		defer func() {
			if egressCleanup != nil {
				egressCleanup()
			}
		}()
	}

	// The binary is a tool like any other: read-only, at its host path, so
	// the same path works as argv inside.
	tools := spec.Tools
	if tools == nil {
		tools = sandbox.DefaultTools
	}
	spec.Tools = append(append([]string{}, tools...), binary)

	env := spec.Env
	if env == nil {
		env = sandbox.DefaultEnv
	}
	spec.Env = append(append([]string{}, env...), "RELAY_RESULT_STDOUT=1")
	spec.Env = append(spec.Env, s.Env...)

	if spec.Dir == "" {
		dir, mkErr := os.MkdirTemp("", "relay-sandbox-")
		if mkErr != nil {
			return nil, fmt.Errorf("sandboxed: %w", mkErr)
		}
		defer os.RemoveAll(dir)
		spec.Dir = dir
	}

	ctx, cancel := context.WithTimeout(ctx, cfg.Timeout)
	defer cancel()

	cmd, err := spec.Command(ctx, append([]string{binary}, subagentArgs(cfg)...)...)
	if err != nil {
		return nil, fmt.Errorf("sandboxed: %w", err)
	}
	// CombinedOutput so stderr is captured, not lost to the terminal.
	out, runErr := cmd.CombinedOutput()
	if ctx.Err() != nil {
		return timedOut(cfg), nil
	}

	if res, derr := decodeResult(out); derr == nil {
		// Prepend raw subprocess output so the operator sees everything.
		if len(out) > 0 {
			res.Output = string(out) + "\n" + res.Output
		}
		return res, nil
	} else if runErr != nil {
		tail := out
		if len(tail) > 2000 {
			tail = tail[len(tail)-2000:]
		}
		return nil, fmt.Errorf("sandboxed: %w\n%s", runErr, strings.TrimSpace(string(tail)))
	} else {
		return nil, fmt.Errorf("sandboxed: %w", derr)
	}
// §foot page/pkg/subagent/executor.go Sandboxed.Run