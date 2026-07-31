// §head page/pkg/subagent/executor_test.go:270-288 TestResolveBinaryCopiesInDevMode
// §sig func TestResolveBinaryCopiesInDevMode(t *testing.T)
	t.Setenv("RELAY_DEV_CHILD", "1")
	exe, err := os.Executable()
	if err != nil {
		t.Skip("no os.Executable")
	}

	path, cleanup, err := resolveBinary(exe)
	if err != nil {
		t.Fatalf("resolveBinary: %v", err)
	}
	defer cleanup()
	if path == exe {
		t.Fatal("dev mode returned original path — race with hotreload rebuild")
	}
	if _, err := os.Stat(path); err != nil {
		t.Fatalf("materialized binary missing: %v", err)
	}
// §foot page/pkg/subagent/executor_test.go TestResolveBinaryCopiesInDevMode