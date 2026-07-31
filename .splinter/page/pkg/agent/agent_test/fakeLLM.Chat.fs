// §head page/pkg/agent/agent_test.go:20-25 fakeLLM.Chat
// §sig func (f *fakeLLM) Chat(ctx context.Context, req llm.ChatRequest) (*llm.ChatResponse, error)
	return &llm.ChatResponse{
		Message: llm.Message{Role: "assistant", Content: f.reply},
		Usage:   llm.Usage{InputTokens: 10, OutputTokens: 5},
	}, nil
// §foot page/pkg/agent/agent_test.go fakeLLM.Chat