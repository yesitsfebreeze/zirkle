// §head page/pkg/tui/tui_test.go:579-588 mockPlanCommander.Approve
// §sig func (m *mockPlanCommander) Approve(ctx context.Context, convID string) (*plan.Conversation, error)
	m.approveCalled = true
	return &plan.Conversation{
		ID:    convID,
		State: "approved",
		ApprovedPlan: &plan.ApprovedPlan{
			Prompt: "approved prompt",
		},
	}, nil
// §foot page/pkg/tui/tui_test.go mockPlanCommander.Approve