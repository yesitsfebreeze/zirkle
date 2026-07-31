// §head page/pkg/agent/agent_test.go:196-229 TestAgentRunToolCallLoop
// §sig func TestAgentRunToolCallLoop(t *testing.T)
	t.Setenv("RELAY_SUBAGENT_RUN", "1")
	t.Setenv(subagent.EnvSandbox, "off")

	a := &Agent{
		ID: "tc-1", Prompt: "check the thing", Model: "test-model",
		LLM: &toolLLM{},
	}
	out, err := a.Run(context.Background())
	if err != nil {
		t.Fatalf("Run: %v", err)
	}
	if out != "done" {
		t.Fatalf("output = %q, want %q", out, "done")
	}
	if a.Recap != "all clear" {
		t.Fatalf("recap = %q, want %q", a.Recap, "all clear")
	}
	if a.turn != 2 {
		t.Fatalf("turns = %d, want 2 (tool call + final)", a.turn)
	}
	if len(a.msgs) != 5 {
		t.Fatalf("msgs = %d, want 5 (system, user, assistant tool call, user tool result, assistant text)", len(a.msgs))
	}
	if a.msgs[0].Role != "system" {
		t.Fatalf("msgs[0] role = %q, want system", a.msgs[0].Role)
	}
	if a.msgs[2].ToolUse == nil {
		t.Fatal("msgs[2] must carry the tool call")
	}
	if a.msgs[3].ToolResult == nil {
		t.Fatal("msgs[3] must carry the tool result")
	}
// §foot page/pkg/agent/agent_test.go TestAgentRunToolCallLoop