// §head page/pkg/config/config_test.go:44-52 TestLoadMissingFile
// §sig func TestLoadMissingFile(t *testing.T)
	c, err := Load("/nonexistent/config.toml")
	if err != nil {
		t.Fatalf("missing file should not error: %v", err)
	}
	if c.LLM.Provider != "ollama" {
		t.Errorf("provider = %q, want ollama (default)", c.LLM.Provider)
	}
// §foot page/pkg/config/config_test.go TestLoadMissingFile