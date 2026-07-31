// §head page/pkg/tui/tui_test.go:473-498 TestTickRefreshAndDetailPersistence
// §sig func TestTickRefreshAndDetailPersistence(t *testing.T)
	m := New(mockSource{views: testViews()}, nil, nil)
	next, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	mm := next.(Model)

	// Set detail view open
	mm.detail = "alpha"

	// Trigger tickMsg
	next, cmd := mm.Update(tickMsg(time.Now()))
	mm = next.(Model)

	if cmd == nil {
		t.Fatal("expected tickMsg to return batch cmd including load()")
	}

	// Dispatch refreshMsg
	newViews := testViews()
	next, _ = mm.Update(refreshMsg(newViews))
	mm = next.(Model)

	// Detail view must be preserved by id across refresh, not reset
	if mm.detail != "alpha" {
		t.Errorf("expected detail to stay %q after refreshMsg, got %q", "alpha", mm.detail)
	}
// §foot page/pkg/tui/tui_test.go TestTickRefreshAndDetailPersistence