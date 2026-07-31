// §head page/pkg/agent/agent_test.go:182-191 TestHandleToolCallRejectsUnknownTool
// §sig func TestHandleToolCallRejectsUnknownTool(t *testing.T)
	a := &Agent{ID: "parent-1"}
	out := a.handleToolCall(context.Background(), &llm.ToolCall{
		Name:  "frobnicate",
		Input: map[string]any{},
	})
	if !strings.Contains(out, "unknown tool") {
		t.Fatalf("got %q, want unknown tool error", out)
	}
// §foot page/pkg/agent/agent_test.go TestHandleToolCallRejectsUnknownTool