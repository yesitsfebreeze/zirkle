// §head page/pkg/tui/timeline.go:42-56 TimelineConfig.dayStartMinutes
// §sig func (c TimelineConfig) dayStartMinutes() int
	h, m, ok := strings.Cut(c.DayStart, ":")
	if !ok {
		return 0
	}
	hh, err := strconv.Atoi(strings.TrimSpace(h))
	if err != nil || hh < 0 || hh > 23 {
		return 0
	}
	mm, err := strconv.Atoi(strings.TrimSpace(m))
	if err != nil || mm < 0 || mm > 59 {
		return 0
	}
	return hh*60 + mm
// §foot page/pkg/tui/timeline.go TimelineConfig.dayStartMinutes