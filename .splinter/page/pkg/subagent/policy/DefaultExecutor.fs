// §head page/pkg/subagent/policy.go:66-100 DefaultExecutor
// §sig func DefaultExecutor() (Executor, error)
	if Unconfined() {
		warnUnconfined.Do(func() {
			fmt.Fprintf(os.Stderr,
				"relay: %s=off — subagents run unconfined on this machine\n", EnvSandbox)
		})
		return Local{}, nil
	}

	if err := sandbox.Probe(); err != nil {
		return nil, fmt.Errorf("%w (or set %s=off to run unconfined)", err, EnvSandbox)
	}

	var env []string
	for _, key := range forwardedEnv {
		if v := os.Getenv(key); v != "" {
			env = append(env, key+"="+v)
		}
	}

	spec := DefaultSpec()

	// The subpod's job includes learning the pod library: searching shards,
	// reading them, and writing improved .pod files back. Mount the host
	// store RW and point RELAY_DATA_DIR at it so the CLI inside the sandbox
	// lands on the real library instead of an empty ephemeral one.  This is
	// a deliberate hole in the wall — it is the whole point of the worker.
	relayDir := comp.DataDir()
	if mkErr := os.MkdirAll(relayDir, 0o755); mkErr == nil {
		spec.RW = append(spec.RW, relayDir)
		env = append(env, "RELAY_DATA_DIR="+relayDir)
	}

	return Sandboxed{Spec: spec, Env: env}, nil
// §foot page/pkg/subagent/policy.go DefaultExecutor