// §head page/pkg/hotreload/hotreload.go:146-148 IsDevMode
// §sig func IsDevMode() bool
	return IsDevChild() || os.Getenv("RELAY_DEV") == "1"
// §foot page/pkg/hotreload/hotreload.go IsDevMode