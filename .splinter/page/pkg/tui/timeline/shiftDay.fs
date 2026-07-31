// §head page/pkg/tui/timeline.go:79-86 shiftDay
// §sig func shiftDay(t time.Time, off time.Duration) time.Time
	midnight := time.Date(t.Year(), t.Month(), t.Day(), 0, 0, 0, 0, t.Location())
	start := midnight.Add(off)
	if t.Before(start) {
		start = start.AddDate(0, 0, -1)
	}
	return start
// §foot page/pkg/tui/timeline.go shiftDay