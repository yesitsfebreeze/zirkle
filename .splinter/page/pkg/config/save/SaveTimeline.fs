// §head page/pkg/config/save.go:15-70 SaveTimeline
// §sig func SaveTimeline(path string, tl TimelineConfig) error
	if path == "" {
		return fmt.Errorf("config: no path to save timeline to")
	}
	if _, err := os.Stat(path); os.IsNotExist(err) {
		if _, err := EnsureDefault(); err != nil {
			return err
		}
	}
	raw, err := os.ReadFile(path)
	if err != nil {
		return err
	}

	block := []string{
		"[timeline]",
		fmt.Sprintf("enabled = %t", tl.Enabled),
		fmt.Sprintf("frame = %q", tl.Frame),
		fmt.Sprintf("day_start = %q", tl.DayStart),
		fmt.Sprintf("show_count = %t", tl.ShowCount),
		fmt.Sprintf("show_states = %t", tl.ShowStates),
		fmt.Sprintf("show_span = %t", tl.ShowSpan),
	}

	lines := strings.Split(string(raw), "\n")
	start, end := -1, len(lines)
	for i, l := range lines {
		t := strings.TrimSpace(l)
		if start == -1 {
			if t == "[timeline]" {
				start = i
			}
			continue
		}
		if strings.HasPrefix(t, "[") && strings.HasSuffix(t, "]") {
			end = i
			break
		}
	}

	var out []string
	if start == -1 {
		out = append(out, lines...)
		if len(out) > 0 && strings.TrimSpace(out[len(out)-1]) != "" {
			out = append(out, "")
		}
		out = append(out, block...)
		out = append(out, "")
	} else {
		out = append(out, lines[:start]...)
		out = append(out, block...)
		out = append(out, lines[end:]...)
	}

	return os.WriteFile(path, []byte(strings.Join(out, "\n")), 0o644)
// §foot page/pkg/config/save.go SaveTimeline