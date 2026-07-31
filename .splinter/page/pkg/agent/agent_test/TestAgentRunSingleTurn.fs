// §head page/pkg/agent/agent_test.go:75-108 TestAgentRunSingleTurn
// §sig func TestAgentRunSingleTurn(t *testing.T)
	s, err := store.Open(filepath.Join(t.TempDir(), "test.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer s.Close()
	if err := s.Create("a1", "do thing", "smart"); err != nil {
		t.Fatal(err)
	}
	a := &Agent{
		ID: "a1", Prompt: "do thing", Mode: "smart",
		LLM: &fakeLLM{reply: "done"}, Store: s,
	}
	out, err := a.Run(context.Background())
	if err != nil {
		t.Fatal(err)
	}
	if out != "done" {
		t.Fatalf("output: %q", out)
	}
	if a.turn != 1 {
		t.Fatalf("turns: %d", a.turn)
	}
	if a.tokens != 15 {
		t.Fatalf("tokens: %d", a.tokens)
	}
	got, err := s.LoadCheckpoint("a1", 1)
	if err != nil {
		t.Fatalf("checkpoint missing: %v", err)
	}
	if len(got) == 0 {
		t.Fatal("empty checkpoint")
	}
// §foot page/pkg/agent/agent_test.go TestAgentRunSingleTurn