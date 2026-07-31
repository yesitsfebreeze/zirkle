// §head page/pkg/comp/rank_test.go:5-17 TestRankNameMatch
// §sig func TestRankNameMatch(t *testing.T)
	rows := []Shard{
		{Name: "check-ci", Description: "", Tags: nil, UseWhen: ""},
		{Name: "deploy", Description: "deploy things", Tags: []string{"deploy"}, UseWhen: ""},
	}
	got := Rank(rows, "check")
	if len(got) == 0 {
		t.Fatal("expected results")
	}
	if got[0].Name != "check-ci" {
		t.Errorf("expected check-ci first, got %q", got[0].Name)
	}
// §foot page/pkg/comp/rank_test.go TestRankNameMatch