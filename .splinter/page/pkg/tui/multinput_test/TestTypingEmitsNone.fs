// §head page/pkg/tui/multinput_test.go:112-118 TestTypingEmitsNone
// §sig func TestTypingEmitsNone(t *testing.T)
	mi := NewMultiInput(60)
	_, _, evt := mi.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune("a")})
	if evt != BoundaryNone {
		t.Fatalf("typing 'a' = %v, want BoundaryNone", evt)
	}
// §foot page/pkg/tui/multinput_test.go TestTypingEmitsNone