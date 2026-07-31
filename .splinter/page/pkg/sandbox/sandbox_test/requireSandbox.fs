// §head page/pkg/sandbox/sandbox_test.go:12-17 requireSandbox
// §sig func requireSandbox(t *testing.T)
	t.Helper()
	if err := Probe(); err != nil {
		t.Skipf("no sandbox on this host: %v", err)
	}
// §foot page/pkg/sandbox/sandbox_test.go requireSandbox