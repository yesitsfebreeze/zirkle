// §head page/pkg/agent/agent.go:38-61 extractRecap
// §sig func extractRecap(content string) (string, string)
	lines := strings.Split(content, "\n")
	for i := len(lines) - 1; i >= 0; i-- {
		if strings.HasPrefix(strings.TrimSpace(lines[i]), recapPrefix) {
			recap := strings.TrimSpace(lines[i])[len(recapPrefix):]
			recap = strings.TrimSpace(recap)
			// Remove the SUMMARY line from content
			rest := strings.Join(append(lines[:i], lines[i+1:]...), "\n")
			return recap, strings.TrimSpace(rest)
		}
	}
	// Fallback for multi-line content: use the first non-empty line as recap
	// and strip it from the output. Single-line content with no SUMMARY is
	// left intact — recap stays empty.
	if len(lines) > 1 {
		for i, line := range lines {
			if t := strings.TrimSpace(line); t != "" {
				rest := strings.Join(append(lines[:i], lines[i+1:]...), "\n")
				return t, strings.TrimSpace(rest)
			}
		}
	}
	return "", content
// §foot page/pkg/agent/agent.go extractRecap