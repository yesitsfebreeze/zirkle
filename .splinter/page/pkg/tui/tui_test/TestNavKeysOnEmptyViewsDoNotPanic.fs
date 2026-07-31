// §head page/pkg/tui/tui_test.go:225-238 TestNavKeysOnEmptyViewsDoNotPanic
// §sig func TestNavKeysOnEmptyViewsDoNotPanic(t *testing.T)
	m := New(mockSource{views: nil}, nil, nil)
	got, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	mm := got.(Model)

	for _, key := range []string{"left", "right", "up", "down", "enter"} {
		next, _ := mm.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune(key)})
		_ = next
	}
	for _, kt := range []tea.KeyType{tea.KeyLeft, tea.KeyRight, tea.KeyUp, tea.KeyDown} {
		next, _ := mm.Update(tea.KeyMsg{Type: kt})
		mm = next.(Model)
	}
// §foot page/pkg/tui/tui_test.go TestNavKeysOnEmptyViewsDoNotPanic