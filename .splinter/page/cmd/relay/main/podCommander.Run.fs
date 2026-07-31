// §head page/cmd/relay/main.go:143-145 podCommander.Run
// §sig func (c *podCommander) Run(ctx context.Context, prompt string) (out string, rerr error)
	return c.runWithStream(ctx, prompt, nil)
// §foot page/cmd/relay/main.go podCommander.Run