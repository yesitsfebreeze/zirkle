// §head page/pkg/tui/timeline_test.go:130-146 TestTimelineHeaderLineCarriesStats
// §sig func TestTimelineHeaderLineCarriesStats(t *testing.T)
	h := TimelineHeader{
		Label:   "today",
		Total:   12,
		Symbols: map[string]int{"■": 9, "▶": 1, "✕": 2},
		Span:    4*time.Hour + 12*time.Minute,
	}
	line := h.Line(80, DefaultTimeline())
	for _, want := range []string{"today", "12 pods", "9■", "1▶", "2✕", "span 4h12m", "──"} {
		if !strings.Contains(line, want) {
			t.Errorf("header line missing %q: %q", want, line)
		}
	}
	if strings.Contains(line, "0●") {
		t.Errorf("zero tally must be omitted: %q", line)
	}
// §foot page/pkg/tui/timeline_test.go TestTimelineHeaderLineCarriesStats