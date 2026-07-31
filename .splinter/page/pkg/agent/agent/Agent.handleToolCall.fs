// §head page/pkg/agent/agent.go:68-132 Agent.handleToolCall
// §sig func (a *Agent) handleToolCall(ctx context.Context, call *llm.ToolCall) string
	if call.Name != "spawn" {
		return fmt.Sprintf("ERROR: unknown tool %q", call.Name)
	}
	prompt, _ := call.Input["prompt"].(string)
	if prompt == "" {
		return "ERROR: spawn requires a non-empty prompt"
	}

	// Timeout stays zero on purpose — subagent.Spawn owns the default.
	// Naming a duration here once cost 60ns instead of 60s.
	cfg := subagent.Config{
		Prompt:   prompt,
		ParentID: a.ID,
		Model:    a.Model,
	}
	if a.Budget > 0 {
		cfg.MaxTokens = a.Budget - a.tokens
	}

	// Allow overrides from the Subagents map (keyed by prompt).
	if a.Subagents != nil {
		if override, ok := a.Subagents[prompt]; ok {
			if override.Timeout > 0 {
				cfg.Timeout = override.Timeout
			}
			if override.Model != "" {
				cfg.Model = override.Model
			}
			if override.MaxTokens > 0 {
				cfg.MaxTokens = override.MaxTokens
			}
		}
	}

	// Inline by default: run the subpod loop in-process.  No bwrap, no binary
	// re-exec — just Go.  Spawn (subprocess + sandbox) is opt-in via Executor.
	var result *subagent.Result
	var err error
	if cfg.Executor != nil {
		result, err = subagent.Spawn(ctx, cfg)
	} else {
		result, err = subagent.RunInline(ctx, cfg)
	}
	if err != nil {
		return fmt.Sprintf("SPAWN ERROR: %v", err)
	}
	// Execution is memory: record every subpod run so any pod in the
	// workspace can later search what was done and how it went.
	if a.Store != nil {
		_ = a.Store.RecordExecution(&store.Execution{
			ParentID: a.ID,
			Prompt:   prompt,
			Summary:  result.Summary,
			Output:   result.Output,
			Success:  result.Success,
			Tokens:   result.Tokens,
			Model:    cfg.Model,
		})
	}
	if !result.Success {
		return fmt.Sprintf("SPAWN FAILED: %s\n%s", result.Summary, result.Output)
	}
	return fmt.Sprintf("SPAWN OK: %s\n%s", result.Summary, result.Output)
// §foot page/pkg/agent/agent.go Agent.handleToolCall