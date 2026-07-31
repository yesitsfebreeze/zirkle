// §head page/pkg/config/config.go:235-243 expandTilde
// §sig func expandTilde(p, home string) string
	if p == "~" {
		return home
	}
	if len(p) >= 2 && p[0] == '~' && p[1] == '/' {
		return filepath.Join(home, p[2:])
	}
	return p
// §foot page/pkg/config/config.go expandTilde