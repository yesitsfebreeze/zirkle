// §head page/pkg/llm/ollama_test.go:13-58 TestOllamaChat
// §sig func TestOllamaChat(t *testing.T)
	var got ollamaReq
	srv := httptest.NewServer(http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		if r.URL.Path != "/api/chat" {
			t.Errorf("path = %q, want /api/chat", r.URL.Path)
		}
		if err := json.NewDecoder(r.Body).Decode(&got); err != nil {
			t.Fatal(err)
		}
		json.NewEncoder(w).Encode(ollamaResp{
			Message:         ollamaMsg{Role: "assistant", Content: "pong"},
			PromptEvalCount: 7,
			EvalCount:       3,
		})
	}))
	defer srv.Close()

	o := NewOllama(srv.URL, "llama3.2:3b")
	resp, err := o.Chat(context.Background(), ChatRequest{
		Messages:  []Message{{Role: "user", Content: "ping"}},
		MaxTokens: 64,
	})
	if err != nil {
		t.Fatalf("Chat: %v", err)
	}

	// Streaming must be off — the agent loop reads one whole response.
	if got.Stream {
		t.Error("stream = true, want false")
	}
	if got.Model != "llama3.2:3b" {
		t.Errorf("model = %q, want llama3.2:3b", got.Model)
	}
	if got.Options.NumPredict != 64 {
		t.Errorf("num_predict = %d, want 64", got.Options.NumPredict)
	}
	if len(got.Messages) != 1 || got.Messages[0].Content != "ping" {
		t.Errorf("messages = %+v, want one user ping", got.Messages)
	}
	if resp.Message.Content != "pong" {
		t.Errorf("content = %q, want pong", resp.Message.Content)
	}
	if resp.Usage.InputTokens != 7 || resp.Usage.OutputTokens != 3 {
		t.Errorf("usage = %+v, want 7 in / 3 out", resp.Usage)
	}
// §foot page/pkg/llm/ollama_test.go TestOllamaChat