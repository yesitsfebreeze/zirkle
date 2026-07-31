// §head page/pkg/subagent/executor.go:276-289 subagentArgs
// §sig func subagentArgs(cfg Config) []string
	args := []string{
		"--subagent",
		"--parent", cfg.ParentID,
		"--task", cfg.Prompt,
	}
	if cfg.Model != "" {
		args = append(args, "--model", cfg.Model)
	}
	if cfg.MaxTokens > 0 {
		args = append(args, "--max-tokens", strconv.Itoa(cfg.MaxTokens))
	}
	return args
// §foot page/pkg/subagent/executor.go subagentArgs