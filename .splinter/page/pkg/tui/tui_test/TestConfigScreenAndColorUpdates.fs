// §head page/pkg/tui/tui_test.go:414-471 TestConfigScreenAndColorUpdates
// §sig func TestConfigScreenAndColorUpdates(t *testing.T)
	m := New(mockSource{views: testViews()}, nil, nil)
	next, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	mm := next.(Model)

	// Check default colors
	if mm.highlightColor != ansiMagenta {
		t.Errorf("default highlightColor=%q, want %s", mm.highlightColor, ansiMagenta)
	}
	if mm.attentionColor != ansiBlue {
		t.Errorf("default attentionColor=%q, want %s", mm.attentionColor, ansiBlue)
	}

	// Submit config in command mode opens the config screen. The prefix is a
	// prompt, so the buffer holds the command name alone.
	mm.mode = modeCommand
	mm.input.SetValue("config")
	next, _ = mm.Update(tea.KeyMsg{Type: tea.KeyEnter})
	mm = next.(Model)

	if !mm.config {
		t.Fatal("expected config screen to be open")
	}

	// Pressing down moves cursor in config
	next, _ = mm.Update(tea.KeyMsg{Type: tea.KeyDown})
	mm = next.(Model)
	if mm.configCur != 1 {
		t.Errorf("configCur=%d after down, want 1", mm.configCur)
	}

	// Pressing enter cycles attention color
	oldAtt := mm.attentionColor
	next, _ = mm.Update(tea.KeyMsg{Type: tea.KeyEnter})
	mm = next.(Model)
	if mm.attentionColor == oldAtt {
		t.Errorf("expected attentionColor to change from %s", oldAtt)
	}

	// Esc exits config screen
	next, _ = mm.Update(tea.KeyMsg{Type: tea.KeyEscape})
	mm = next.(Model)
	if mm.config {
		t.Fatal("expected config screen to close after esc")
	}

	// Passing color args updates colors directly
	mm.input.SetValue("config #00E5FF #FF1744")
	next, _ = mm.Update(tea.KeyMsg{Type: tea.KeyEnter})
	mm = next.(Model)

	if mm.highlightColor != "#00E5FF" {
		t.Errorf("highlightColor=%q, want #00E5FF", mm.highlightColor)
	}
	if mm.attentionColor != "#FF1744" {
		t.Errorf("attentionColor=%q, want #FF1744", mm.attentionColor)
	}
// §foot page/pkg/tui/tui_test.go TestConfigScreenAndColorUpdates