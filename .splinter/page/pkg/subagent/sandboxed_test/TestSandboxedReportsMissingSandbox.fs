// §head page/pkg/subagent/sandboxed_test.go:99-110 TestSandboxedReportsMissingSandbox
// §sig func TestSandboxedReportsMissingSandbox(t *testing.T)
	// Probe must be the gate: an unsandboxable host fails loudly rather
	// than silently running the agent unconfined.
	t.Setenv("PATH", t.TempDir())
	_, err := Sandboxed{}.Run(context.Background(), Config{Prompt: "x", Timeout: time.Second})
	if err == nil {
		t.Fatal("expected failure when bwrap is unavailable")
	}
	if !strings.Contains(err.Error(), "sandbox unavailable") {
		t.Fatalf("unexpected error: %v", err)
	}
// §foot page/pkg/subagent/sandboxed_test.go TestSandboxedReportsMissingSandbox