// §head page/pkg/tui/timeline_test.go:13-26 TestTimelineDayStartOffsetMovesRollover
// §sig func TestTimelineDayStartOffsetMovesRollover(t *testing.T)
	c := TimelineConfig{Enabled: true, Frame: "day", DayStart: "04:00"}
	late := time.Date(2026, 7, 30, 2, 30, 0, 0, time.UTC)
	early := time.Date(2026, 7, 30, 5, 0, 0, 0, time.UTC)
	if got := c.frameStart(late); !got.Equal(time.Date(2026, 7, 29, 4, 0, 0, 0, time.UTC)) {
		t.Errorf("02:30 with day_start 04:00: got %v, want 2026-07-29 04:00", got)
	}
	if got := c.frameStart(early); !got.Equal(time.Date(2026, 7, 30, 4, 0, 0, 0, time.UTC)) {
		t.Errorf("05:00 with day_start 04:00: got %v, want 2026-07-30 04:00", got)
	}
	if c.frameStart(late).Equal(c.frameStart(early)) {
		t.Error("frames must differ across the rollover")
	}
// §foot page/pkg/tui/timeline_test.go TestTimelineDayStartOffsetMovesRollover