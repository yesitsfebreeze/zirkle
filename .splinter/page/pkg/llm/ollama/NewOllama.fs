// §head page/pkg/llm/ollama.go:88-108 NewOllama
// §sig func NewOllama(baseURL, model string) *Ollama
	if baseURL == "" {
		baseURL = os.Getenv("OLLAMA_HOST")
	}
	if baseURL == "" {
		baseURL = defaultOllamaURLForHost()
	}
	if !strings.HasPrefix(baseURL, "http://") && !strings.HasPrefix(baseURL, "https://") {
		baseURL = "http://" + baseURL
	}
	if model == "" {
		model = defaultOllamaModel
	}
	return &Ollama{
		BaseURL: strings.TrimRight(baseURL, "/"),
		Model:   model,
		// Local models on CPU are slow; a cloud-sized 60s deadline truncates
		// the first token far too often.
		HTTP: &http.Client{Timeout: 10 * time.Minute},
	}
// §foot page/pkg/llm/ollama.go NewOllama