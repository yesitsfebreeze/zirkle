// §head page/pkg/tui/tui_test.go:599-608 mockPlanCommander.GetConversation
// §sig func (m *mockPlanCommander) GetConversation(convID string) (*plan.Conversation, error)
	return &plan.Conversation{
		ID:    convID,
		State: "planning",
		Intent: plan.Intent{
			Prompt: "my plan",
			Todos:  []string{"step 1", "step 2"},
		},
	}, nil
// §foot page/pkg/tui/tui_test.go mockPlanCommander.GetConversation