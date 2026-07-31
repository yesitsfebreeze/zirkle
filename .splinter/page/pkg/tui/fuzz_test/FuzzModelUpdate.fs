// §head page/pkg/tui/fuzz_test.go:86-119 FuzzModelUpdate
// §sig func FuzzModelUpdate(f *testing.F)
	// The exact reported crash: size, then press left.
	f.Add([]byte{0}, false)
	f.Add([]byte{0, 1}, false)
	f.Add([]byte{1, 0, 2, 3}, false)
	f.Add([]byte{4, 0, 0, 1, 1}, true)
	f.Add([]byte{6}, false)
	f.Add([]byte{}, false)

	f.Fuzz(func(t *testing.T, keys []byte, populated bool) {
		if len(keys) > 64 {
			keys = keys[:64]
		}

		var views []PodView
		if populated {
			views = testViews()
		}

		m := New(mockSource{views: views}, nil, nil)
		next, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
		mm := next.(Model)
		if populated {
			next, _ = mm.Update(refreshMsg(views))
			mm = next.(Model)
		}

		for _, b := range keys {
			next, _ := mm.Update(keyFor(b))
			mm = next.(Model)
			checkInvariants(t, mm)
		}
	})
// §foot page/pkg/tui/fuzz_test.go FuzzModelUpdate