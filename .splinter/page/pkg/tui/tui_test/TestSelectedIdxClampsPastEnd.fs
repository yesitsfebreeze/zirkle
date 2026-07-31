// §head page/pkg/tui/tui_test.go:247-256 TestSelectedIdxClampsPastEnd
// §sig func TestSelectedIdxClampsPastEnd(t *testing.T)
	m := New(mockSource{views: testViews()}, nil, nil)
	got, _ := m.Update(refreshMsg(testViews()))
	mm := got.(Model)
	mm.cursor = 99
	idx := mm.selectedIdx()
	if idx < 0 || idx >= len(mm.views) {
		t.Fatalf("selectedIdx with runaway cursor = %d, want a valid views index", idx)
	}
// §foot page/pkg/tui/tui_test.go TestSelectedIdxClampsPastEnd