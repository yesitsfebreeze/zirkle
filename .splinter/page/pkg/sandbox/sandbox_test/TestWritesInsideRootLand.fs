// §head page/pkg/sandbox/sandbox_test.go:49-69 TestWritesInsideRootLand
// §sig func TestWritesInsideRootLand(t *testing.T)
	requireSandbox(t)
	dir := t.TempDir()

	out, ok := run(t, Spec{Dir: dir}, "echo hello > "+Root+"/note.txt && cat "+Root+"/note.txt")
	if !ok {
		t.Fatalf("write inside root failed: %s", out)
	}
	if !strings.Contains(out, "hello") {
		t.Fatalf("got %q", out)
	}

	// A bind-mounted root is durable: the host sees the pod's work.
	body, err := os.ReadFile(filepath.Join(dir, "note.txt"))
	if err != nil {
		t.Fatalf("host read: %v", err)
	}
	if strings.TrimSpace(string(body)) != "hello" {
		t.Fatalf("host file: got %q", body)
	}
// §foot page/pkg/sandbox/sandbox_test.go TestWritesInsideRootLand