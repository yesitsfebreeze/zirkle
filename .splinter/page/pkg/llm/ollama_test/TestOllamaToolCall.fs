// §head page/pkg/llm/ollama_test.go:106-151 TestOllamaToolCall
// §sig func TestOllamaToolCall(t *testing.T)
	var got ollamaReq
	srv := httptest.NewServer(http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		json.NewDecoder(r.Body).Decode(&got)
		json.NewEncoder(w).Encode(ollamaResp{
			Message: ollamaMsg{
				Role:    "assistant",
				Content: "",
				ToolCalls: []ollamaToolCall{{
					Function: ollamaToolCallFn{
						Name:      "spawn",
						Arguments: map[string]any{"prompt": "check the thing"},
					},
				}},
			},
			PromptEvalCount: 10,
			EvalCount:       5,
		})
	}))
	defer srv.Close()

	o := NewOllama(srv.URL, "llama3.2:3b")
	resp, err := o.Chat(context.Background(), ChatRequest{
		Messages: []Message{{Role: "user", Content: "spawn a subagent"}},
		Tools: []Tool{{
			Name:        "spawn",
			Description: "Spawn a subagent",
			InputSchema: map[string]any{"type": "object"},
		}},
	})
	if err != nil {
		t.Fatal(err)
	}
	if resp.Message.ToolUse == nil {
		t.Fatal("want tool call in response")
	}
	if resp.Message.ToolUse.Name != "spawn" {
		t.Errorf("tool name = %q, want spawn", resp.Message.ToolUse.Name)
	}
	if p, _ := resp.Message.ToolUse.Input["prompt"].(string); p != "check the thing" {
		t.Errorf("prompt = %q, want check the thing", p)
	}
	if len(got.Tools) != 1 || got.Tools[0].Function.Name != "spawn" {
		t.Errorf("tools not sent: %+v", got.Tools)
	}
// §foot page/pkg/llm/ollama_test.go TestOllamaToolCall