// §head page/pkg/llm/ollama_test.go:156-188 TestOllamaToolResultRoundTrip
// §sig func TestOllamaToolResultRoundTrip(t *testing.T)
	var got ollamaReq
	srv := httptest.NewServer(http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		json.NewDecoder(r.Body).Decode(&got)
		json.NewEncoder(w).Encode(ollamaResp{
			Message: ollamaMsg{Role: "assistant", Content: "done"},
		})
	}))
	defer srv.Close()

	_, err := NewOllama(srv.URL, "llama3.2:3b").Chat(context.Background(), ChatRequest{
		Messages: []Message{
			{Role: "user", Content: "spawn a subagent"},
			{Role: "assistant", ToolUse: &ToolCall{ID: "x", Name: "spawn", Input: map[string]any{"prompt": "check"}}},
			{Role: "user", ToolResult: &ToolResult{ID: "x", Content: "SPAWN RESULT: ok"}},
		},
	})
	if err != nil {
		t.Fatal(err)
	}
	if len(got.Messages) != 3 {
		t.Fatalf("messages = %d, want 3", len(got.Messages))
	}
	if got.Messages[2].Role != "tool" {
		t.Errorf("tool result role = %q, want tool", got.Messages[2].Role)
	}
	if got.Messages[2].Content != "SPAWN RESULT: ok" {
		t.Errorf("tool result content = %q", got.Messages[2].Content)
	}
	if len(got.Messages[1].ToolCalls) != 1 || got.Messages[1].ToolCalls[0].Function.Name != "spawn" {
		t.Errorf("assistant tool_calls not serialized: %+v", got.Messages[1].ToolCalls)
	}
// §foot page/pkg/llm/ollama_test.go TestOllamaToolResultRoundTrip