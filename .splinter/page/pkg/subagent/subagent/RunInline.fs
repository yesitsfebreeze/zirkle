// §head page/pkg/subagent/subagent.go:114-127 RunInline
// §sig func RunInline(ctx context.Context, cfg Config) (*Result, error)
	// Test mode — same canned result as the subprocess path, so tests that
	// set RELAY_SUBAGENT_RUN=1 don't need a real model pulled.
	if os.Getenv("RELAY_SUBAGENT_RUN") == "1" {
		return &Result{Success: true, Summary: "test summary", Output: "test output", Tokens: 50}, nil
	}
	cfg = cfg.withDefaults()
	l, err := llm.New("", cfg.Model)
	if err != nil {
		return &Result{Success: false, Summary: err.Error(), Output: err.Error()}, nil
	}
	res := runSubpodLoop(ctx, l, cfg.Model, cfg.Prompt, cfg.MaxTokens, cfg.ToolOptional)
	return &res, nil
// §foot page/pkg/subagent/subagent.go RunInline