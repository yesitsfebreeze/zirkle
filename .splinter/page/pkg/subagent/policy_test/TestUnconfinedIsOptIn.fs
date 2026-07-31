// §head page/pkg/subagent/policy_test.go:31-54 TestUnconfinedIsOptIn
// §sig func TestUnconfinedIsOptIn(t *testing.T)
	for _, value := range []string{"off", "0", "false", "no"} {
		t.Setenv(EnvSandbox, value)
		if !Unconfined() {
			t.Fatalf("%s=%q should select the local escape hatch", EnvSandbox, value)
		}
		exec, err := DefaultExecutor()
		if err != nil {
			t.Fatalf("DefaultExecutor: %v", err)
		}
		if _, ok := exec.(Local); !ok {
			t.Fatalf("%s=%q gave %T, want Local", EnvSandbox, value, exec)
		}
	}

	// Anything else is not an off switch — a typo must not silently
	// unconfine the agent.
	for _, value := range []string{"", "on", "1", "yes", "OFF "} {
		t.Setenv(EnvSandbox, value)
		if Unconfined() {
			t.Fatalf("%s=%q should not unconfine", EnvSandbox, value)
		}
	}
// §foot page/pkg/subagent/policy_test.go TestUnconfinedIsOptIn