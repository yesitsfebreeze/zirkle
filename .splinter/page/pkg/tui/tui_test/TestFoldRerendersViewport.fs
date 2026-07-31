// §head page/pkg/tui/tui_test.go:92-104 TestFoldRerendersViewport
// §sig func TestFoldRerendersViewport(t *testing.T)
	m := New(mockSource{views: testViews()}, nil, nil)
	got, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	got, _ = got.(Model).Update(refreshMsg(testViews()))
	mm := got.(Model)
	mm.cursor = 1 // alpha (root with child)
	before := mm.vp.View()

	got, _ = mm.Update(tea.KeyMsg{Type: tea.KeyLeft})
	if got.(Model).vp.View() == before {
		t.Fatal("viewport content unchanged after collapsing a subtree")
	}
// §foot page/pkg/tui/tui_test.go TestFoldRerendersViewport