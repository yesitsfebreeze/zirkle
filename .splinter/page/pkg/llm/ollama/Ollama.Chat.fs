// §head page/pkg/llm/ollama.go:164-239 Ollama.Chat
// §sig func (o *Ollama) Chat(ctx context.Context, req ChatRequest) (*ChatResponse, error)
	model := req.Model
	if model == "" {
		model = o.Model
	}
	if len(req.Messages) == 0 {
		return nil, fmt.Errorf("ollama: chat request has no messages")
	}
	if model == "" {
		return nil, fmt.Errorf("ollama: chat request model is empty")
	}
	msgs := make([]ollamaMsg, len(req.Messages))
	for i, m := range req.Messages {
		msgs[i] = toOllamaMsg(m)
	}
	body, err := json.Marshal(ollamaReq{
		Model:    model,
		Messages: msgs,
		Stream:   false,
		Tools:    toOllamaTools(req.Tools),
		Options:  ollamaOptions{NumPredict: req.MaxTokens},
	})
	if err != nil {
		return nil, err
	}
	hr, err := http.NewRequestWithContext(ctx, http.MethodPost, o.BaseURL+"/api/chat", bytes.NewReader(body))
	if err != nil {
		return nil, err
	}
	hr.Header.Set("content-type", "application/json")

	resp, err := o.HTTP.Do(hr)
	if err != nil {
		var netErr *net.OpError
		if errors.As(err, &netErr) {
			return nil, fmt.Errorf("ollama: cannot reach %s — is `ollama serve` running? (%w)", o.BaseURL, err)
		}
		return nil, fmt.Errorf("ollama: %w", err)
	}
	defer resp.Body.Close()

	var or ollamaResp
	if err := json.NewDecoder(resp.Body).Decode(&or); err != nil {
		return nil, fmt.Errorf("ollama: decode: %w", err)
	}
	if resp.StatusCode != http.StatusOK {
		if strings.Contains(or.Error, "not found") {
			return nil, fmt.Errorf("ollama: model %q not pulled — run `ollama pull %s`", model, model)
		}
		if or.Error != "" {
			return nil, fmt.Errorf("ollama: HTTP %d: %s", resp.StatusCode, or.Error)
		}
		return nil, fmt.Errorf("ollama: HTTP %d", resp.StatusCode)
	}

	// Small models produce malformed XML tool calls; treat as done.
	if strings.Contains(strings.ToLower(or.Error), "xml syntax error") {
		return &ChatResponse{
			Message: Message{Role: "assistant", Content: or.Message.Content},
			Usage:   Usage{},
		}, nil
	}

	out := &ChatResponse{
		Message: Message{Role: "assistant", Content: or.Message.Content},
		Usage:   Usage{InputTokens: or.PromptEvalCount, OutputTokens: or.EvalCount},
	}
	if len(or.Message.ToolCalls) > 0 {
		tc := or.Message.ToolCalls[0]
		out.Message.ToolUse = &ToolCall{
			Name:  tc.Function.Name,
			Input: tc.Function.Arguments,
		}
	}
	return out, nil
// §foot page/pkg/llm/ollama.go Ollama.Chat