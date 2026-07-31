// §head page/pkg/config/config.go:155-161 DefaultPath
// §sig func DefaultPath() (string, error)
	home, err := os.UserHomeDir()
	if err != nil {
		return "", err
	}
	return filepath.Join(home, ".relay", "config.toml"), nil
// §foot page/pkg/config/config.go DefaultPath