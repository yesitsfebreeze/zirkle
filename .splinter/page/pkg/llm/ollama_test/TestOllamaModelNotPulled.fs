// §head page/pkg/llm/ollama_test.go:60-76 TestOllamaModelNotPulled
// §sig func TestOllamaModelNotPulled(t *testing.T)
	srv := httptest.NewServer(http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		w.WriteHeader(http.StatusNotFound)
		json.NewEncoder(w).Encode(ollamaResp{Error: `model "nope" not found, try pulling it first`})
	}))
	defer srv.Close()

	_, err := NewOllama(srv.URL, "nope").Chat(context.Background(), ChatRequest{
		Messages: []Message{{Role: "user", Content: "hi"}},
	})
	if err == nil {
		t.Fatal("want error for unpulled model")
	}
	if !strings.Contains(err.Error(), "ollama pull nope") {
		t.Errorf("error = %q, want an actionable `ollama pull` hint", err)
	}
// §foot page/pkg/llm/ollama_test.go TestOllamaModelNotPulled