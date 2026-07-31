// §head page/pkg/comp/dispatch.go:30-44 hostPlatform
// §sig func hostPlatform() string
	switch runtime.GOOS {
	case "darwin":
		return "macos"
	case "linux":
		if isWSL() {
			return "wsl"
		}
		return "unix"
	case "windows":
		return "windows"
	default:
		return "unix"
	}
// §foot page/pkg/comp/dispatch.go hostPlatform