// §head page/pkg/subagent/executor_test.go:151-167 TestPodRunKeepsResultOnNonZeroExit
// §sig func TestPodRunKeepsResultOnNonZeroExit(t *testing.T)
	o := Pod{
		Host:    "pod-1",
		Command: shim(t, `echo '{"success":false,"summary":"llm refused","output":"o","tokens":0}'; exit 1`),
	}

	res, err := o.Run(context.Background(), Config{Prompt: "x", Timeout: 5 * time.Second})
	if err != nil {
		t.Fatalf("Pod.Run: %v", err)
	}
	if res.Success {
		t.Fatal("expected failure result")
	}
	if res.Summary != "llm refused" {
		t.Fatalf("summary: got %q", res.Summary)
	}
// §foot page/pkg/subagent/executor_test.go TestPodRunKeepsResultOnNonZeroExit