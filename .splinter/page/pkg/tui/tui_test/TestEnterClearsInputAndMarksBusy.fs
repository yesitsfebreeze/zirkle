// §head page/pkg/tui/tui_test.go:273-305 TestEnterClearsInputAndMarksBusy
// §sig func TestEnterClearsInputAndMarksBusy(t *testing.T)
	cmdr := &recordingCommander{}
	m := New(mockSource{views: testViews()}, cmdr, nil)
	next, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	mm := next.(Model)
	mm.input.SetValue("do the thing")

	// Enter submits the prompt.
	next, cmd := mm.Update(tea.KeyMsg{Type: tea.KeyEnter})
	mm = next.(Model)

	if got := mm.input.Value(); got != "" {
		t.Errorf("input = %q after Enter, want cleared", got)
	}
	if !mm.busy {
		t.Error("busy = false after Enter, want true")
	}
	if cmd == nil {
		t.Fatal("Enter returned no command, nothing was dispatched")
	}

	msg := cmd()
	done, ok := msg.(doneRun)
	if !ok {
		t.Fatalf("command returned %T, want doneRun", msg)
	}
	if done.prompt != "do the thing" {
		t.Errorf("dispatched prompt = %q, want %q", done.prompt, "do the thing")
	}
	if len(cmdr.prompts) != 1 || cmdr.prompts[0] != "do the thing" {
		t.Errorf("commander saw %v, want one dispatch of the prompt", cmdr.prompts)
	}
// §foot page/pkg/tui/tui_test.go TestEnterClearsInputAndMarksBusy