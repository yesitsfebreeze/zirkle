// §head page/pkg/config/config_test.go:54-62 TestLoadEmptyPath
// §sig func TestLoadEmptyPath(t *testing.T)
	c, err := Load("")
	if err != nil {
		t.Fatal(err)
	}
	if c.Daemon.Port != 9842 {
		t.Errorf("port = %d, want 9842", c.Daemon.Port)
	}
// §foot page/pkg/config/config_test.go TestLoadEmptyPath