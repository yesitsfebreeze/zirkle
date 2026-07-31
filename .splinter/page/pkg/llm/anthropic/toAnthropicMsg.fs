// §head page/pkg/llm/anthropic.go:277-302 toAnthropicMsg
// §sig func toAnthropicMsg(m Message) anthropicMsg
	if m.ToolUse != nil {
		var blocks []anthropicBlock
		if m.Content != "" {
			blocks = append(blocks, anthropicBlock{Type: "text", Text: m.Content})
		}
		blocks = append(blocks, anthropicBlock{
			Type:  "tool_use",
			ID:    m.ToolUse.ID,
			Name:  m.ToolUse.Name,
			Input: m.ToolUse.Input,
		})
		return anthropicMsg{Role: m.Role, Content: blocks}
	}
	if m.ToolResult != nil {
		return anthropicMsg{
			Role: m.Role,
			Content: []anthropicBlock{{
				Type:      "tool_result",
				ToolUseID: m.ToolResult.ID,
				Result:    m.ToolResult.Content,
			}},
		}
	}
	return anthropicMsg{Role: m.Role, Content: m.Content}
// §foot page/pkg/llm/anthropic.go toAnthropicMsg