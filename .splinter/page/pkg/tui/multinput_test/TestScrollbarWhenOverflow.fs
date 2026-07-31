// §head page/pkg/tui/multinput_test.go:86-98 TestScrollbarWhenOverflow
// §sig func TestScrollbarWhenOverflow(t *testing.T)
	mi := NewMultiInput(60)
	// Create 5 lines, set height to 2
	for i := 0; i < 5; i++ {
		mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune("x")})
		mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyCtrlJ})
	}
	mi.SetHeight(2)
	view := mi.View()
	if !strings.Contains(view, "█") && !strings.Contains(view, "░") {
		t.Fatal("scrollbar chars not found in overflow view")
	}
// §foot page/pkg/tui/multinput_test.go TestScrollbarWhenOverflow