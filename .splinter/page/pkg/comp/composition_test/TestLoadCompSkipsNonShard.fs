// §head page/pkg/comp/composition_test.go:64-82 TestLoadCompSkipsNonShard
// §sig func TestLoadCompSkipsNonShard(t *testing.T)
	dir := t.TempDir()
	shardsDir := filepath.Join(dir, ".relay", "shards")
	os.MkdirAll(shardsDir, 0o755)
	os.WriteFile(filepath.Join(shardsDir, "README.md"), []byte("not a shard"), 0o644)
	os.WriteFile(filepath.Join(shardsDir, "real.shard"), []byte("---\nname: real\n---\nBody.\n"), 0o644)
	db, _ := sql.Open("sqlite", ":memory:")
	defer db.Close()
	s := Open(db)
	s.EnsureSchema()
	_, err := LoadComp(dir, s)
	if err != nil {
		t.Fatal(err)
	}
	all, _ := s.All()
	if len(all) != 1 {
		t.Errorf("expected 1 shard, got %d", len(all))
	}
// §foot page/pkg/comp/composition_test.go TestLoadCompSkipsNonShard