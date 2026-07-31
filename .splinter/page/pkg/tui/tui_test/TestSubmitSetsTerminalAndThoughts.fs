// §head page/pkg/tui/tui_test.go:673-706 TestSubmitSetsTerminalAndThoughts
// §sig func TestSubmitSetsTerminalAndThoughts(t *testing.T)
	cmdr := &recordingCommander{}
	m := New(mockSource{views: testViews()}, cmdr, nil)
	next, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	mm := next.(Model)
	mm.input.SetValue("test prompt")

	next, cmd := mm.Update(tea.KeyMsg{Type: tea.KeyEnter})
	mm = next.(Model)

	if !mm.busy {
		t.Fatal("busy = false")
	}
	if !mm.streaming {
		t.Fatal("streaming = false")
	}
	// The prompt lives in the chat log (left pane), not the terminal (right).
	if len(mm.chat) == 0 || mm.chat[len(mm.chat)-1].Role != "user" ||
		!strings.Contains(mm.chat[len(mm.chat)-1].Content, "test prompt") {
		t.Fatalf("chat = %#v, want user 'test prompt'", mm.chat)
	}
	// Terminal pane holds subpod/shell output only — no prompt echo.
	for _, line := range mm.terminal {
		if strings.Contains(line, "test prompt") {
			t.Fatalf("terminal echoed prompt: %q", line)
		}
	}
	// View must still contain the prompt (via chat pane).
	v := mm.View()
	if !strings.Contains(v, "test prompt") {
		t.Fatalf("View does not contain 'test prompt'")
	}
	_ = cmd
// §foot page/pkg/tui/tui_test.go TestSubmitSetsTerminalAndThoughts