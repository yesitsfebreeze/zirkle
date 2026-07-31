// §head page/pkg/tui/tui_test.go:186-203 TestViewportHeightLeavesNoGap
// §sig func TestViewportHeightLeavesNoGap(t *testing.T)
	m := New(mockSource{views: testViews()}, nil, nil)
	got, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	mm := got.(Model)
	// View() must fill the terminal exactly: top vp + pane-header(1) +
	// input(1) + status(1) + bottom vp.
	want := 24
	chrome := 2 // pane header + status bar
	if mm.vpChat.Height+mm.vp.Height+mm.input.Height()+chrome != want {
		t.Fatalf("chrome math: vpChat %d + vp %d + input %d + chrome %d = %d, want %d",
			mm.vpChat.Height, mm.vp.Height, mm.input.Height(), chrome,
			mm.vpChat.Height+mm.vp.Height+mm.input.Height()+chrome, want)
	}
	lines := strings.Count(mm.View(), "\n") + 1
	if lines != want {
		t.Fatalf("View rendered %d lines, want %d (gap below status bar)", lines, want)
	}
// §foot page/pkg/tui/tui_test.go TestViewportHeightLeavesNoGap