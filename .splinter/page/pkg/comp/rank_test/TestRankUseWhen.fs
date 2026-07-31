// §head page/pkg/comp/rank_test.go:30-39 TestRankUseWhen
// §sig func TestRankUseWhen(t *testing.T)
	rows := []Shard{
		{Name: "alpha", Description: "check", Tags: nil, UseWhen: ""},
		{Name: "beta", Description: "", Tags: nil, UseWhen: "check failing"},
	}
	got := Rank(rows, "check")
	if got[0].Name != "beta" {
		t.Errorf("use_when match (+3) should beat description (+1), got %q", got[0].Name)
	}
// §foot page/pkg/comp/rank_test.go TestRankUseWhen