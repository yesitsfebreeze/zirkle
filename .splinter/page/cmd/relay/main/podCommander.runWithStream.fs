// §head page/cmd/relay/main.go:151-195 podCommander.runWithStream
// §sig func (c *podCommander) runWithStream(ctx context.Context, prompt string, events chan<- llm.StreamEvent) (out string, rerr error)
	id := fmt.Sprintf("pod-%x", time.Now().UnixNano())
	c.store.Create(id, prompt, "smart")

	// A panic in the agent loop must leave the pod marked failed and a fault
	// on disk, not a row stuck in 'running' and a dead daemon. Recovering here
	// rather than re-panicking keeps one bad pod from taking the process down.
	defer func() {
		if r := recover(); r != nil {
			stack := string(debug.Stack())
			msg := fmt.Sprint(r)
			fault.Recovered(c.store, id, "pod.run", msg, stack)
			if o, _ := c.store.Load(id); o != nil {
				o.State = "failed"
				o.Recap = "panic: " + msg
				c.store.Save(o)
			}
			out, rerr = "", fmt.Errorf("pod %s panicked: %s", id, msg)
		}
	}()

	// Model empty: the configured provider applies its own default.
	a := &agent.Agent{
		ID: id, Prompt: prompt, Mode: "smart",
		Budget: 100000,
		LLM:    c.llm, Store: c.store,
	}
	resp, err := a.RunStream(ctx, events)
	if err != nil {
		fault.Record(c.store, id, "pod.run", err)
		if o, _ := c.store.Load(id); o != nil {
			o.State = "failed"
			o.Recap = "error: " + err.Error()
			c.store.Save(o)
		}
		return "", err
	}
	o, _ := c.store.Load(id)
	if o != nil {
		o.State = "done"
		o.Recap = a.Recap
		c.store.Save(o)
	}
	return resp, nil
// §foot page/cmd/relay/main.go podCommander.runWithStream