// §head page/pkg/agent/agent_test.go:27-35 fakeLLM.ChatStream
// §sig func (f *fakeLLM) ChatStream(ctx context.Context, req llm.ChatRequest) <-chan llm.StreamEvent
	ch := make(chan llm.StreamEvent, 4)
	go func() {
		defer close(ch)
		ch <- llm.StreamEvent{Content: f.reply}
		ch <- llm.StreamEvent{Done: true, Usage: &llm.Usage{InputTokens: 10, OutputTokens: 5}}
	}()
	return ch
// §foot page/pkg/agent/agent_test.go fakeLLM.ChatStream