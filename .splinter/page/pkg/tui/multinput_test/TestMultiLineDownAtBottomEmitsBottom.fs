// §head page/pkg/tui/multinput_test.go:70-83 TestMultiLineDownAtBottomEmitsBottom
// §sig func TestMultiLineDownAtBottomEmitsBottom(t *testing.T)
	mi := NewMultiInput(60)
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune("a")})
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyCtrlJ})
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune("b")})
	// Already on last line (line 1)
	if mi.Line() != 1 {
		t.Fatalf("expected line 1, got %d", mi.Line())
	}
	_, _, evt := mi.Update(tea.KeyMsg{Type: tea.KeyDown})
	if evt != BoundaryBottom {
		t.Fatalf("down at last line = %v, want BoundaryBottom", evt)
	}
// §foot page/pkg/tui/multinput_test.go TestMultiLineDownAtBottomEmitsBottom