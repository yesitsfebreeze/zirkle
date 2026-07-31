// §head page/pkg/tui/timeline_test.go:150-176 TestTimelineHeadersRenderWithoutMovingSelection
// §sig func TestTimelineHeadersRenderWithoutMovingSelection(t *testing.T)
	now := time.Now()
	views := []PodView{
		{ID: "p2", Prompt: "today job", State: "done", CreatedAt: now},
		{ID: "p1", Prompt: "old job", State: "done", CreatedAt: now.AddDate(0, 0, -1)},
	}
	m := New(mockSource{views: views}, nil, nil)
	m.views = views
	m.vp.Width = 80
	m.cursor = 1

	out := m.renderTree()
	if !strings.Contains(out, "today") || !strings.Contains(out, "yesterday") {
		t.Errorf("rendered tree missing frame headers:\n%s", out)
	}
	if id := m.selectedID(); id != "p1" {
		t.Errorf("cursor 1 must still select p1, got %q", id)
	}
	if n := len(m.visible()); n != 2 {
		t.Errorf("headers must not enter the selection space: %d visible rows", n)
	}

	m.tl = TimelineConfig{}
	if off := m.renderTree(); strings.Contains(off, "yesterday") {
		t.Errorf("disabled timeline still rendered a header:\n%s", off)
	}
// §foot page/pkg/tui/timeline_test.go TestTimelineHeadersRenderWithoutMovingSelection