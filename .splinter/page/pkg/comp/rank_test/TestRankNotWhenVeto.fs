// §head page/pkg/comp/rank_test.go:41-56 TestRankNotWhenVeto
// §sig func TestRankNotWhenVeto(t *testing.T)
	rows := []Shard{
		{Name: "check", Description: "check CI", Tags: []string{"ci"}, UseWhen: "CI check", NotWhen: "check disabled"},
		{Name: "check-backup", Description: "check backup status", Tags: nil, UseWhen: ""},
	}
	got := Rank(rows, "check")
	if len(got) == 0 {
		t.Fatal("expected results")
	}
	if got[0].Name == "check" {
		t.Error("not_when match should veto — check should have score 0, not first")
	}
	if got[0].Name != "check-backup" {
		t.Errorf("expected check-backup first, got %q", got[0].Name)
	}
// §foot page/pkg/comp/rank_test.go TestRankNotWhenVeto