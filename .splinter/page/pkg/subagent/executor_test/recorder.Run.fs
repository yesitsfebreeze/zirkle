// §head page/pkg/subagent/executor_test.go:31-35 recorder.Run
// §sig func (r *recorder) Run(ctx context.Context, cfg Config) (*Result, error)
	r.called = true
	r.got = cfg
	return &Result{Success: true, Summary: "recorded"}, nil
// §foot page/pkg/subagent/executor_test.go recorder.Run