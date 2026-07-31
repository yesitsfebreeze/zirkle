// §head page/pkg/subagent/executor_test.go:58-86 TestPodRemoteCommandEscapes
// §sig func TestPodRemoteCommandEscapes(t *testing.T)
	o := Pod{
		Host:   "pod-1",
		Binary: "/opt/relay",
		Env:    []string{"ANTHROPIC_API_KEY=sk-it's-secret"},
	}
	cmd := o.remoteCommand(Config{
		ParentID:  "p1",
		Prompt:    "fix it; rm -rf /",
		Model:     "claude-opus-5",
		MaxTokens: 200,
	})

	if !strings.HasPrefix(cmd, "RELAY_RESULT_STDOUT=1 ") {
		t.Fatalf("stdout switch missing: %q", cmd)
	}
	for _, want := range []string{
		`ANTHROPIC_API_KEY='sk-it'\''s-secret'`,
		`'/opt/relay'`,
		`'--subagent'`,
		`'fix it; rm -rf /'`,
		`'--model' 'claude-opus-5'`,
		`'--max-tokens' '200'`,
	} {
		if !strings.Contains(cmd, want) {
			t.Fatalf("remote command missing %q\ngot: %s", want, cmd)
		}
	}
// §foot page/pkg/subagent/executor_test.go TestPodRemoteCommandEscapes