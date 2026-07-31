// §head page/pkg/sandbox/sandbox_test.go:177-188 TestEnvIsNotInherited
// §sig func TestEnvIsNotInherited(t *testing.T)
	requireSandbox(t)
	t.Setenv("RELAY_SECRET_TOKEN", "leaked-value")

	out, _ := run(t, Spec{Dir: t.TempDir()}, "env")
	if strings.Contains(out, "leaked-value") {
		t.Fatalf("host environment leaked into sandbox: %s", out)
	}
	if !strings.Contains(out, "HOME="+Root) {
		t.Fatalf("default env missing: %s", out)
	}
// §foot page/pkg/sandbox/sandbox_test.go TestEnvIsNotInherited