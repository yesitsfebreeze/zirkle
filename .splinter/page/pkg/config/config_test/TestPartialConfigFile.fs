// §head page/pkg/config/config_test.go:211-231 TestPartialConfigFile
// §sig func TestPartialConfigFile(t *testing.T)
	path := writeConfigFile(t, `
[llm]
provider = "openai"
`)
	c, err := Load(path)
	if err != nil {
		t.Fatal(err)
	}
	// Overridden value from file.
	if c.LLM.Provider != "openai" {
		t.Errorf("provider = %q, want openai", c.LLM.Provider)
	}
	// Non-overridden values keep defaults.
	if c.Daemon.Port != 9842 {
		t.Errorf("port = %d, want 9842 (default)", c.Daemon.Port)
	}
	if c.Sandbox.Mode != "on" {
		t.Errorf("sandbox.mode = %q, want on (default)", c.Sandbox.Mode)
	}
// §foot page/pkg/config/config_test.go TestPartialConfigFile