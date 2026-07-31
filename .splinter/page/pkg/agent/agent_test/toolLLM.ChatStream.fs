// §head page/pkg/agent/agent_test.go:59-73 toolLLM.ChatStream
// §sig func (f *toolLLM) ChatStream(ctx context.Context, req llm.ChatRequest) <-chan llm.StreamEvent
	ch := make(chan llm.StreamEvent, 4)
	go func() {
		defer close(ch)
		f.turn++
		if f.turn == 1 {
			ch <- llm.StreamEvent{ToolCall: &llm.ToolCall{ID: "call-1", Name: "spawn", Input: map[string]any{"prompt": "check the thing"}}}
			ch <- llm.StreamEvent{Done: true, Usage: &llm.Usage{InputTokens: 10, OutputTokens: 5}}
			return
		}
		ch <- llm.StreamEvent{Content: "done\nSUMMARY: all clear"}
		ch <- llm.StreamEvent{Done: true, Usage: &llm.Usage{InputTokens: 8, OutputTokens: 3}}
	}()
	return ch
// §foot page/pkg/agent/agent_test.go toolLLM.ChatStream