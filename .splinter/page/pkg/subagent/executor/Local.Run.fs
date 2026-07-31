// §head page/pkg/subagent/executor.go:35-92 Local.Run
// §sig func (Local) Run(ctx context.Context, cfg Config) (*Result, error)
	cfg = cfg.withDefaults()

	// Create pipe: parent reads r, child writes to fd 3 (w).
	r, w, err := os.Pipe()
	if err != nil {
		return nil, fmt.Errorf("subagent pipe: %w", err)
	}

	ctx, cancel := context.WithTimeout(ctx, cfg.Timeout)
	defer cancel()

	cmd := exec.CommandContext(ctx, os.Args[0], subagentArgs(cfg)...)
	cmd.ExtraFiles = []*os.File{w} // becomes fd 3 in child
	cmd.Stderr = os.Stderr
	cmd.Env = os.Environ() // inherit parent env (including test vars)

	if err := cmd.Start(); err != nil {
		r.Close()
		w.Close()
		return nil, fmt.Errorf("subagent start: %w", err)
	}
	w.Close() // parent never writes to the pipe

	// Read Result from pipe in a goroutine so we can honour timeout.
	type pipeResult struct {
		result *Result
		err    error
	}
	ch := make(chan pipeResult, 1)
	go func() {
		defer fault.Guard(nil, "", "subagent.decode")
		var res Result
		if err := json.NewDecoder(r).Decode(&res); err != nil {
			ch <- pipeResult{err: fmt.Errorf("subagent decode: %w", err)}
			return
		}
		ch <- pipeResult{result: &res}
	}()

	select {
	case <-ctx.Done():
		// Kill on timeout and return a partial/failure result.
		cmd.Process.Kill()
		cmd.Wait()
		r.Close()
		return timedOut(cfg), nil

	case pr := <-ch:
		r.Close()
		// Ignore Wait error if the process was already reaped.
		cmd.Wait()
		if pr.err != nil {
			return nil, pr.err
		}
		return pr.result, nil
	}
// §foot page/pkg/subagent/executor.go Local.Run