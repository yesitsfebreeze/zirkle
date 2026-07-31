// §head page/pkg/comp/dispatch.go:58-92 PlatformStrip
// §sig func PlatformStrip(justfile string) string
	host := hostTags()
	lines := strings.Split(justfile, "\n")
	var out []string
	skipRecipe := false
	for _, line := range lines {
		trimmed := strings.TrimSpace(line)
		if strings.HasPrefix(trimmed, "[") && strings.HasSuffix(trimmed, "]") {
			tag := trimmed[1 : len(trimmed)-1]
			if platformTags[tag] {
				skipRecipe = !host[tag]
				continue
			}
			if skipRecipe {
				skipRecipe = false
			}
			out = append(out, line)
			continue
		}
		if skipRecipe {
			if trimmed == "" {
				continue
			}
			if strings.HasPrefix(line, " ") || strings.HasPrefix(line, "\t") {
				continue
			}
			if strings.HasSuffix(trimmed, ":") {
				continue
			}
			skipRecipe = false
		}
		out = append(out, line)
	}
	return strings.Join(out, "\n")
// §foot page/pkg/comp/dispatch.go PlatformStrip