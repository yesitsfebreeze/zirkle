// §head page/pkg/tui/tui.go:558-572 imagePath
// §sig func imagePath(s string) bool
	clean := s
	switch {
	case strings.HasPrefix(s, "file://"):
		clean = strings.TrimPrefix(s, "file://")
	case strings.HasPrefix(s, "data:image/"):
		return true
	}
	ext := strings.ToLower(filepath.Ext(clean))
	switch ext {
	case ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".webp", ".tiff", ".tif", ".svg":
		return true
	}
	return false
// §foot page/pkg/tui/tui.go imagePath