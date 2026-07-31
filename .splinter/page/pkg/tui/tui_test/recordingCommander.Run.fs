// §head page/pkg/tui/tui_test.go:263-268 recordingCommander.Run
// §sig func (c *recordingCommander) Run(ctx context.Context, prompt string) (string, error)
	c.mu.Lock()
	defer c.mu.Unlock()
	c.prompts = append(c.prompts, prompt)
	return "ok", nil
// §foot page/pkg/tui/tui_test.go recordingCommander.Run