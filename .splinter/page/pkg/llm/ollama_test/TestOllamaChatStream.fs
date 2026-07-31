// §head page/pkg/llm/ollama_test.go:251-285 TestOllamaChatStream
// §sig func TestOllamaChatStream(t *testing.T)
	srv := httptest.NewServer(http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		// NDJSON: two content chunks then a done chunk with usage.
		io.WriteString(w, `{"message":{"role":"assistant","content":"Hello"},"done":false}`+"\n")
		io.WriteString(w, `{"message":{"role":"assistant","content":" world"},"done":false}`+"\n")
		io.WriteString(w, `{"done":true,"prompt_eval_count":5,"eval_count":2}`+"\n")
	}))
	defer srv.Close()

	o := NewOllama(srv.URL, "llama3.2:3b")
	var content strings.Builder
	var usage *Usage
	var doneCount int
	for ev := range o.ChatStream(context.Background(), ChatRequest{
		Messages: []Message{{Role: "user", Content: "hi"}},
	}) {
		if ev.Err != nil {
			t.Fatalf("stream error: %v", ev.Err)
		}
		content.WriteString(ev.Content)
		if ev.Done {
			doneCount++
			usage = ev.Usage
		}
	}
	if content.String() != "Hello world" {
		t.Errorf("content = %q, want %q", content.String(), "Hello world")
	}
	if doneCount != 1 {
		t.Errorf("done events = %d, want 1", doneCount)
	}
	if usage == nil || usage.InputTokens != 5 || usage.OutputTokens != 2 {
		t.Errorf("usage = %+v, want 5 in / 2 out", usage)
	}
// §foot page/pkg/llm/ollama_test.go TestOllamaChatStream