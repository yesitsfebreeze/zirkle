// §head page/pkg/sandbox/landlock.go:15-22 LandlockABI
// §sig func LandlockABI() int
	ret, _, errno := unix.Syscall(unix.SYS_LANDLOCK_CREATE_RULESET, 0, 0,
		unix.LANDLOCK_CREATE_RULESET_VERSION)
	if errno != 0 {
		return 0
	}
	return int(ret)
// §foot page/pkg/sandbox/landlock.go LandlockABI