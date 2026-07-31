// §head page/pkg/tui/multinput_test.go:146-157 TestResetClearsContent
// §sig func TestResetClearsContent(t *testing.T)
	mi := NewMultiInput(60)
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune("text")})
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyCtrlJ})
	mi.Reset()
	if mi.Value() != "" {
		t.Fatalf("after reset, value = %q, want empty", mi.Value())
	}
	if mi.LineCount() != 1 {
		t.Fatalf("after reset, line count = %d, want 1", mi.LineCount())
	}
// §foot page/pkg/tui/multinput_test.go TestResetClearsContent