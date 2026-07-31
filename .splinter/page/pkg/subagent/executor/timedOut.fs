// §head page/pkg/subagent/executor.go:293-299 timedOut
// §sig func timedOut(cfg Config) *Result
	return &Result{
		Success: false,
		Summary: "subagent timed out",
		Output:  fmt.Sprintf("subagent %q timed out after %v", cfg.Prompt, cfg.Timeout),
	}
// §foot page/pkg/subagent/executor.go timedOut