// §head page/pkg/tui/multinput_test.go:101-109 TestNoScrollbarWhenFits
// §sig func TestNoScrollbarWhenFits(t *testing.T)
	mi := NewMultiInput(60)
	mi, _, _ = mi.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune("hello")})
	mi.SetHeight(3)
	view := mi.View()
	if strings.Contains(view, "█") || strings.Contains(view, "░") {
		t.Fatal("scrollbar found when content fits")
	}
// §foot page/pkg/tui/multinput_test.go TestNoScrollbarWhenFits