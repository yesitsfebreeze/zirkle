// §head page/pkg/llm/ollama.go:376-392 toOllamaTools
// §sig func toOllamaTools(tools []Tool) []ollamaTool
	if len(tools) == 0 {
		return nil
	}
	out := make([]ollamaTool, len(tools))
	for i, t := range tools {
		out[i] = ollamaTool{
			Type: "function",
			Function: ollamaToolSpec{
				Name:        t.Name,
				Description: t.Description,
				Parameters:  t.InputSchema,
			},
		}
	}
	return out
// §foot page/pkg/llm/ollama.go toOllamaTools