// §head page/pkg/comp/store_test.go:87-102 TestSearchAll
// §sig func TestSearchAll(t *testing.T)
	s := testStore(t)
	if err := s.Index(&Shard{Key: "a", Name: "x"}); err != nil {
		t.Fatal(err)
	}
	if err := s.Index(&Shard{Key: "b", Name: "y"}); err != nil {
		t.Fatal(err)
	}
	all, err := s.All()
	if err != nil {
		t.Fatal(err)
	}
	if len(all) != 2 {
		t.Errorf("expected 2, got %d", len(all))
	}
// §foot page/pkg/comp/store_test.go TestSearchAll