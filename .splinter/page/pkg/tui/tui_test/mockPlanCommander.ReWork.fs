// §head page/pkg/tui/tui_test.go:594-597 mockPlanCommander.ReWork
// §sig func (m *mockPlanCommander) ReWork(ctx context.Context, convID string, correction string) (string, error)
	m.reworkCalled = true
	return "rework finished", nil
// §foot page/pkg/tui/tui_test.go mockPlanCommander.ReWork