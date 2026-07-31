// §head page/pkg/llm/ollama.go:359-374 toOllamaMsg
// §sig func toOllamaMsg(m Message) ollamaMsg
	om := ollamaMsg{Role: m.Role, Content: m.Content}
	if m.ToolUse != nil {
		om.ToolCalls = []ollamaToolCall{{
			Function: ollamaToolCallFn{
				Name:      m.ToolUse.Name,
				Arguments: m.ToolUse.Input,
			},
		}}
	}
	if m.ToolResult != nil {
		om.Role = "tool"
		om.Content = m.ToolResult.Content
	}
	return om
// §foot page/pkg/llm/ollama.go toOllamaMsg