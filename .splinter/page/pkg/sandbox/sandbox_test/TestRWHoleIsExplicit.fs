// §head page/pkg/sandbox/sandbox_test.go:164-175 TestRWHoleIsExplicit
// §sig func TestRWHoleIsExplicit(t *testing.T)
	requireSandbox(t)
	shared := t.TempDir()

	out, ok := run(t, Spec{Dir: t.TempDir(), RW: []string{shared}}, "echo shared > "+shared+"/cache.txt")
	if !ok {
		t.Fatalf("declared RW path was not writable: %s", out)
	}
	if _, err := os.Stat(filepath.Join(shared, "cache.txt")); err != nil {
		t.Fatalf("RW path did not reach the host: %v", err)
	}
// §foot page/pkg/sandbox/sandbox_test.go TestRWHoleIsExplicit