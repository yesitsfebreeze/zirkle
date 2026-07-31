// §head page/pkg/sandbox/sandbox_test.go:73-88 TestHostPathsAreInvisible
// §sig func TestHostPathsAreInvisible(t *testing.T)
	requireSandbox(t)

	secret := filepath.Join(t.TempDir(), "secret.txt")
	if err := os.WriteFile(secret, []byte("do not leak"), 0o600); err != nil {
		t.Fatalf("seed: %v", err)
	}

	out, ok := run(t, Spec{Dir: t.TempDir()}, "cat "+secret)
	if ok {
		t.Fatalf("sandbox read a host file it should not see: %s", out)
	}
	if strings.Contains(out, "do not leak") {
		t.Fatalf("host secret leaked into sandbox: %s", out)
	}
// §foot page/pkg/sandbox/sandbox_test.go TestHostPathsAreInvisible