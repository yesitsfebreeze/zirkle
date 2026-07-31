// §head page/pkg/sandbox/landlock.go:41-106 ApplyLandlock
// §sig func ApplyLandlock(roPaths, rwPaths []string) error
	abi := LandlockABI()
	if abi == 0 {
		return nil // not fatal: bwrap is the boundary
	}

	accessFS := uint64(unix.LANDLOCK_ACCESS_FS_EXECUTE |
		unix.LANDLOCK_ACCESS_FS_WRITE_FILE |
		unix.LANDLOCK_ACCESS_FS_READ_FILE |
		unix.LANDLOCK_ACCESS_FS_READ_DIR |
		unix.LANDLOCK_ACCESS_FS_REMOVE_DIR |
		unix.LANDLOCK_ACCESS_FS_REMOVE_FILE |
		unix.LANDLOCK_ACCESS_FS_MAKE_REG |
		unix.LANDLOCK_ACCESS_FS_MAKE_DIR |
		unix.LANDLOCK_ACCESS_FS_MAKE_CHAR |
		unix.LANDLOCK_ACCESS_FS_MAKE_BLOCK |
		unix.LANDLOCK_ACCESS_FS_MAKE_FIFO |
		unix.LANDLOCK_ACCESS_FS_MAKE_SOCK |
		unix.LANDLOCK_ACCESS_FS_MAKE_SYM)
	if abi >= 2 {
		accessFS |= uint64(unix.LANDLOCK_ACCESS_FS_TRUNCATE)
	}
	if abi >= 3 {
		accessFS |= uint64(unix.LANDLOCK_ACCESS_FS_REFER)
	}

	attr := landlockRulesetAttr{HandledAccessFS: accessFS}
	fd, _, errno := unix.Syscall(unix.SYS_LANDLOCK_CREATE_RULESET,
		uintptr(unsafe.Pointer(&attr)),
		unsafe.Sizeof(attr),
		0)
	if errno != 0 {
		return fmt.Errorf("landlock: create_ruleset: %w", errno)
	}
	rulesetFD := int(fd)
	defer unix.Close(rulesetFD)

	roAccess := uint64(unix.LANDLOCK_ACCESS_FS_EXECUTE |
		unix.LANDLOCK_ACCESS_FS_READ_FILE |
		unix.LANDLOCK_ACCESS_FS_READ_DIR)
	for _, p := range roPaths {
		if err := addLandlockRule(rulesetFD, p, roAccess); err != nil {
			return fmt.Errorf("landlock: ro rule %s: %w", p, err)
		}
	}

	for _, p := range rwPaths {
		if err := addLandlockRule(rulesetFD, p, accessFS); err != nil {
			return fmt.Errorf("landlock: rw rule %s: %w", p, err)
		}
	}

	// PR_SET_NO_NEW_PRIVS is required before restrict_self when the thread
	// lacks CAP_SYS_ADMIN.  It also prevents privilege escalation via setuid
	// binaries inside the sandbox.
	if _, _, errno := unix.Syscall(unix.SYS_PRCTL, unix.PR_SET_NO_NEW_PRIVS, 1, 0); errno != 0 {
		return fmt.Errorf("landlock: prctl PR_SET_NO_NEW_PRIVS: %w", errno)
	}

	_, _, errno = unix.Syscall(unix.SYS_LANDLOCK_RESTRICT_SELF,
		uintptr(rulesetFD), 0, 0)
	if errno != 0 {
		return fmt.Errorf("landlock: restrict_self: %w", errno)
	}
	return nil
// §foot page/pkg/sandbox/landlock.go ApplyLandlock