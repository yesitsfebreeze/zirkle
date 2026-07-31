// §head page/pkg/tui/tui_test.go:307-321 TestEnterOnEmptyInputDoesNothing
// §sig func TestEnterOnEmptyInputDoesNothing(t *testing.T)
	cmdr := &recordingCommander{}
	m := New(mockSource{views: nil}, cmdr, nil)
	next, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	mm := next.(Model)
	mm.input.SetValue("   ")

	next, cmd := mm.Update(tea.KeyMsg{Type: tea.KeyEnter})
	if cmd != nil {
		t.Error("whitespace-only input dispatched a command")
	}
	if next.(Model).busy {
		t.Error("busy set for an empty dispatch")
	}
// §foot page/pkg/tui/tui_test.go TestEnterOnEmptyInputDoesNothing