// §head page/pkg/tui/timeline_test.go:37-50 TestTimelineLabelsAreRelative
// §sig func TestTimelineLabelsAreRelative(t *testing.T)
	c := DefaultTimeline()
	now := tlNow()
	cases := map[string]time.Time{
		"today":      now,
		"yesterday":  now.AddDate(0, 0, -1),
		"Sat 25 Jul": time.Date(2026, 7, 25, 9, 0, 0, 0, time.UTC),
	}
	for want, at := range cases {
		if got := c.frameLabel(c.frameStart(at), now); got != want {
			t.Errorf("label for %v: got %q, want %q", at, got, want)
		}
	}
// §foot page/pkg/tui/timeline_test.go TestTimelineLabelsAreRelative