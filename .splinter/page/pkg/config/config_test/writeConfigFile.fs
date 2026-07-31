// §head page/pkg/config/config_test.go:12-20 writeConfigFile
// §sig func writeConfigFile(t *testing.T, body string) string
	t.Helper()
	dir := t.TempDir()
	path := filepath.Join(dir, "config.toml")
	if err := os.WriteFile(path, []byte(body), 0o644); err != nil {
		t.Fatal(err)
	}
	return path
// §foot page/pkg/config/config_test.go writeConfigFile