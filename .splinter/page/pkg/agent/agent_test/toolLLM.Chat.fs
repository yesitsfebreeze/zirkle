// §head page/pkg/agent/agent_test.go:41-57 toolLLM.Chat
// §sig func (f *toolLLM) Chat(ctx context.Context, req llm.ChatRequest) (*llm.ChatResponse, error)
	f.turn++
	if f.turn == 1 {
		return &llm.ChatResponse{
			Message: llm.Message{
				Role:    "assistant",
				Content: "",
				ToolUse: &llm.ToolCall{ID: "call-1", Name: "spawn", Input: map[string]any{"prompt": "check the thing"}},
			},
			Usage: llm.Usage{InputTokens: 10, OutputTokens: 5},
		}, nil
	}
	return &llm.ChatResponse{
		Message: llm.Message{Role: "assistant", Content: "done\nSUMMARY: all clear"},
		Usage:   llm.Usage{InputTokens: 8, OutputTokens: 3},
	}, nil
// §foot page/pkg/agent/agent_test.go toolLLM.Chat