// §head page/pkg/config/config_test.go:114-147 TestEnvOverridesConfigFile
// §sig func TestEnvOverridesConfigFile(t *testing.T)
	path := writeConfigFile(t, `
[llm]
provider = "anthropic"
model = "claude-3"
[webhook]
secret = "filesecret"
`)
	t.Setenv("RELAY_LLM_PROVIDER", "openai")
	t.Setenv("RELAY_MODEL", "gpt-4")
	t.Setenv("RELAY_WEBHOOK_SECRET", "envsecret")
	t.Setenv("RELAY_WEBHOOK_PORT", "6666")
	t.Setenv("RELAY_SANDBOX", "off")

	c, err := Load(path)
	if err != nil {
		t.Fatal(err)
	}
	if c.LLM.Provider != "openai" {
		t.Errorf("provider = %q, want openai (env override)", c.LLM.Provider)
	}
	if c.LLM.Model != "gpt-4" {
		t.Errorf("model = %q, want gpt-4 (env override)", c.LLM.Model)
	}
	if c.Webhook.Secret != "envsecret" {
		t.Errorf("webhook.secret = %q, want envsecret", c.Webhook.Secret)
	}
	if c.Daemon.Port != 6666 {
		t.Errorf("port = %d, want 6666 (env override)", c.Daemon.Port)
	}
	if c.Sandbox.Mode != "off" {
		t.Errorf("sandbox.mode = %q, want off (env override)", c.Sandbox.Mode)
	}
// §foot page/pkg/config/config_test.go TestEnvOverridesConfigFile