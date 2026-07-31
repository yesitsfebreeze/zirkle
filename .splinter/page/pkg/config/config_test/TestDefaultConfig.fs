// §head page/pkg/config/config_test.go:22-42 TestDefaultConfig
// §sig func TestDefaultConfig(t *testing.T)
	c := Default()
	if c.Daemon.Port != 9842 {
		t.Errorf("port = %d, want 9842", c.Daemon.Port)
	}
	if c.LLM.Provider != "ollama" {
		t.Errorf("provider = %q, want ollama", c.LLM.Provider)
	}
	if c.Sandbox.Mode != "on" {
		t.Errorf("sandbox.mode = %q, want on", c.Sandbox.Mode)
	}
	if c.Sandbox.SizeMB != 256 {
		t.Errorf("size_mb = %d, want 256", c.Sandbox.SizeMB)
	}
	if !c.Sandbox.Ephemeral {
		t.Error("ephemeral = false, want true")
	}
	if c.Log.Level != "info" {
		t.Errorf("log.level = %q, want info", c.Log.Level)
	}
// §foot page/pkg/config/config_test.go TestDefaultConfig