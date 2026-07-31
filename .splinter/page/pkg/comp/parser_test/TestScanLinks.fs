// §head page/pkg/comp/parser_test.go:108-123 TestScanLinks
// §sig func TestScanLinks(t *testing.T)
	body := "See @check-ci and @deploy/rollback for more. Also @?fuzzy."
	links := scanLinks(body)
	if len(links) != 3 {
		t.Fatalf("expected 3 links, got %d: %v", len(links), links)
	}
	if links[0] != "@check-ci" {
		t.Errorf("links[0] = %q", links[0])
	}
	if links[1] != "@deploy/rollback" {
		t.Errorf("links[1] = %q", links[1])
	}
	if links[2] != "@?fuzzy" {
		t.Errorf("links[2] = %q", links[2])
	}
// §foot page/pkg/comp/parser_test.go TestScanLinks