// §head page/pkg/comp/parser_test.go:37-46 TestSplitFrontmatterNone
// §sig func TestSplitFrontmatterNone(t *testing.T)
	content := "just a body\nno frontmatter"
	fm, body := splitFrontmatter(content)
	if fm != "" {
		t.Errorf("expected empty frontmatter, got %q", fm)
	}
	if body != content {
		t.Errorf("expected full content as body, got %q", body)
	}
// §foot page/pkg/comp/parser_test.go TestSplitFrontmatterNone