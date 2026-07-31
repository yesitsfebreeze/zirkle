// §head page/pkg/subagent/executor_test.go:107-132 TestPodRunEndToEnd
// §sig func TestPodRunEndToEnd(t *testing.T)
	o := Pod{
		Host:    "pod-1",
		Binary:  os.Args[0],
		Command: shim(t, `eval "$1"`),
		Env:     []string{"RELAY_SUBAGENT_RUN=1"},
	}

	res, err := o.Run(context.Background(), Config{
		Prompt:   "do something",
		ParentID: "test-parent",
		Timeout:  10 * time.Second,
	})
	if err != nil {
		t.Fatalf("Pod.Run: %v", err)
	}
	if !res.Success {
		t.Fatalf("expected success, got %+v", res)
	}
	if res.Summary != "test summary" {
		t.Fatalf("summary: got %q, want %q", res.Summary, "test summary")
	}
	if res.Tokens != 50 {
		t.Fatalf("tokens: got %d, want 50", res.Tokens)
	}
// §foot page/pkg/subagent/executor_test.go TestPodRunEndToEnd