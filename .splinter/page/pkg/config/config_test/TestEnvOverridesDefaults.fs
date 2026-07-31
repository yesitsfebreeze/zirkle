// §head page/pkg/config/config_test.go:149-163 TestEnvOverridesDefaults
// §sig func TestEnvOverridesDefaults(t *testing.T)
	t.Setenv("RELAY_LLM_PROVIDER", "anthropic")
	t.Setenv("RELAY_STORE_DIR", "/custom/relay")

	c, err := Load("/nonexistent/config.toml")
	if err != nil {
		t.Fatal(err)
	}
	if c.LLM.Provider != "anthropic" {
		t.Errorf("provider = %q, want anthropic", c.LLM.Provider)
	}
	if c.Store.Dir != "/custom/relay" {
		t.Errorf("store.dir = %q, want /custom/relay", c.Store.Dir)
	}
// §foot page/pkg/config/config_test.go TestEnvOverridesDefaults