// §head page/pkg/tui/tui_test.go:500-513 TestPlusNewButtonPreservedAtTop
// §sig func TestPlusNewButtonPreservedAtTop(t *testing.T)
	views := []PodView{
		{ID: "+ new", State: "ready"},
		{ID: "pod-1", State: "running"},
		{ID: "pod-2", State: "done"},
	}
	res := reverseGroups(views)
	if len(res) != 3 {
		t.Fatalf("expected 3 views, got %d", len(res))
	}
	if res[0].ID != "+ new" {
		t.Errorf("expected + new at index 0, got %s", res[0].ID)
	}
// §foot page/pkg/tui/tui_test.go TestPlusNewButtonPreservedAtTop