// §head page/pkg/subagent/executor_test.go:250-266 TestResolveBinaryUsesPathWhenPresent
// §sig func TestResolveBinaryUsesPathWhenPresent(t *testing.T)
	exe, err := os.Executable()
	if err != nil {
		t.Skip("no os.Executable")
	}
	path, cleanup, err := resolveBinary(exe)
	if err != nil {
		t.Fatalf("resolveBinary: %v", err)
	}
	defer cleanup()
	if path == "" {
		t.Fatal("empty path")
	}
	if _, err := os.Stat(path); err != nil {
		t.Fatalf("resolved path missing: %v", err)
	}
// §foot page/pkg/subagent/executor_test.go TestResolveBinaryUsesPathWhenPresent