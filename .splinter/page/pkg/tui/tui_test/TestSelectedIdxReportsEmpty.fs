// §head page/pkg/tui/tui_test.go:240-245 TestSelectedIdxReportsEmpty
// §sig func TestSelectedIdxReportsEmpty(t *testing.T)
	m := New(mockSource{views: nil}, nil, nil)
	if got := m.selectedIdx(); got != -1 {
		t.Fatalf("selectedIdx on empty views = %d, want -1", got)
	}
// §foot page/pkg/tui/tui_test.go TestSelectedIdxReportsEmpty