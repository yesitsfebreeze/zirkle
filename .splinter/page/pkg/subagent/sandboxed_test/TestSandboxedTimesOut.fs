// §head page/pkg/subagent/sandboxed_test.go:79-97 TestSandboxedTimesOut
// §sig func TestSandboxedTimesOut(t *testing.T)
	requireSandbox(t)

	res, err := Sandboxed{
		Env: []string{"RELAY_SUBAGENT_SLEEP=30s"},
	}.Run(context.Background(), Config{
		Prompt:  "slow one",
		Timeout: 300 * time.Millisecond,
	})
	if err != nil {
		t.Fatalf("Sandboxed.Run: %v", err)
	}
	if res.Success {
		t.Fatal("expected timeout failure")
	}
	if res.Summary != "subagent timed out" {
		t.Fatalf("summary: got %q", res.Summary)
	}
// §foot page/pkg/subagent/sandboxed_test.go TestSandboxedTimesOut