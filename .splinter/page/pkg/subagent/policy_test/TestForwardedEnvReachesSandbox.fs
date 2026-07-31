// §head page/pkg/subagent/policy_test.go:94-113 TestForwardedEnvReachesSandbox
// §sig func TestForwardedEnvReachesSandbox(t *testing.T)
	if err := sandbox.Probe(); err != nil {
		t.Skipf("no sandbox on this host: %v", err)
	}
	t.Setenv(EnvSandbox, "")
	t.Setenv("RELAY_MODEL", "llama3.2:3b")
	t.Setenv("RELAY_SECRET_TOKEN", "leaked-value")

	exec, err := DefaultExecutor()
	if err != nil {
		t.Fatalf("DefaultExecutor: %v", err)
	}
	env := strings.Join(exec.(Sandboxed).Env, " ")
	if !strings.Contains(env, "RELAY_MODEL=llama3.2:3b") {
		t.Fatalf("model not forwarded: %s", env)
	}
	if strings.Contains(env, "leaked-value") {
		t.Fatalf("unlisted host variable forwarded: %s", env)
	}
// §foot page/pkg/subagent/policy_test.go TestForwardedEnvReachesSandbox