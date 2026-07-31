// §head page/pkg/tui/settings_test.go:14-26 settingsModel
// §sig func settingsModel(t *testing.T) Model
	t.Helper()
	now := time.Now()
	views := []PodView{
		{ID: "p2", Prompt: "today job", State: "done", CreatedAt: now},
		{ID: "p1", Prompt: "old job", State: "failed", CreatedAt: now.AddDate(0, 0, -1)},
	}
	m := New(mockSource{views: views}, nil, nil)
	m.views = views
	m.vp.Width = 80
	m.config = true
	return m
// §foot page/pkg/tui/settings_test.go settingsModel