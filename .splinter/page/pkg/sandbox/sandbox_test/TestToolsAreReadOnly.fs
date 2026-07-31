// §head page/pkg/sandbox/sandbox_test.go:117-127 TestToolsAreReadOnly
// §sig func TestToolsAreReadOnly(t *testing.T)
	requireSandbox(t)

	out, ok := run(t, Spec{Dir: t.TempDir()}, "/bin/echo tools-work && touch /usr/bin/oops")
	if ok {
		t.Fatalf("wrote into the read-only tool tree: %s", out)
	}
	if !strings.Contains(out, "tools-work") {
		t.Fatalf("host tools not usable inside sandbox: %s", out)
	}
// §foot page/pkg/sandbox/sandbox_test.go TestToolsAreReadOnly