// §head page/pkg/agent/agent_test.go:235-286 TestPodOverOllamaEndToEnd
// §sig func TestPodOverOllamaEndToEnd(t *testing.T)
	srv := httptest.NewServer(http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		if r.URL.Path != "/api/chat" {
			t.Errorf("path = %q, want /api/chat", r.URL.Path)
		}
		// NDJSON streaming: one content chunk, one done chunk.
		io.WriteString(w, `{"message":{"role":"assistant","content":"disk is fine\nSUMMARY: all clear"},"done":false}`+"\n")
		io.WriteString(w, `{"done":true,"prompt_eval_count":11,"eval_count":4}`+"\n")
	}))
	defer srv.Close()

	s, err := store.Open(filepath.Join(t.TempDir(), "e2e.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer s.Close()
	if err := s.Create("pod-1", "check disk", "smart"); err != nil {
		t.Fatal(err)
	}

	a := &Agent{
		ID: "pod-1", Prompt: "check disk", Mode: "smart",
		LLM: llm.NewOllama(srv.URL, "llama3.2:3b"), Store: s,
	}
	out, err := a.Run(context.Background())
	if err != nil {
		t.Fatalf("pod over ollama: %v", err)
	}

	if out != "disk is fine" {
		t.Errorf("output = %q, want the SUMMARY line stripped", out)
	}
	if a.Recap != "all clear" {
		t.Errorf("recap = %q, want %q", a.Recap, "all clear")
	}
	if a.tokens != 15 {
		t.Errorf("tokens = %d, want 15 (11 prompt + 4 eval)", a.tokens)
	}

	o, err := s.Load("pod-1")
	if err != nil {
		t.Fatal(err)
	}
	o.Recap = a.Recap
	o.State = "done"
	if err := s.Save(o); err != nil {
		t.Fatal(err)
	}
	if reloaded, _ := s.Load("pod-1"); reloaded.Recap != "all clear" {
		t.Errorf("persisted recap = %q, want %q", reloaded.Recap, "all clear")
	}
// §foot page/pkg/agent/agent_test.go TestPodOverOllamaEndToEnd