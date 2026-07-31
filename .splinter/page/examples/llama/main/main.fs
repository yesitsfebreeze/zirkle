// §head page/examples/llama/main.go:21-69 main
// §sig func main()
	model := defaultModel
	if m := os.Getenv("RELAY_MODEL"); m != "" {
		model = m
	}
	if len(os.Args) > 1 && os.Args[1] != "" {
		model = os.Args[1]
	}

	baseURL := os.Getenv("OLLAMA_HOST")
	if baseURL == "" {
		baseURL = "localhost:11434"
	}
	if !strings.HasPrefix(baseURL, "http://") && !strings.HasPrefix(baseURL, "https://") {
		baseURL = "http://" + baseURL
	}
	baseURL = strings.TrimRight(baseURL, "/")

	// Probe Ollama before chatting — same gate as `just smoke`.
	if err := probeOllama(baseURL, model); err != nil {
		log.Fatalf("probe: %v", err)
	}

	client := llm.NewOllama(baseURL, model)
	ctx, cancel := context.WithTimeout(context.Background(), 10*time.Minute)
	defer cancel()

	resp, err := client.Chat(ctx, llm.ChatRequest{
		Model: model,
		Messages: []llm.Message{
			{Role: "user", Content: "Reply with exactly: relay online"},
		},
	})
	if err != nil {
		log.Fatalf("chat: %v", err)
	}

	fmt.Printf("response: %s\n", resp.Message.Content)
	fmt.Printf("usage: input=%d output=%d\n", resp.Usage.InputTokens, resp.Usage.OutputTokens)

	// Self-check: round-trip must produce content and tokens.
	if strings.TrimSpace(resp.Message.Content) == "" {
		log.Fatalf("self-check failed: response content is empty")
	}
	if resp.Usage.OutputTokens == 0 {
		log.Fatalf("self-check failed: output tokens = 0 (expected > 0)")
	}
	fmt.Println("self-check passed")
// §foot page/examples/llama/main.go main