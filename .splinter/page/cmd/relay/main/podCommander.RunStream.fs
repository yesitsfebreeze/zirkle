// §head page/cmd/relay/main.go:147-149 podCommander.RunStream
// §sig func (c *podCommander) RunStream(ctx context.Context, prompt string, events chan<- llm.StreamEvent) (string, error)
	return c.runWithStream(ctx, prompt, events)
// §foot page/cmd/relay/main.go podCommander.RunStream