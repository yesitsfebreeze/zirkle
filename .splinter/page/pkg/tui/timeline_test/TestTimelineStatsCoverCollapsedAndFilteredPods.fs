// §head page/pkg/tui/timeline_test.go:106-128 TestTimelineStatsCoverCollapsedAndFilteredPods
// §sig func TestTimelineStatsCoverCollapsedAndFilteredPods(t *testing.T)
	now := tlNow()
	views := []PodView{
		{ID: "p2", State: "running", CreatedAt: now, HasChildren: true},
		{ID: "p2.1", State: "done", CreatedAt: now.Add(-30 * time.Minute), Depth: 1},
		{ID: "p2.2", State: "failed", CreatedAt: now.Add(-90 * time.Minute), Depth: 1},
		{ID: "p1", State: "done", CreatedAt: now.Add(-3 * time.Hour)},
	}
	got := timelineHeaders(views, []int{0}, DefaultTimeline(), now) // children collapsed away
	h, ok := got[0]
	if !ok {
		t.Fatal("no header for the today frame")
	}
	if h.Total != 4 {
		t.Errorf("total: got %d, want 4 (all pods in frame, collapsed included)", h.Total)
	}
	if h.Symbols["■"] != 2 || h.Symbols["▶"] != 1 || h.Symbols["✕"] != 1 {
		t.Errorf("tallies wrong: %v", h.Symbols)
	}
	if h.Span != 3*time.Hour {
		t.Errorf("span: got %v, want 3h", h.Span)
	}
// §foot page/pkg/tui/timeline_test.go TestTimelineStatsCoverCollapsedAndFilteredPods