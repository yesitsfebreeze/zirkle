// §head page/pkg/config/config_test.go:165-178 TestStoreDirTildeExpansion
// §sig func TestStoreDirTildeExpansion(t *testing.T)
	c, err := Load("/nonexistent/config.toml")
	if err != nil {
		t.Fatal(err)
	}
	if c.Store.Dir == "~/.relay" {
		t.Error("store.dir not expanded")
	}
	home, _ := os.UserHomeDir()
	want := filepath.Join(home, ".relay")
	if c.Store.Dir != want {
		t.Errorf("store.dir = %q, want %q", c.Store.Dir, want)
	}
// §foot page/pkg/config/config_test.go TestStoreDirTildeExpansion