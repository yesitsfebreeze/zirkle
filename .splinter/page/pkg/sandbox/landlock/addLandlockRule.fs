// §head page/pkg/sandbox/landlock.go:108-128 addLandlockRule
// §sig func addLandlockRule(rulesetFD int, path string, access uint64) error
	fd, err := unix.Open(path, unix.O_PATH|unix.O_CLOEXEC, 0)
	if err != nil {
		return err
	}
	defer unix.Close(fd)

	attr := landlockPathBeneathAttr{
		AllowedAccess: access,
		ParentFD:      int32(fd),
	}
	_, _, errno := unix.Syscall6(unix.SYS_LANDLOCK_ADD_RULE,
		uintptr(rulesetFD),
		unix.LANDLOCK_RULE_PATH_BENEATH,
		uintptr(unsafe.Pointer(&attr)),
		0, 0, 0)
	if errno != 0 {
		return fmt.Errorf("add_rule: %w", errno)
	}
	return nil
// §foot page/pkg/sandbox/landlock.go addLandlockRule