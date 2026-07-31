// §head page/pkg/llm/ollama.go:244-354 Ollama.ChatStream
// §sig func (o *Ollama) ChatStream(ctx context.Context, req ChatRequest) <-chan StreamEvent
	ch := make(chan StreamEvent, 64)
	go func() {
		defer close(ch)
		model := req.Model
		if model == "" {
			model = o.Model
		}
		if len(req.Messages) == 0 {
			ch <- StreamEvent{Done: true, Err: fmt.Errorf("ollama: chat request has no messages")}
			return
		}
		if model == "" {
			ch <- StreamEvent{Done: true, Err: fmt.Errorf("ollama: chat request model is empty")}
			return
		}
		msgs := make([]ollamaMsg, len(req.Messages))
		for i, m := range req.Messages {
			msgs[i] = toOllamaMsg(m)
		}
		body, err := json.Marshal(ollamaReq{
			Model:    model,
			Messages: msgs,
			Stream:   true,
			Tools:    toOllamaTools(req.Tools),
			Options:  ollamaOptions{NumPredict: req.MaxTokens},
		})
		if err != nil {
			ch <- StreamEvent{Done: true, Err: err}
			return
		}
		hr, err := http.NewRequestWithContext(ctx, http.MethodPost, o.BaseURL+"/api/chat", bytes.NewReader(body))
		if err != nil {
			ch <- StreamEvent{Done: true, Err: err}
			return
		}
		hr.Header.Set("content-type", "application/json")

		resp, err := o.HTTP.Do(hr)
		if err != nil {
			var netErr *net.OpError
			if errors.As(err, &netErr) {
				ch <- StreamEvent{Done: true, Err: fmt.Errorf("ollama: cannot reach %s — is `ollama serve` running? (%w)", o.BaseURL, err)}
				return
			}
			ch <- StreamEvent{Done: true, Err: fmt.Errorf("ollama: %w", err)}
			return
		}
		defer resp.Body.Close()

		if resp.StatusCode != http.StatusOK {
			var or ollamaStreamResp
			json.NewDecoder(resp.Body).Decode(&or)
			if strings.Contains(or.Error, "not found") {
				ch <- StreamEvent{Done: true, Err: fmt.Errorf("ollama: model %q not pulled — run `ollama pull %s`", model, model)}
				return
			}
			if or.Error != "" {
				ch <- StreamEvent{Done: true, Err: fmt.Errorf("ollama: HTTP %d: %s", resp.StatusCode, or.Error)}
				return
			}
			ch <- StreamEvent{Done: true, Err: fmt.Errorf("ollama: HTTP %d", resp.StatusCode)}
			return
		}

		dec := json.NewDecoder(resp.Body)
		for {
			var chunk ollamaStreamResp
			if err := dec.Decode(&chunk); err != nil {
				if err == io.EOF {
					break
				}
				ch <- StreamEvent{Done: true, Err: fmt.Errorf("ollama: stream decode: %w", err)}
				return
			}
			if chunk.Error != "" {
				// Small models (qwen3.5:0.8b, llama3.2:3b) often output valid
				// text but produce malformed XML tool calls.  Treat the stream
				// as done — the content already emitted is the real answer.
				// ponytail: text-parsing the error string; stable Ollama
				// error codes would be better but don't exist yet.
				if strings.Contains(strings.ToLower(chunk.Error), "xml syntax error") {
					ch <- StreamEvent{Done: true}
					return
				}
				ch <- StreamEvent{Done: true, Err: fmt.Errorf("ollama: %s", chunk.Error)}
				return
			}
			if len(chunk.Message.ToolCalls) > 0 {
				tc := chunk.Message.ToolCalls[0]
				ch <- StreamEvent{ToolCall: &ToolCall{
					Name:  tc.Function.Name,
					Input: tc.Function.Arguments,
				}}
			}
			if chunk.Message.Content != "" {
				ch <- StreamEvent{Content: chunk.Message.Content}
			}
			if chunk.Done {
				ch <- StreamEvent{Done: true, Usage: &Usage{
					InputTokens:  chunk.PromptEvalCount,
					OutputTokens: chunk.EvalCount,
				}}
				return
			}
		}
		// Stream ended without done=true — treat as done with no usage.
		ch <- StreamEvent{Done: true}
	}()
	return ch
// §foot page/pkg/llm/ollama.go Ollama.ChatStream