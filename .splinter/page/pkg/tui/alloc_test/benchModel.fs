// §head page/pkg/tui/alloc_test.go:19-34 benchModel
// §sig func benchModel(t testing.TB, n int) Model
	t.Helper()
	views := make([]PodView, n)
	for i := range views {
		views[i] = PodView{
			ID:     "pod-abcdef",
			Prompt: "do the thing",
			State:  "running",
			Recap:  "working on it, about halfway",
		}
	}
	m := New(mockSource{views: views}, nil, nil)
	next, _ := m.Update(tea.WindowSizeMsg{Width: 100, Height: 40})
	next, _ = next.(Model).Update(refreshMsg(views))
	return next.(Model)
// §foot page/pkg/tui/alloc_test.go benchModel