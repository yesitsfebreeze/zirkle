// §head page/pkg/hotreload/hotreload.go:261-271 findGoBinary
// §sig func findGoBinary() string
	if p, err := exec.LookPath("go"); err == nil {
		return p
	}
	home, _ := os.UserHomeDir()
	sdkGo := filepath.Join(home, "sdk", "go", "bin", "go")
	if _, err := os.Stat(sdkGo); err == nil {
		return sdkGo
	}
	return "go"
// §foot page/pkg/hotreload/hotreload.go findGoBinary