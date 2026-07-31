// §head page/pkg/llm/ollama_test.go:91-104 TestOllamaDefaults
// §sig func TestOllamaDefaults(t *testing.T)
	t.Setenv("OLLAMA_HOST", "")
	o := NewOllama("", "")
	if o.BaseURL != defaultOllamaURLForHost() {
		t.Errorf("BaseURL = %q, want %q", o.BaseURL, defaultOllamaURLForHost())
	}
	if o.Model != defaultOllamaModel {
		t.Errorf("Model = %q, want %q", o.Model, defaultOllamaModel)
	}
	// A bare host:port from OLLAMA_HOST must still resolve to a URL.
	if got := NewOllama("box:11434", "m").BaseURL; got != "http://box:11434" {
		t.Errorf("BaseURL = %q, want http://box:11434", got)
	}
// §foot page/pkg/llm/ollama_test.go TestOllamaDefaults