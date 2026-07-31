// §head page/pkg/tui/timeline_test.go:28-35 TestTimelineDayStartGarbageFallsBackToMidnight
// §sig func TestTimelineDayStartGarbageFallsBackToMidnight(t *testing.T)
	for _, bad := range []string{"", "nonsense", "99:99", "12", "-1:00"} {
		c := TimelineConfig{Enabled: true, Frame: "day", DayStart: bad}
		if got := c.dayStartMinutes(); got != 0 {
			t.Errorf("day_start %q: got %d minutes, want 0", bad, got)
		}
	}
// §foot page/pkg/tui/timeline_test.go TestTimelineDayStartGarbageFallsBackToMidnight