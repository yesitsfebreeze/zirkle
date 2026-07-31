// §head page/pkg/tui/tui_test.go:147-162 TestUpDownMoveSelection
// §sig func TestUpDownMoveSelection(t *testing.T)
	m := New(mockSource{views: testViews()}, nil, nil)
	got, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	got, _ = got.(Model).Update(refreshMsg(testViews()))
	mm := got.(Model)
	// Newest (beta) at top = cursor 0. Down moves to alpha (1), up back to beta (0).
	got, _ = mm.Update(tea.KeyMsg{Type: tea.KeyDown})
	mm = got.(Model)
	if mm.cursor != 1 {
		t.Fatalf("cursor = %d after down, want 1", mm.cursor)
	}
	got, _ = mm.Update(tea.KeyMsg{Type: tea.KeyUp})
	if mm = got.(Model); mm.cursor != 0 {
		t.Fatalf("cursor = %d after up, want 0", mm.cursor)
	}
// §foot page/pkg/tui/tui_test.go TestUpDownMoveSelection