// §head page/pkg/comp/parser_test.go:48-86 TestParse
// §sig func TestParse(t *testing.T)
	shard, err := Parse("shards/check-ci.shard", testShard)
	if err != nil {
		t.Fatal(err)
	}
	if shard.Name != "check-ci" {
		t.Errorf("Name = %q", shard.Name)
	}
	if shard.Kind != "tool" {
		t.Errorf("Kind = %q", shard.Kind)
	}
	if shard.Description != "Check CI pipeline status" {
		t.Errorf("Description = %q", shard.Description)
	}
	if len(shard.Tags) != 2 || shard.Tags[0] != "ci" || shard.Tags[1] != "check" {
		t.Errorf("Tags = %v", shard.Tags)
	}
	if shard.UseWhen != "CI build is failing" {
		t.Errorf("UseWhen = %q", shard.UseWhen)
	}
	if shard.NotWhen != "no CI configured" {
		t.Errorf("NotWhen = %q", shard.NotWhen)
	}
	if len(shard.Requires) != 2 || shard.Requires[0] != "gh" || shard.Requires[1] != "jq" {
		t.Errorf("Requires = %v", shard.Requires)
	}
	if shard.Run != "check-ci" {
		t.Errorf("Run = %q", shard.Run)
	}
	if !shard.HasFM {
		t.Error("HasFM should be true")
	}
	if shard.Justfile == "" || !contains(shard.Justfile, "gh run list") {
		t.Errorf("Justfile = %q", shard.Justfile)
	}
	if !contains(shard.Justfile, "[unix]") {
		t.Errorf("Justfile should contain platform guard: %q", shard.Justfile)
	}
// §foot page/pkg/comp/parser_test.go TestParse