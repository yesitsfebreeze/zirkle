// §head page/pkg/sandbox/sandbox_test.go:90-115 TestEscapeAttemptsAreDenied
// §sig func TestEscapeAttemptsAreDenied(t *testing.T)
	requireSandbox(t)
	home, err := os.UserHomeDir()
	if err != nil {
		t.Skip("no home dir")
	}

	for _, script := range []string{
		"echo pwned > /etc/pwned",
		"echo pwned > /usr/pwned",
		"echo pwned > " + filepath.Join(home, "pwned"),
		"echo pwned > " + Root + "/../pwned",
		"mkdir -p /host && mount --bind / /host",
	} {
		out, ok := run(t, Spec{Dir: t.TempDir()}, script)
		if ok {
			t.Fatalf("escape succeeded: %s\noutput: %s", script, out)
		}
	}

	// Nothing leaked onto the host either.
	if _, err := os.Stat(filepath.Join(home, "pwned")); err == nil {
		os.Remove(filepath.Join(home, "pwned"))
		t.Fatal("sandbox wrote into the host home directory")
	}
// §foot page/pkg/sandbox/sandbox_test.go TestEscapeAttemptsAreDenied