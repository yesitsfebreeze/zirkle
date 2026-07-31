// §head page/pkg/llm/ollama.go:84-86 init
// §sig func init()
	Register("ollama", func(model string) LLM { return NewOllama("", model) })
// §foot page/pkg/llm/ollama.go init