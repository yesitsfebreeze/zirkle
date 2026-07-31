// §head page/pkg/tui/timeline_test.go:68-94 TestTimelineHeadersOnlyAtFrameBoundaries
// §sig func TestTimelineHeadersOnlyAtFrameBoundaries(t *testing.T)
	now := tlNow()
	views := []PodView{
		{ID: "+ new", State: "ready"},
		{ID: "p3", State: "running", CreatedAt: now, Depth: 0, HasChildren: true},
		{ID: "p3.1", State: "done", CreatedAt: now.AddDate(0, 0, -1), Depth: 1},
		{ID: "p2", State: "done", CreatedAt: now.Add(-2 * time.Hour), Depth: 0},
		{ID: "p1", State: "failed", CreatedAt: now.AddDate(0, 0, -1), Depth: 0},
	}
	vis := []int{0, 1, 2, 3, 4}
	got := timelineHeaders(views, vis, DefaultTimeline(), now)
	if len(got) != 2 {
		t.Fatalf("want 2 headers (today, yesterday), got %d: %v", len(got), got)
	}
	if h, ok := got[1]; !ok || h.Label != "today" {
		t.Errorf("row 1 must open the today frame, got %+v", got[1])
	}
	if h, ok := got[4]; !ok || h.Label != "yesterday" {
		t.Errorf("row 4 must open the yesterday frame, got %+v", got[4])
	}
	if _, ok := got[2]; ok {
		t.Error("a depth-1 child must not open a frame")
	}
	if _, ok := got[0]; ok {
		t.Error("the + new sentinel must not carry a header")
	}
// §foot page/pkg/tui/timeline_test.go TestTimelineHeadersOnlyAtFrameBoundaries