// §head page/pkg/tui/multinput_test.go:29-49 TestMultiLineUpMovesCursor
// §sig func TestMultiLineUpMovesCursor(t *testing.T)
	mi := NewMultiInput(60)
	// Type "line1" + ctrl+j + "line2"
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune("line1")})
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyCtrlJ})
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune("line2")})
	if mi.LineCount() != 2 {
		t.Fatalf("line count = %d, want 2", mi.LineCount())
	}
	if mi.Line() != 1 {
		t.Fatalf("cursor line = %d, want 1", mi.Line())
	}
	// Up should move to line 0, no boundary event
	mi, _, evt := mi.Update(tea.KeyMsg{Type: tea.KeyUp})
	if evt != BoundaryNone {
		t.Fatalf("up on line 1 = %v, want BoundaryNone", evt)
	}
	if mi.Line() != 0 {
		t.Fatalf("cursor line = %d, want 0 after up", mi.Line())
	}
// §foot page/pkg/tui/multinput_test.go TestMultiLineUpMovesCursor