// §head page/pkg/tui/fuzz_test.go:123-161 FuzzModelMessages
// §sig func FuzzModelMessages(f *testing.F)
	f.Add(80, 24, 3, 2)
	f.Add(0, 0, 0, 0)
	f.Add(1, 1, 3, 9)
	f.Add(200, 5, 1, 0)
	f.Add(-5, -5, 2, 1)

	f.Fuzz(func(t *testing.T, w, h, n, cursor int) {
		if n < 0 {
			n = 0
		}
		if n > 32 {
			n = 32
		}

		views := make([]PodView, n)
		for i := range views {
			views[i] = PodView{ID: "pod", State: "running"}
		}

		m := New(mockSource{views: views}, nil, nil)
		next, _ := m.Update(tea.WindowSizeMsg{Width: w, Height: h})
		mm := next.(Model)
		next, _ = mm.Update(refreshMsg(views))
		mm = next.(Model)

		if cursor >= 0 {
			mm.cursor = cursor
		}

		// Shrink the list under a cursor that was valid a moment ago.
		next, _ = mm.Update(refreshMsg(nil))
		mm = next.(Model)
		checkInvariants(t, mm)

		next, _ = mm.Update(tea.KeyMsg{Type: tea.KeyLeft})
		checkInvariants(t, next.(Model))
	})
// §foot page/pkg/tui/fuzz_test.go FuzzModelMessages