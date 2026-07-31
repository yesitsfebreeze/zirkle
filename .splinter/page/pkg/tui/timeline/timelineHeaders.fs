// §head page/pkg/tui/timeline.go:179-226 timelineHeaders
// §sig func timelineHeaders(views []PodView, vis []int, c TimelineConfig, now time.Time) map[int]TimelineHeader
	if !c.Enabled || len(vis) == 0 {
		return nil
	}
	stats := map[time.Time]*TimelineHeader{}
	for _, v := range views {
		if v.CreatedAt.IsZero() || v.ID == "+ new" {
			continue
		}
		s := c.frameStart(v.CreatedAt)
		h := stats[s]
		if h == nil {
			h = &TimelineHeader{
				Start: s, Label: c.frameLabel(s, now), Symbols: map[string]int{},
				first: v.CreatedAt, last: v.CreatedAt,
			}
			stats[s] = h
		}
		h.Total++
		h.Symbols[symbolForState(v.State)]++
		if v.CreatedAt.Before(h.first) {
			h.first = v.CreatedAt
		}
		if v.CreatedAt.After(h.last) {
			h.last = v.CreatedAt
		}
		h.Span = h.last.Sub(h.first)
	}

	out := map[int]TimelineHeader{}
	var prev time.Time
	started := false
	for vi, idx := range vis {
		v := views[idx]
		if v.Depth != 0 || v.CreatedAt.IsZero() || v.ID == "+ new" {
			continue
		}
		s := c.frameStart(v.CreatedAt)
		if started && s.Equal(prev) {
			continue
		}
		if h := stats[s]; h != nil {
			out[vi] = *h
		}
		prev, started = s, true
	}
	return out
// §foot page/pkg/tui/timeline.go timelineHeaders