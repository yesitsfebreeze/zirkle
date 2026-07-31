// §head page/pkg/llm/anthropic.go:28-41 NewAnthropic
// §sig func NewAnthropic(apiKey, model string) *Anthropic
	if apiKey == "" {
		apiKey = os.Getenv("ANTHROPIC_API_KEY")
	}
	if model == "" {
		model = defaultModel
	}
	return &Anthropic{
		APIKey:  apiKey,
		BaseURL: "https://api.anthropic.com",
		Model:   model,
		HTTP:    &http.Client{Timeout: 60 * time.Second},
	}
// §foot page/pkg/llm/anthropic.go NewAnthropic