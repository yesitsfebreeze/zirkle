// §head page/pkg/llm/ollama_test.go:240-249 TestProviderEnvOverride
// §sig func TestProviderEnvOverride(t *testing.T)
	t.Setenv("RELAY_LLM_PROVIDER", "anthropic")
	l, err := New("", "")
	if err != nil {
		t.Fatal(err)
	}
	if _, ok := l.(*Anthropic); !ok {
		t.Fatalf("RELAY_LLM_PROVIDER=anthropic gave %T, want *Anthropic", l)
	}
// §foot page/pkg/llm/ollama_test.go TestProviderEnvOverride