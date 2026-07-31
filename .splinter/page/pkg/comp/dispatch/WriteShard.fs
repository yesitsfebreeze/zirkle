// §head page/pkg/comp/dispatch.go:159-165 WriteShard
// §sig func WriteShard(compDir, filename, content string) error
	shardsDir := filepath.Join(compDir, ".relay", "shards")
	if err := os.MkdirAll(shardsDir, 0o755); err != nil {
		return err
	}
	return os.WriteFile(filepath.Join(shardsDir, filename), []byte(content), 0o644)
// §foot page/pkg/comp/dispatch.go WriteShard