// §head page/pkg/llm/anthropic.go:24-26 init
// §sig func init()
	Register("anthropic", func(model string) LLM { return NewAnthropic("", model) })
// §foot page/pkg/llm/anthropic.go init