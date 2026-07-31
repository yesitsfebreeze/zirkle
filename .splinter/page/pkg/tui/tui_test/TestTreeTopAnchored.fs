// §head page/pkg/tui/tui_test.go:205-220 TestTreeTopAnchored
// §sig func TestTreeTopAnchored(t *testing.T)
	m := New(mockSource{views: testViews()}, nil, nil)
	got, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	got, _ = got.(Model).Update(refreshMsg(testViews()))
	mm := got.(Model)

	content := mm.treeContent()
	rows := strings.Split(content, "\n")
	// Top-anchored: newest pod (beta) is the first row, no padding.
	if strings.TrimSpace(rows[0]) == "" {
		t.Fatalf("row 0 is blank, want newest pod at top")
	}
	if !strings.Contains(rows[0], "beta") {
		t.Fatalf("row 0 = %q, want beta (newest) at top", rows[0])
	}
// §foot page/pkg/tui/tui_test.go TestTreeTopAnchored