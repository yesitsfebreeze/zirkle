// §head page/pkg/tui/tui.go:644-654 readLoad
// §sig func readLoad() string
	data, err := os.ReadFile("/proc/loadavg")
	if err != nil {
		return "?"
	}
	f := strings.Fields(string(data))
	if len(f) > 0 {
		return f[0]
	}
	return "?"
// §foot page/pkg/tui/tui.go readLoad