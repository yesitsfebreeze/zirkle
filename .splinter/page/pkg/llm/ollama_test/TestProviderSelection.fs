// §head page/pkg/llm/ollama_test.go:216-238 TestProviderSelection
// §sig func TestProviderSelection(t *testing.T)
	t.Setenv("RELAY_LLM_PROVIDER", "")
	t.Setenv("RELAY_MODEL", "")

	l, err := New("", "")
	if err != nil {
		t.Fatal(err)
	}
	if _, ok := l.(*Ollama); !ok {
		t.Fatalf("default provider = %T, want *Ollama", l)
	}

	if l, err = New("anthropic", "claude-sonnet-4-20250514"); err != nil {
		t.Fatal(err)
	}
	if _, ok := l.(*Anthropic); !ok {
		t.Fatalf("anthropic provider = %T, want *Anthropic", l)
	}

	if _, err = New("gpt5-turbo-ultra", ""); err == nil {
		t.Fatal("want error for unknown provider")
	}
// §foot page/pkg/llm/ollama_test.go TestProviderSelection