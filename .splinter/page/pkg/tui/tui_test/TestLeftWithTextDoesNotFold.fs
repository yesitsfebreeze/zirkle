// §head page/pkg/tui/tui_test.go:107-119 TestLeftWithTextDoesNotFold
// §sig func TestLeftWithTextDoesNotFold(t *testing.T)
	m := New(mockSource{views: testViews()}, nil, nil)
	got, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	got, _ = got.(Model).Update(refreshMsg(testViews()))
	mm := got.(Model)
	mm.cursor = 0
	mm.input.SetValue("typing")

	got, _ = mm.Update(tea.KeyMsg{Type: tea.KeyLeft})
	if got.(Model).collapsed[0] {
		t.Fatal("left collapsed the tree while the input held text")
	}
// §foot page/pkg/tui/tui_test.go TestLeftWithTextDoesNotFold