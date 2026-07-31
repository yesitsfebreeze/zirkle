// §head page/pkg/comp/parser_test.go:132-141 TestExtractJustBlock
// §sig func TestExtractJustBlock(t *testing.T)
	body := "Text before.\n\n" + "```just\n[unix]\nfoo:\n    echo hi\n``" + "``\n\nText after."
	jf := extractJustBlock(body)
	if !contains(jf, "echo hi") {
		t.Errorf("Justfile = %q", jf)
	}
	if !contains(jf, "[unix]") {
		t.Errorf("platform guard missing: %q", jf)
	}
// §foot page/pkg/comp/parser_test.go TestExtractJustBlock