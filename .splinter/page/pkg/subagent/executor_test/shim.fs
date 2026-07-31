// §head page/pkg/subagent/executor_test.go:15-23 shim
// §sig func shim(t *testing.T, body string) string
	t.Helper()
	path := filepath.Join(t.TempDir(), "shim")
	script := "#!/bin/sh\nshift\n" + body + "\n"
	if err := os.WriteFile(path, []byte(script), 0o755); err != nil {
		t.Fatalf("write shim: %v", err)
	}
	return path
// §foot page/pkg/subagent/executor_test.go shim