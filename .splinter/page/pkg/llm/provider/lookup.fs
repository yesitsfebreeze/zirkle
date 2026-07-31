// §head page/pkg/llm/provider.go:32-37 lookup
// §sig func lookup(id string) (providerFactory, bool)
	providersMu.RLock()
	defer providersMu.RUnlock()
	f, ok := providers[id]
	return f, ok
// §foot page/pkg/llm/provider.go lookup