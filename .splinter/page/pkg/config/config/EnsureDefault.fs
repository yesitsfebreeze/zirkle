// §head page/pkg/config/config.go:166-186 EnsureDefault
// §sig func EnsureDefault() (string, error)
	path, err := DefaultPath()
	if err != nil {
		return "", err
	}
	if _, err := os.Stat(path); err == nil {
		return path, nil // already present, leave it alone
	}
	dir := filepath.Dir(path)
	if err := os.MkdirAll(dir, 0o755); err != nil {
		return "", fmt.Errorf("config: %w", err)
	}
	body, err := defaultConfigFS.ReadFile("default.toml")
	if err != nil {
		return "", fmt.Errorf("config: %w", err)
	}
	if err := os.WriteFile(path, body, 0o644); err != nil {
		return "", fmt.Errorf("config: %w", err)
	}
	return path, nil
// §foot page/pkg/config/config.go EnsureDefault