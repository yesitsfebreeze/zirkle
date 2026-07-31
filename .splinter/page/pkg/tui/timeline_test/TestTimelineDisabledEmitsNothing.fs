// §head page/pkg/tui/timeline_test.go:96-102 TestTimelineDisabledEmitsNothing
// §sig func TestTimelineDisabledEmitsNothing(t *testing.T)
	now := tlNow()
	views := []PodView{{ID: "p1", State: "done", CreatedAt: now}}
	if got := timelineHeaders(views, []int{0}, TimelineConfig{}, now); got != nil {
		t.Errorf("disabled timeline must return nil, got %v", got)
	}
// §foot page/pkg/tui/timeline_test.go TestTimelineDisabledEmitsNothing