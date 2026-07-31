// §head page/pkg/tui/multinput_test.go:134-143 TestValueMultiLine
// §sig func TestValueMultiLine(t *testing.T)
	mi := NewMultiInput(60)
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune("hello")})
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyCtrlJ})
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune("world")})
	val := mi.Value()
	if !strings.Contains(val, "hello") || !strings.Contains(val, "world") {
		t.Fatalf("value = %q, want both lines", val)
	}
// §foot page/pkg/tui/multinput_test.go TestValueMultiLine