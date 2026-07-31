// §head page/pkg/subagent/subagent_test.go:33-62 TestSpawnAndCollect
// §sig func TestSpawnAndCollect(t *testing.T)
	// Tell child processes that they are in test mode.  Because the child
	// inherits the environment, RunSubagent will check for this variable
	// and write a canned result immediately instead of calling the LLM.
	t.Setenv("RELAY_SUBAGENT_RUN", "1")

	// Local is named on purpose: the default is confined, and a test of the
	// fd 3 child-process path has to ask for that path.
	result, err := Spawn(context.Background(), Config{
		Prompt:   "do something",
		ParentID: "test-parent",
		Timeout:  5 * time.Second,
		Executor: Local{},
	})
	if err != nil {
		t.Fatalf("Spawn: %v", err)
	}
	if !result.Success {
		t.Fatal("expected success")
	}
	if result.Summary != "test summary" {
		t.Fatalf("summary: got %q, want %q", result.Summary, "test summary")
	}
	if result.Output != "test output" {
		t.Fatalf("output: got %q, want %q", result.Output, "test output")
	}
	if result.Tokens != 50 {
		t.Fatalf("tokens: got %d, want 50", result.Tokens)
	}
// §foot page/pkg/subagent/subagent_test.go TestSpawnAndCollect