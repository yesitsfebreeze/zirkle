// §head page/pkg/agent/agent_test.go:153-169 TestHandleToolCallSpawn
// §sig func TestHandleToolCallSpawn(t *testing.T)
	t.Setenv("RELAY_SUBAGENT_RUN", "1")
	t.Setenv(subagent.EnvSandbox, "off")

	a := &Agent{ID: "parent-1", Model: "test-model", Budget: 1000}
	out := a.handleToolCall(context.Background(), &llm.ToolCall{
		Name:  "spawn",
		Input: map[string]any{"prompt": "go check the thing"},
	})

	if strings.Contains(out, "SPAWN ERROR") {
		t.Fatalf("spawn failed, likely a nanosecond deadline: %q", out)
	}
	if !strings.Contains(out, "test summary") {
		t.Fatalf("got %q, want the subagent summary", out)
	}
// §foot page/pkg/agent/agent_test.go TestHandleToolCallSpawn