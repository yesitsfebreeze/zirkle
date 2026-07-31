// §head page/pkg/comp/rank_test.go:19-28 TestRankTagVsDescription
// §sig func TestRankTagVsDescription(t *testing.T)
	rows := []Shard{
		{Name: "other", Description: "check stuff", Tags: nil, UseWhen: ""},
		{Name: "tagged", Description: "", Tags: []string{"check"}, UseWhen: ""},
	}
	got := Rank(rows, "check")
	if got[0].Name != "tagged" {
		t.Errorf("tag match (+2) should beat description match (+1), got %q", got[0].Name)
	}
// §foot page/pkg/comp/rank_test.go TestRankTagVsDescription