// §head page/pkg/agent/agent_test.go:171-180 TestHandleToolCallRejectsEmptyPrompt
// §sig func TestHandleToolCallRejectsEmptyPrompt(t *testing.T)
	a := &Agent{ID: "parent-1"}
	out := a.handleToolCall(context.Background(), &llm.ToolCall{
		Name:  "spawn",
		Input: map[string]any{"prompt": ""},
	})
	if !strings.Contains(out, "ERROR") {
		t.Fatalf("got %q, want an error for empty prompt", out)
	}
// §foot page/pkg/agent/agent_test.go TestHandleToolCallRejectsEmptyPrompt