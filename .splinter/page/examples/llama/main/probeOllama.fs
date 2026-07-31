// §head page/examples/llama/main.go:73-107 probeOllama
// §sig func probeOllama(baseURL, model string) error
	probe := &http.Client{Timeout: 5 * time.Second}
	r, err := probe.Get(baseURL + "/api/version")
	if err != nil {
		return fmt.Errorf("ollama not reachable at %s — start it with 'ollama serve', or set OLLAMA_HOST (%w)", baseURL, err)
	}
	r.Body.Close()
	if r.StatusCode != http.StatusOK {
		return fmt.Errorf("ollama /api/version returned HTTP %d", r.StatusCode)
	}

	r, err = probe.Get(baseURL + "/api/tags")
	if err != nil {
		return fmt.Errorf("ollama /api/tags: %w", err)
	}
	defer r.Body.Close()
	body, err := io.ReadAll(r.Body)
	if err != nil {
		return fmt.Errorf("ollama /api/tags read: %w", err)
	}
	var tags struct {
		Models []struct {
			Name string `json:"name"`
		} `json:"models"`
	}
	if err := json.Unmarshal(body, &tags); err != nil {
		return fmt.Errorf("ollama /api/tags decode: %w", err)
	}
	for _, m := range tags.Models {
		if m.Name == model {
			return nil
		}
	}
	return fmt.Errorf("model %q not pulled — run: ollama pull %s", model, model)
// §foot page/examples/llama/main.go probeOllama