// §head page/pkg/hotreload/hotreload.go:141-143 IsDevChild
// §sig func IsDevChild() bool
	return os.Getenv("RELAY_DEV_CHILD") == "1"
// §foot page/pkg/hotreload/hotreload.go IsDevChild