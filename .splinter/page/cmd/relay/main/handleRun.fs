// §head page/cmd/relay/main.go:508-550 handleRun
// §sig func handleRun(conn net.Conn, id int, params map[string]any, s store.Store, l llm.LLM)
	prompt, _ := params["prompt"].(string)
	if prompt == "" {
		sendStream(conn, id, "error", "missing prompt")
		return
	}

	ctx := context.Background()
	podID := fmt.Sprintf("pod-%x", time.Now().UnixNano())
	if err := s.Create(podID, prompt, "smart"); err != nil {
		sendStream(conn, id, "error", err.Error())
		return
	}

	a := &agent.Agent{
		ID: podID, Prompt: prompt, Mode: "smart",
		Budget: 100000,
		LLM:    l, Store: s,
	}

	resp, err := a.Run(ctx)
	if err != nil {
		o, _ := s.Load(podID)
		if o != nil {
			o.State = "failed"
			s.Save(o)
		}
		sendStream(conn, id, "error", err.Error())
		return
	}

	o, _ := s.Load(podID)
	if o != nil {
		o.State = "done"
		o.Recap = a.Recap
		s.Save(o)
	}

	for _, line := range strings.Split(resp, "\n") {
		sendStream(conn, id, "line", line)
	}
	sendStream(conn, id, "done", "")
// §foot page/cmd/relay/main.go handleRun