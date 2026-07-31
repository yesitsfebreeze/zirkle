// §head page/pkg/tui/tui_test.go:568-577 mockPlanCommander.Plan
// §sig func (m *mockPlanCommander) Plan(ctx context.Context, prompt string) (*plan.Conversation, error)
	m.planCalled = true
	return &plan.Conversation{
		ID:    "p1",
		State: "planning",
		Intent: plan.Intent{
			Prompt: prompt,
		},
	}, nil
// §foot page/pkg/tui/tui_test.go mockPlanCommander.Plan