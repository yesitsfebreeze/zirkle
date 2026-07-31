// §head page/pkg/tui/tui_test.go:165-184 TestUpAtTopExitsToInput
// §sig func TestUpAtTopExitsToInput(t *testing.T)
	m := New(mockSource{views: testViews()}, nil, nil)
	got, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	got, _ = got.(Model).Update(refreshMsg(testViews()))
	mm := got.(Model)
	if mm.pane != 0 {
		t.Fatalf("start: pane = %d, want 0 (pods)", mm.pane)
	}
	if mm.cursor != 0 {
		t.Fatalf("start: cursor = %d, want 0 (top)", mm.cursor)
	}
	got, _ = mm.Update(tea.KeyMsg{Type: tea.KeyUp})
	mm = got.(Model)
	if mm.pane != 1 {
		t.Fatalf("up at top: pane = %d, want 1 (input)", mm.pane)
	}
	if !mm.input.Focused() {
		t.Fatal("up at top: input not focused")
	}
// §foot page/pkg/tui/tui_test.go TestUpAtTopExitsToInput