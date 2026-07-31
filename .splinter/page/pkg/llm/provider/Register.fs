// §head page/pkg/llm/provider.go:26-30 Register
// §sig func Register(id string, f providerFactory)
	providersMu.Lock()
	defer providersMu.Unlock()
	providers[id] = f
// §foot page/pkg/llm/provider.go Register