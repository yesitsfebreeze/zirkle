// §head page/pkg/agent/agent.go:181-290 Agent.RunStream
// §sig func (a *Agent) RunStream(ctx context.Context, events chan<- llm.StreamEvent) (string, error)
	if err := a.Provision(); err != nil {
		return "", err
	}

	// Warm dispatch: if a composition is attached, search its shards for a
	// match before calling the LLM. Exit 0 = done, no LLM call needed.
	if a.Comp != nil {
		shard, output, exitCode, derr := comp.WarmDispatch(a.Comp.Store.DB(), a.Comp.Store, a.Prompt)
		if derr == nil && shard != nil && exitCode == 0 {
			a.Recap = "warm dispatch: " + shard.Name
			if events != nil {
				events <- llm.StreamEvent{Content: output}
				events <- llm.StreamEvent{Done: true}
			}
			return output, nil
		}
	}

	budget := a.Budget
	// Inventory: build a compact listing of indexed pods for the system
	// prompt. The model sees names + kinds + descriptions so it can reason
	// about which tool fits without reading every pod file first.
	if a.Comp != nil && a.Comp.Store != nil && a.Inventory == "" {
		a.Inventory = Inventory(a.Comp.Store)
	}

	a.msgs = []llm.Message{
		{Role: "system", Content: SystemPrompt(a.Inventory)},
		{Role: "user", Content: a.Prompt},
	}
	for {
		if a.tokens >= budget {
			return "", fmt.Errorf("agent %s: token budget exhausted (%d)", a.ID, a.tokens)
		}

		var contentBuf strings.Builder
		var toolCall *llm.ToolCall
		var usage llm.Usage

		stream := a.LLM.ChatStream(ctx, llm.ChatRequest{
			Model:    a.Model,
			Messages: a.msgs,
			Tools:    []llm.Tool{spawnTool},
		})
		var streamErr error
		for ev := range stream {
			if ev.Err != nil {
				streamErr = ev.Err
				break
			}
			if ev.ToolCall != nil {
				toolCall = ev.ToolCall
				if events != nil {
					events <- ev
				}
			}
			if ev.Content != "" {
				contentBuf.WriteString(ev.Content)
				if events != nil {
					events <- ev
				}
			}
			if ev.Done && ev.Usage != nil {
				usage = *ev.Usage
			}
		}
		if streamErr != nil {
			return "", fmt.Errorf("agent %s: llm: %w", a.ID, streamErr)
		}

		content := contentBuf.String()
		msg := llm.Message{Role: "assistant", Content: content}
		if toolCall != nil {
			msg.ToolUse = toolCall
		}
		a.msgs = append(a.msgs, msg)
		a.tokens += usage.InputTokens + usage.OutputTokens
		a.turn++
		if a.Store != nil {
			state, err := json.Marshal(a.msgs)
			if err != nil {
				return "", err
			}
			if err := a.Store.Checkpoint(a.ID, a.turn, state); err != nil {
				return "", err
			}
		}
		a.Recap, content = extractRecap(content)

		// Handle tool calls from the LLM (e.g. spawn). Inject the result as
		// a tool-result message and continue the loop so the model can use it.
		if toolCall != nil {
			result := a.handleToolCall(ctx, toolCall)
			if events != nil {
				events <- llm.StreamEvent{ToolOutput: result}
			}
			a.msgs = append(a.msgs, llm.Message{
				Role:       "user",
				ToolResult: &llm.ToolResult{ID: toolCall.ID, Content: result},
			})
			continue
		}

		if events != nil {
			events <- llm.StreamEvent{Done: true}
		}
		return content, nil
	}
// §foot page/pkg/agent/agent.go Agent.RunStream