// §head page/pkg/comp/dispatch.go:46-52 isWSL
// §sig func isWSL() bool
	b, err := os.ReadFile("/proc/sys/kernel/osrelease")
	if err != nil {
		return false
	}
	return strings.Contains(strings.ToLower(string(b)), "microsoft")
// §foot page/pkg/comp/dispatch.go isWSL