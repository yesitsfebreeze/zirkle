// §head page/pkg/comp/parser_test.go:143-148 TestExtractJustBlockNone
// §sig func TestExtractJustBlockNone(t *testing.T)
	jf := extractJustBlock("no code blocks here")
	if jf != "" {
		t.Errorf("expected empty, got %q", jf)
	}
// §foot page/pkg/comp/parser_test.go TestExtractJustBlockNone