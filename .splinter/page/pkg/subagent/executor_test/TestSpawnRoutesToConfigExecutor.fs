// §head page/pkg/subagent/executor_test.go:37-56 TestSpawnRoutesToConfigExecutor
// §sig func TestSpawnRoutesToConfigExecutor(t *testing.T)
	rec := &recorder{}
	res, err := Spawn(context.Background(), Config{
		Prompt:   "do something",
		ParentID: "test-parent",
		Executor: rec,
	})
	if err != nil {
		t.Fatalf("Spawn: %v", err)
	}
	if !rec.called {
		t.Fatal("executor was not called")
	}
	if rec.got.Prompt != "do something" {
		t.Fatalf("prompt: got %q", rec.got.Prompt)
	}
	if res.Summary != "recorded" {
		t.Fatalf("summary: got %q, want %q", res.Summary, "recorded")
	}
// §foot page/pkg/subagent/executor_test.go TestSpawnRoutesToConfigExecutor