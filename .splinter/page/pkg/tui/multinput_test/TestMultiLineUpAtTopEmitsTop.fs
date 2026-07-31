// §head page/pkg/tui/multinput_test.go:52-67 TestMultiLineUpAtTopEmitsTop
// §sig func TestMultiLineUpAtTopEmitsTop(t *testing.T)
	mi := NewMultiInput(60)
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune("a")})
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyCtrlJ})
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune("b")})
	// Move cursor to line 0
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyUp})
	if mi.Line() != 0 {
		t.Fatalf("expected line 0, got %d", mi.Line())
	}
	// Up at line 0 → BoundaryTop
	_, _, evt := mi.Update(tea.KeyMsg{Type: tea.KeyUp})
	if evt != BoundaryTop {
		t.Fatalf("up at line 0 = %v, want BoundaryTop", evt)
	}
// §foot page/pkg/tui/multinput_test.go TestMultiLineUpAtTopEmitsTop