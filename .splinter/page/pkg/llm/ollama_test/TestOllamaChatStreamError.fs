// §head page/pkg/llm/ollama_test.go:287-306 TestOllamaChatStreamError
// §sig func TestOllamaChatStreamError(t *testing.T)
	srv := httptest.NewServer(http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		w.WriteHeader(http.StatusInternalServerError)
		io.WriteString(w, `{"error":"internal boom"}`)
	}))
	defer srv.Close()

	o := NewOllama(srv.URL, "m")
	var gotErr error
	for ev := range o.ChatStream(context.Background(), ChatRequest{
		Messages: []Message{{Role: "user", Content: "hi"}},
	}) {
		if ev.Err != nil {
			gotErr = ev.Err
		}
	}
	if gotErr == nil || !strings.Contains(gotErr.Error(), "internal boom") {
		t.Fatalf("err = %v, want internal boom", gotErr)
	}
// §foot page/pkg/llm/ollama_test.go TestOllamaChatStreamError