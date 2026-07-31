// §head page/pkg/llm/anthropic.go:304-317 toAnthropicTools
// §sig func toAnthropicTools(tools []Tool) []anthropicTool
	if len(tools) == 0 {
		return nil
	}
	out := make([]anthropicTool, len(tools))
	for i, t := range tools {
		out[i] = anthropicTool{
			Name:        t.Name,
			Description: t.Description,
			InputSchema: t.InputSchema,
		}
	}
	return out
// §foot page/pkg/llm/anthropic.go toAnthropicTools