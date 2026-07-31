// §head page/pkg/llm/provider.go:53-68 New
// §sig func New(provider, model string) (LLM, error)
	if provider == "" {
		provider = os.Getenv("RELAY_LLM_PROVIDER")
	}
	if provider == "" {
		provider = DefaultProvider
	}
	if model == "" {
		model = os.Getenv("RELAY_MODEL")
	}
	f, ok := lookup(provider)
	if !ok {
		return nil, fmt.Errorf("llm: unknown provider %q (have %v)", provider, providerNames())
	}
	return f(model), nil
// §foot page/pkg/llm/provider.go New