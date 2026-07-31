// §head page/pkg/comp/store_test.go:66-85 TestSearch
// §sig func TestSearch(t *testing.T)
	s := testStore(t)
	shards := []*Shard{
		{Key: "a", Name: "check-ci", Description: "Check CI", Tags: []string{"ci"}},
		{Key: "b", Name: "deploy", Description: "Deploy stuff", Tags: []string{"deploy"}},
	}
	for _, sh := range shards {
		if err := s.Index(sh); err != nil {
			t.Fatal(err)
		}
	}

	got, err := s.Search("check")
	if err != nil {
		t.Fatal(err)
	}
	if len(got) != 1 || got[0].Name != "check-ci" {
		t.Errorf("expected 1 result (check-ci), got %d: %v", len(got), got)
	}
// §foot page/pkg/comp/store_test.go TestSearch