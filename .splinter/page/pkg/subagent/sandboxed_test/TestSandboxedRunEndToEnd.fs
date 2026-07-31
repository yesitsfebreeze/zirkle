// §head page/pkg/subagent/sandboxed_test.go:25-44 TestSandboxedRunEndToEnd
// §sig func TestSandboxedRunEndToEnd(t *testing.T)
	requireSandbox(t)

	res, err := Sandboxed{
		Env: []string{"RELAY_SUBAGENT_RUN=1"},
	}.Run(context.Background(), Config{
		Prompt:   "do something",
		ParentID: "test-parent",
		Timeout:  30 * time.Second,
	})
	if err != nil {
		t.Fatalf("Sandboxed.Run: %v", err)
	}
	if !res.Success {
		t.Fatalf("expected success, got %+v", res)
	}
	if res.Summary != "test summary" {
		t.Fatalf("summary: got %q, want %q", res.Summary, "test summary")
	}
// §foot page/pkg/subagent/sandboxed_test.go TestSandboxedRunEndToEnd