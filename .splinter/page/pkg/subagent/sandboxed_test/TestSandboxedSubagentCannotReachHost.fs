// §head page/pkg/subagent/sandboxed_test.go:48-77 TestSandboxedSubagentCannotReachHost
// §sig func TestSandboxedSubagentCannotReachHost(t *testing.T)
	requireSandbox(t)

	work := t.TempDir()
	secret := filepath.Join(t.TempDir(), "secret.txt")
	if err := os.WriteFile(secret, []byte("do not leak"), 0o600); err != nil {
		t.Fatalf("seed: %v", err)
	}

	res, err := Sandboxed{
		Spec: sandbox.Spec{Dir: work},
		Env:  []string{"RELAY_SUBAGENT_RUN=1"},
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

	// The secret file is outside every mount the spec declared, so nothing
	// inside could have opened it.
	if _, err := os.Stat(secret); err != nil {
		t.Fatalf("host file disturbed: %v", err)
	}
// §foot page/pkg/subagent/sandboxed_test.go TestSandboxedSubagentCannotReachHost