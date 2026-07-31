// §head page/pkg/comp/composition.go:15-43 LoadComp
// §sig func LoadComp(root string, store *Store) (*Composition, error)
	shardsDir := filepath.Join(root, ".relay", "shards")
	entries, err := os.ReadDir(shardsDir)
	if err != nil {
		if os.IsNotExist(err) {
			return &Composition{Root: root, Store: store}, nil
		}
		return nil, err
	}
	for _, entry := range entries {
		if entry.IsDir() || !strings.HasSuffix(entry.Name(), ".shard") {
			continue
		}
		path := filepath.Join(shardsDir, entry.Name())
		content, err := os.ReadFile(path)
		if err != nil {
			return nil, err
		}
		rel := filepath.Join(".relay", "shards", entry.Name())
		shard, err := Parse(rel, string(content))
		if err != nil {
			return nil, err
		}
		if err := store.Index(shard); err != nil {
			return nil, err
		}
	}
	return &Composition{Root: root, Store: store}, nil
// §foot page/pkg/comp/composition.go LoadComp