// §head page/pkg/comp/composition_test.go:12-47 TestLoadComp
// §sig func TestLoadComp(t *testing.T)
	dir := t.TempDir()
	shardsDir := filepath.Join(dir, ".relay", "shards")
	os.MkdirAll(shardsDir, 0o755)
	shardContent := "---\nname: check-ci\ndescription: Check CI pipeline status\nkind: tool\ntags: [ci, check]\nuse_when: CI build is failing\nrun: check-ci\n---\n\nCheck CI.\n\n" + "```just\ncheck-ci:\n    echo checking\n```"
	err := os.WriteFile(filepath.Join(shardsDir, "check-ci.shard"), []byte(shardContent), 0o644)
	if err != nil {
		t.Fatal(err)
	}
	db, err := sql.Open("sqlite", ":memory:")
	if err != nil {
		t.Fatal(err)
	}
	t.Cleanup(func() { db.Close() })
	s := Open(db)
	if err := s.EnsureSchema(); err != nil {
		t.Fatal(err)
	}
	comp, err := LoadComp(dir, s)
	if err != nil {
		t.Fatal(err)
	}
	if comp == nil {
		t.Fatal("composition is nil")
	}
	all, err := s.All()
	if err != nil {
		t.Fatal(err)
	}
	if len(all) != 1 {
		t.Fatalf("expected 1 shard, got %d", len(all))
	}
	if all[0].Name != "check-ci" {
		t.Errorf("Name = %q", all[0].Name)
	}
// §foot page/pkg/comp/composition_test.go TestLoadComp