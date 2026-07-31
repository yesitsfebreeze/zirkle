// §head page/pkg/comp/parser_test.go:88-106 TestParseNoFrontmatter
// §sig func TestParseNoFrontmatter(t *testing.T)
	content := "Just a body with no frontmatter.\n\n`" + "`" + "`just\nfoo:\n    echo hi\n`" + "`" + "`"
	shard, err := Parse("shards/bare.shard", content)
	if err != nil {
		t.Fatal(err)
	}
	if shard.HasFM {
		t.Error("HasFM should be false")
	}
	if shard.Name != "" {
		t.Errorf("Name should be empty, got %q", shard.Name)
	}
	if !contains(shard.Body, "Just a body") {
		t.Errorf("Body = %q", shard.Body)
	}
	if shard.Justfile == "" {
		t.Error("Justfile should be extracted")
	}
// §foot page/pkg/comp/parser_test.go TestParseNoFrontmatter