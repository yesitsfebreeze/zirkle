// §head page/pkg/llm/ollama.go:72-79 defaultOllamaURLForHost
// §sig func defaultOllamaURLForHost() string
	if isWSL() {
		if gw := wslGatewayIP(); gw != "" {
			return "http://" + gw + ":11434"
		}
	}
	return defaultOllamaURL
// §foot page/pkg/llm/ollama.go defaultOllamaURLForHost