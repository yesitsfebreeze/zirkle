// §head page/pkg/subagent/sandboxed_test.go:14-19 requireSandbox
// §sig func requireSandbox(t *testing.T)
	t.Helper()
	if err := sandbox.Probe(); err != nil {
		t.Skipf("no sandbox on this host: %v", err)
	}
// §foot page/pkg/subagent/sandboxed_test.go requireSandbox