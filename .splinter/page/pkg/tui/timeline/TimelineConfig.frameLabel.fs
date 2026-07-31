// §head page/pkg/tui/timeline.go:90-117 TimelineConfig.frameLabel
// §sig func (c TimelineConfig) frameLabel(start, now time.Time) string
	switch c.Frame {
	case "hour":
		day := ""
		switch c.frameStart(start).Format("2006-01-02") {
		case c.frameStart(now).Format("2006-01-02"):
			day = " today"
		case c.frameStart(now.AddDate(0, 0, -1)).Format("2006-01-02"):
			day = " yesterday"
		}
		if day == "" {
			return start.Format("02 Jan 15:04")
		}
		return start.Format("15:04") + day
	case "week":
		return "week of " + start.Format("02 Jan")
	case "month":
		return start.Format("Jan 2006")
	default:
		switch start.Format("2006-01-02") {
		case c.frameStart(now).Format("2006-01-02"):
			return "today"
		case c.frameStart(now.AddDate(0, 0, -1)).Format("2006-01-02"):
			return "yesterday"
		}
		return start.Format("Mon 02 Jan")
	}
// §foot page/pkg/tui/timeline.go TimelineConfig.frameLabel