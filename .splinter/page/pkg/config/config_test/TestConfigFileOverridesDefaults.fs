// §head page/pkg/config/config_test.go:64-112 TestConfigFileOverridesDefaults
// §sig func TestConfigFileOverridesDefaults(t *testing.T)
	path := writeConfigFile(t, `
[daemon]
port = 7777
socket = "/custom/relay.sock"

[llm]
provider = "anthropic"
model = "claude-3"
max_tokens = 50000

[sandbox]
mode = "off"
size_mb = 512
allowed_domains = ["api.example.com", "*.internal.org"]
denied_domains = ["evil.com"]
`)
	c, err := Load(path)
	if err != nil {
		t.Fatal(err)
	}
	if c.Daemon.Port != 7777 {
		t.Errorf("port = %d, want 7777", c.Daemon.Port)
	}
	if c.Daemon.Socket != "/custom/relay.sock" {
		t.Errorf("socket = %q, want /custom/relay.sock", c.Daemon.Socket)
	}
	if c.LLM.Provider != "anthropic" {
		t.Errorf("provider = %q, want anthropic", c.LLM.Provider)
	}
	if c.LLM.Model != "claude-3" {
		t.Errorf("model = %q, want claude-3", c.LLM.Model)
	}
	if c.LLM.MaxTokens != 50000 {
		t.Errorf("max_tokens = %d, want 50000", c.LLM.MaxTokens)
	}
	if c.Sandbox.Mode != "off" {
		t.Errorf("sandbox.mode = %q, want off", c.Sandbox.Mode)
	}
	if c.Sandbox.SizeMB != 512 {
		t.Errorf("size_mb = %d, want 512", c.Sandbox.SizeMB)
	}
	if !reflect.DeepEqual(c.Sandbox.AllowedDomains, []string{"api.example.com", "*.internal.org"}) {
		t.Errorf("allowed_domains = %v", c.Sandbox.AllowedDomains)
	}
	if !reflect.DeepEqual(c.Sandbox.DeniedDomains, []string{"evil.com"}) {
		t.Errorf("denied_domains = %v", c.Sandbox.DeniedDomains)
	}
// §foot page/pkg/config/config_test.go TestConfigFileOverridesDefaults