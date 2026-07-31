// §head page/pkg/comp/store.go:176-189 splitCSV
// §sig func splitCSV(s string) []string
	if s == "" {
		return nil
	}
	parts := strings.Split(s, ",")
	out := make([]string, 0, len(parts))
	for _, p := range parts {
		p = strings.TrimSpace(p)
		if p != "" {
			out = append(out, p)
		}
	}
	return out
// §foot page/pkg/comp/store.go splitCSV