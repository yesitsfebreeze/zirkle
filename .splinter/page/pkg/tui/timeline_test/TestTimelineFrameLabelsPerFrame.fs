// §head page/pkg/tui/timeline_test.go:52-64 TestTimelineFrameLabelsPerFrame
// §sig func TestTimelineFrameLabelsPerFrame(t *testing.T)
	now := tlNow()
	at := time.Date(2026, 7, 28, 15, 30, 0, 0, time.UTC)
	for frame, want := range map[string]string{
		"week":  "week of 27 Jul",
		"month": "Jul 2026",
	} {
		c := TimelineConfig{Enabled: true, Frame: frame, DayStart: "00:00"}
		if got := c.frameLabel(c.frameStart(at), now); got != want {
			t.Errorf("frame %s: got %q, want %q", frame, got, want)
		}
	}
// §foot page/pkg/tui/timeline_test.go TestTimelineFrameLabelsPerFrame