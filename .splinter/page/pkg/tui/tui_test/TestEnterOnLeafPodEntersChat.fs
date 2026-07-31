// §head page/pkg/tui/tui_test.go:515-540 TestEnterOnLeafPodEntersChat
// §sig func TestEnterOnLeafPodEntersChat(t *testing.T)
	views := []PodView{
		{ID: "pod-leaf", HasChildren: false},
	}
	m := New(mockSource{views: views}, nil, nil)
	next, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	mm := next.(Model)
	next, _ = mm.Update(refreshMsg(views))
	mm = next.(Model)

	// Move to pod list pane
	mm.pane = 0
	mm.cursor = 0 // pod-leaf
	mm.input.Reset()

	// Press Enter
	next, _ = mm.Update(tea.KeyMsg{Type: tea.KeyEnter})
	mm = next.(Model)

	if mm.detail != "pod-leaf" {
		t.Errorf("expected detail=%q for leaf pod, got %q", "pod-leaf", mm.detail)
	}
	if mm.pane != 1 {
		t.Errorf("expected pane=1 (input focus) after entering chat, got %d", mm.pane)
	}
// §foot page/pkg/tui/tui_test.go TestEnterOnLeafPodEntersChat