// §head page/pkg/subagent/policy_test.go:12-29 TestDefaultIsConfined
// §sig func TestDefaultIsConfined(t *testing.T)
	if err := sandbox.Probe(); err != nil {
		t.Skipf("no sandbox on this host: %v", err)
	}
	t.Setenv(EnvSandbox, "")

	exec, err := DefaultExecutor()
	if err != nil {
		t.Fatalf("DefaultExecutor: %v", err)
	}
	s, ok := exec.(Sandboxed)
	if !ok {
		t.Fatalf("default executor is %T, want Sandboxed", exec)
	}
	if !s.Spec.Ephemeral {
		t.Fatal("default sandbox should be ephemeral")
	}
// §foot page/pkg/subagent/policy_test.go TestDefaultIsConfined