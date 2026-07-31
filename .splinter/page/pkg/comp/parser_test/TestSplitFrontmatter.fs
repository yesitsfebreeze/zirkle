// §head page/pkg/comp/parser_test.go:24-35 TestSplitFrontmatter
// §sig func TestSplitFrontmatter(t *testing.T)
	fm, body := splitFrontmatter(testShard)
	if fm == "" {
		t.Fatal("expected frontmatter")
	}
	if !contains(fm, "name: check-ci") {
		t.Errorf("frontmatter missing name: got %q", fm)
	}
	if !contains(body, "Check CI pipeline") {
		t.Errorf("body missing content: got %q", body)
	}
// §foot page/pkg/comp/parser_test.go TestSplitFrontmatter