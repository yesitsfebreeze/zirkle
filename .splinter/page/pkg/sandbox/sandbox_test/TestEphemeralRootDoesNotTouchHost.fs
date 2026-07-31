// §head page/pkg/sandbox/sandbox_test.go:129-140 TestEphemeralRootDoesNotTouchHost
// §sig func TestEphemeralRootDoesNotTouchHost(t *testing.T)
	requireSandbox(t)
	dir := t.TempDir()

	out, ok := run(t, Spec{Dir: dir, Ephemeral: true, SizeMB: 16}, "echo gone > "+Root+"/scratch.txt")
	if !ok {
		t.Fatalf("ephemeral write failed: %s", out)
	}
	if _, err := os.Stat(filepath.Join(dir, "scratch.txt")); err == nil {
		t.Fatal("ephemeral root leaked to the host directory")
	}
// §foot page/pkg/sandbox/sandbox_test.go TestEphemeralRootDoesNotTouchHost