// §head page/pkg/agent/agent_test.go:113-136 TestProvisionRejectsBadConfig
// §sig func TestProvisionRejectsBadConfig(t *testing.T)
	cases := []struct {
		name string
		a    *Agent
		want string
	}{
		{name: "empty ID", a: &Agent{Prompt: "p", LLM: &fakeLLM{}}, want: "empty ID"},
		{name: "empty prompt", a: &Agent{ID: "x", LLM: &fakeLLM{}}, want: "empty prompt"},
		{name: "nil LLM", a: &Agent{ID: "x", Prompt: "p"}, want: "nil LLM"},
	}
	for _, c := range cases {
		if err := c.a.Provision(); err == nil || !strings.Contains(err.Error(), c.want) {
			t.Fatalf("%s: err=%v want %q", c.name, err, c.want)
		}
	}
	// Valid config provisions clean and defaults the budget.
	a := &Agent{ID: "x", Prompt: "p", LLM: &fakeLLM{}}
	if err := a.Provision(); err != nil {
		t.Fatalf("valid: %v", err)
	}
	if a.Budget != defaultBudget {
		t.Fatalf("budget = %d want %d", a.Budget, defaultBudget)
	}
// §foot page/pkg/agent/agent_test.go TestProvisionRejectsBadConfig