// §head page/pkg/comp/dispatch.go:13-28 hostTags
// §sig func hostTags() map[string]bool
	tags := make(map[string]bool)
	switch runtime.GOOS {
	case "darwin":
		tags["macos"] = true
		tags["unix"] = true
	case "linux":
		tags["unix"] = true
		if isWSL() {
			tags["wsl"] = true
		}
	case "windows":
		tags["windows"] = true
	}
	return tags
// §foot page/pkg/comp/dispatch.go hostTags