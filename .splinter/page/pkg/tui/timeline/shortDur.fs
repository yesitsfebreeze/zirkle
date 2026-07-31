// §head page/pkg/tui/timeline.go:165-173 shortDur
// §sig func shortDur(d time.Duration) string
	if d < time.Minute {
		return fmt.Sprintf("%ds", int(d.Seconds()))
	}
	if d < time.Hour {
		return fmt.Sprintf("%dm", int(d.Minutes()))
	}
	return fmt.Sprintf("%dh%02dm", int(d.Hours()), int(d.Minutes())%60)
// §foot page/pkg/tui/timeline.go shortDur