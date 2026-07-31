// §head page/pkg/subagent/subagent.go:98-108 Spawn
// §sig func Spawn(ctx context.Context, cfg Config) (*Result, error)
	executor := cfg.Executor
	if executor == nil {
		var err error
		executor, err = DefaultExecutor()
		if err != nil {
			return nil, err
		}
	}
	return executor.Run(ctx, cfg)
// §foot page/pkg/subagent/subagent.go Spawn