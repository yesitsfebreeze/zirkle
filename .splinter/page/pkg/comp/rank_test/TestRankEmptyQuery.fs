// §head page/pkg/comp/rank_test.go:68-76 TestRankEmptyQuery
// §sig func TestRankEmptyQuery(t *testing.T)
	rows := []Shard{
		{Name: "a"}, {Name: "b"},
	}
	got := Rank(rows, "")
	if len(got) != 2 {
		t.Errorf("empty query should return all, got %d", len(got))
	}
// §foot page/pkg/comp/rank_test.go TestRankEmptyQuery