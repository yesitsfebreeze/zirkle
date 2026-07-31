// §head page/pkg/tui/multinput_test.go:121-131 TestCtrlJCreatesNewline
// §sig func TestCtrlJCreatesNewline(t *testing.T)
	mi := NewMultiInput(60)
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune("a")})
	mi, _, evt := mi.Update(tea.KeyMsg{Type: tea.KeyCtrlJ})
	if evt != BoundaryNone {
		t.Fatalf("ctrl+j = %v, want BoundaryNone", evt)
	}
	if mi.LineCount() != 2 {
		t.Fatalf("after ctrl+j, line count = %d, want 2", mi.LineCount())
	}
// §foot page/pkg/tui/multinput_test.go TestCtrlJCreatesNewline