// §head page/pkg/tui/tui_test.go:122-144 TestSelectionHighlightsNewestRow
// §sig func TestSelectionHighlightsNewestRow(t *testing.T)
	m := New(mockSource{views: testViews()}, nil, nil)
	got, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	got, _ = got.(Model).Update(refreshMsg(testViews()))
	mm := got.(Model)

	if mm.cursor != 0 {
		t.Fatalf("cursor = %d, want the newest row 0", mm.cursor)
	}
	if idx := mm.selectedIdx(); idx != 0 {
		t.Fatalf("selectedIdx = %d, want 0 (beta, newest)", idx)
	}

	styled := selectedStyle.Render("x")
	if !strings.Contains(styled, "\x1b[") {
		t.Skip("lipgloss produced no ANSI in this environment")
	}
	for _, row := range strings.Split(mm.renderTree(), "\n") {
		if strings.Contains(row, "beta") && !strings.Contains(row, "\x1b[") {
			t.Fatalf("selected row is not highlighted: %q", row)
		}
	}
// §foot page/pkg/tui/tui_test.go TestSelectionHighlightsNewestRow