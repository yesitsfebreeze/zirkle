// §head page/pkg/agent/agent.go:158-172 Agent.Provision
// §sig func (a *Agent) Provision() error
	if a.ID == "" {
		return fmt.Errorf("agent: empty ID")
	}
	if a.Prompt == "" {
		return fmt.Errorf("agent: empty prompt")
	}
	if a.LLM == nil {
		return fmt.Errorf("agent: nil LLM")
	}
	if a.Budget == 0 {
		a.Budget = defaultBudget
	}
	return nil
// §foot page/pkg/agent/agent.go Agent.Provision