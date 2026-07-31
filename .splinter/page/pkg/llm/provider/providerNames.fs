// §head page/pkg/llm/provider.go:39-48 providerNames
// §sig func providerNames() []string
	providersMu.RLock()
	defer providersMu.RUnlock()
	out := make([]string, 0, len(providers))
	for k := range providers {
		out = append(out, k)
	}
	sort.Strings(out)
	return out
// §foot page/pkg/llm/provider.go providerNames