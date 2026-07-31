// §head page/pkg/llm/anthropic.go:152-272 Anthropic.ChatStream
// §sig func (a *Anthropic) ChatStream(ctx context.Context, req ChatRequest) <-chan StreamEvent
	ch := make(chan StreamEvent, 64)
	go func() {
		defer close(ch)
		model := req.Model
		if model == "" {
			model = a.Model
		}
		if len(req.Messages) == 0 {
			ch <- StreamEvent{Done: true, Err: fmt.Errorf("anthropic: chat request has no messages")}
			return
		}
		if model == "" {
			ch <- StreamEvent{Done: true, Err: fmt.Errorf("anthropic: chat request model is empty")}
			return
		}
		maxTokens := req.MaxTokens
		if maxTokens == 0 {
			maxTokens = 4096
		}
		msgs := make([]anthropicMsg, len(req.Messages))
		for i, m := range req.Messages {
			msgs[i] = toAnthropicMsg(m)
		}
		body, err := json.Marshal(anthropicReq{
			Model:     model,
			MaxTokens: maxTokens,
			Messages:  msgs,
			Tools:     toAnthropicTools(req.Tools),
			Stream:    true,
		})
		if err != nil {
			ch <- StreamEvent{Done: true, Err: err}
			return
		}
		hr, err := http.NewRequestWithContext(ctx, http.MethodPost, a.BaseURL+"/v1/messages", bytes.NewReader(body))
		if err != nil {
			ch <- StreamEvent{Done: true, Err: err}
			return
		}
		hr.Header.Set("x-api-key", a.APIKey)
		hr.Header.Set("anthropic-version", "2023-06-01")
		hr.Header.Set("content-type", "application/json")
		hr.Header.Set("accept", "text/event-stream")

		resp, err := a.HTTP.Do(hr)
		if err != nil {
			ch <- StreamEvent{Done: true, Err: fmt.Errorf("anthropic: %w", err)}
			return
		}
		defer resp.Body.Close()
		if resp.StatusCode != http.StatusOK {
			ch <- StreamEvent{Done: true, Err: fmt.Errorf("anthropic: HTTP %d", resp.StatusCode)}
			return
		}

		var usage Usage
		scanner := bufio.NewScanner(resp.Body)
		scanner.Buffer(make([]byte, 0, 1024*1024), 1024*1024)
		for scanner.Scan() {
			line := scanner.Text()
			if !strings.HasPrefix(line, "data: ") {
				continue
			}
			payload := line[6:]
			var ev struct {
				Type  string          `json:"type"`
				Delta json.RawMessage `json:"delta"`
				Usage *struct {
					InputTokens  int `json:"input_tokens"`
					OutputTokens int `json:"output_tokens"`
				} `json:"usage"`
				ContentBlock *anthropicBlock `json:"content_block"`
			}
			if err := json.Unmarshal([]byte(payload), &ev); err != nil {
				continue
			}
			switch ev.Type {
			case "content_block_start":
				if ev.ContentBlock != nil && ev.ContentBlock.Type == "tool_use" {
					ch <- StreamEvent{ToolCall: &ToolCall{
						ID:    ev.ContentBlock.ID,
						Name:  ev.ContentBlock.Name,
						Input: ev.ContentBlock.Input,
					}}
				}
			case "content_block_delta":
				var d struct {
					Type string `json:"type"`
					Text string `json:"text"`
				}
				if err := json.Unmarshal(ev.Delta, &d); err == nil && d.Text != "" {
					ch <- StreamEvent{Content: d.Text}
				}
			case "message_delta":
				if ev.Usage != nil {
					usage.OutputTokens = ev.Usage.OutputTokens
					if ev.Usage.InputTokens > 0 {
						usage.InputTokens = ev.Usage.InputTokens
					}
				}
			case "message_start":
				// message_start carries initial usage with input tokens.
				if ev.Usage != nil {
					usage.InputTokens = ev.Usage.InputTokens
					usage.OutputTokens = ev.Usage.OutputTokens
				}
			case "message_stop":
				ch <- StreamEvent{Done: true, Usage: &usage}
				return
			}
		}
		if err := scanner.Err(); err != nil {
			ch <- StreamEvent{Done: true, Err: fmt.Errorf("anthropic: stream: %w", err)}
			return
		}
		// Stream ended without message_stop.
		ch <- StreamEvent{Done: true, Usage: &usage}
	}()
	return ch
// §foot page/pkg/llm/anthropic.go Anthropic.ChatStream