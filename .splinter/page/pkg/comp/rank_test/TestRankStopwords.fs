// §head page/pkg/comp/rank_test.go:58-66 TestRankStopwords
// §sig func TestRankStopwords(t *testing.T)
	rows := []Shard{
		{Name: "check-ci", Description: "", Tags: nil, UseWhen: ""},
	}
	got := Rank(rows, "the check")
	if len(got) == 0 || got[0].Name != "check-ci" {
		t.Error("stopword 'the' should be filtered, 'check' should match")
	}
// §foot page/pkg/comp/rank_test.go TestRankStopwords