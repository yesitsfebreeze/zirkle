// §head page/pkg/comp/parser_test.go:125-130 TestScanLinksNone
// §sig func TestScanLinksNone(t *testing.T)
	links := scanLinks("no links here")
	if len(links) != 0 {
		t.Errorf("expected 0 links, got %v", links)
	}
// §foot page/pkg/comp/parser_test.go TestScanLinksNone