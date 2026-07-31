// §head page/pkg/comp/composition_test.go:49-62 TestLoadCompNoShardsDir
// §sig func TestLoadCompNoShardsDir(t *testing.T)
	dir := t.TempDir()
	db, _ := sql.Open("sqlite", ":memory:")
	defer db.Close()
	s := Open(db)
	s.EnsureSchema()
	comp, err := LoadComp(dir, s)
	if err != nil {
		t.Fatalf("missing shards/ dir should not error: %v", err)
	}
	if comp == nil {
		t.Fatal("composition should be non-nil even with no shards")
	}
// §foot page/pkg/comp/composition_test.go TestLoadCompNoShardsDir