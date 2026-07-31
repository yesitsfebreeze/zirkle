// §head page/pkg/comp/dispatch.go:102-118 firstRecipeName
// §sig func firstRecipeName(justfile string) string
	for _, line := range strings.Split(justfile, "\n") {
		trimmed := strings.TrimSpace(line)
		if trimmed == "" || strings.HasPrefix(trimmed, "#") {
			continue
		}
		if !strings.HasPrefix(line, " ") && !strings.HasPrefix(line, "\t") {
			if idx := strings.IndexByte(trimmed, ':'); idx > 0 {
				name := trimmed[:idx]
				if !strings.Contains(name, " ") {
					return name
				}
			}
		}
	}
	return ""
// §foot page/pkg/comp/dispatch.go firstRecipeName