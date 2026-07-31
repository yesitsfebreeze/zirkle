// §head page/pkg/llm/ollama_test.go:78-89 TestOllamaDaemonDown
// §sig func TestOllamaDaemonDown(t *testing.T)
	// Port 1 is reserved and never listening.
	_, err := NewOllama("http://127.0.0.1:1", "llama3.2:3b").Chat(context.Background(), ChatRequest{
		Messages: []Message{{Role: "user", Content: "hi"}},
	})
	if err == nil {
		t.Fatal("want error when the daemon is down")
	}
	if !strings.Contains(err.Error(), "ollama serve") {
		t.Errorf("error = %q, want it to name `ollama serve`", err)
	}
// §foot page/pkg/llm/ollama_test.go TestOllamaDaemonDown