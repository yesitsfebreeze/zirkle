// §head page/pkg/sandbox/sandbox_test.go:21-32 run
// §sig func run(t *testing.T, s Spec, script string) (string, bool)
	t.Helper()
	ctx, cancel := context.WithTimeout(context.Background(), 30*time.Second)
	defer cancel()

	cmd, err := s.Command(ctx, "/bin/sh", "-c", script)
	if err != nil {
		t.Fatalf("Command: %v", err)
	}
	out, err := cmd.CombinedOutput()
	return string(out), err == nil
// §foot page/pkg/sandbox/sandbox_test.go run