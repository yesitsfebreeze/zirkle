// §head page/pkg/config/config_test.go:276-297 TestEnsureDefaultCreatesThenPreserves
// §sig func TestEnsureDefaultCreatesThenPreserves(t *testing.T)
	dir := t.TempDir()
	t.Setenv("HOME", dir)
	path, err := EnsureDefault()
	if err != nil {
		t.Fatal(err)
	}
	if _, err := os.Stat(path); err != nil {
		t.Fatalf("config not written: %v", err)
	}
	// Corrupt it; second call must NOT overwrite (existing file is sacred).
	if err := os.WriteFile(path, []byte("# user edit"), 0o644); err != nil {
		t.Fatal(err)
	}
	if _, err := EnsureDefault(); err != nil {
		t.Fatal(err)
	}
	body, _ := os.ReadFile(path)
	if string(body) != "# user edit" {
		t.Error("EnsureDefault overwrote an existing user config")
	}
// §foot page/pkg/config/config_test.go TestEnsureDefaultCreatesThenPreserves