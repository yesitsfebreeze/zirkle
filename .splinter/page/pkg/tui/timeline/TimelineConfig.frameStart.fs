// §head page/pkg/tui/timeline.go:61-76 TimelineConfig.frameStart
// §sig func (c TimelineConfig) frameStart(t time.Time) time.Time
	off := time.Duration(c.dayStartMinutes()) * time.Minute
	switch c.Frame {
	case "hour":
		return t.Truncate(time.Hour).In(t.Location())
	case "week":
		d := shiftDay(t, off)
		wd := (int(d.Weekday()) + 6) % 7 // Monday = 0
		return d.AddDate(0, 0, -wd)
	case "month":
		d := shiftDay(t, off)
		return time.Date(d.Year(), d.Month(), 1, 0, 0, 0, 0, d.Location()).Add(off)
	default:
		return shiftDay(t, off)
	}
// §foot page/pkg/tui/timeline.go TimelineConfig.frameStart