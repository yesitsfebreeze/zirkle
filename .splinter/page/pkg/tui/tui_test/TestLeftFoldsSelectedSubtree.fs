// §head page/pkg/tui/tui_test.go:68-88 TestLeftFoldsSelectedSubtree
// §sig func TestLeftFoldsSelectedSubtree(t *testing.T)
	m := New(mockSource{views: testViews()}, nil, nil)
	got, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	got, _ = got.(Model).Update(refreshMsg(testViews()))
	mm := got.(Model)
	mm.cursor = 1 // alpha (root with child), now at index 1 after reverseGroups

	got, _ = mm.Update(tea.KeyMsg{Type: tea.KeyLeft})
	mm = got.(Model)
	if !mm.collapsed[1] {
		t.Fatal("left did not collapse the selected root")
	}
	if len(mm.visible()) != 2 {
		t.Fatalf("want 2 visible rows after collapse, got %d", len(mm.visible()))
	}

	got, _ = mm.Update(tea.KeyMsg{Type: tea.KeyRight})
	if mm = got.(Model); mm.collapsed[1] {
		t.Fatal("right did not expand the selected root")
	}
// §foot page/pkg/tui/tui_test.go TestLeftFoldsSelectedSubtree