// §head page/pkg/subagent/policy_test.go:77-92 TestUnsandboxableHostRefusesToDowngrade
// §sig func TestUnsandboxableHostRefusesToDowngrade(t *testing.T)
	t.Setenv(EnvSandbox, "")
	t.Setenv("PATH", t.TempDir())

	_, err := DefaultExecutor()
	if err == nil {
		t.Fatal("expected an error when the host cannot sandbox")
	}
	if !strings.Contains(err.Error(), "sandbox unavailable") {
		t.Fatalf("unexpected error: %v", err)
	}
	// The error has to carry the way out, or the operator is stuck.
	if !strings.Contains(err.Error(), EnvSandbox+"=off") {
		t.Fatalf("error does not name the escape hatch: %v", err)
	}
// §foot page/pkg/subagent/policy_test.go TestUnsandboxableHostRefusesToDowngrade