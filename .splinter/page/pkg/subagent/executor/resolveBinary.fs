// §head page/pkg/subagent/executor.go:330-385 resolveBinary
// §sig func resolveBinary(want string) (string, func(), error)
	noop := func() {}

	binary := want
	if binary == "" {
		exe, err := os.Executable()
		if err != nil {
			return "", noop, err
		}
		binary = exe
	}
	if abs, err := filepath.Abs(binary); err == nil {
		binary = abs
	}
	if resolved, err := filepath.EvalSymlinks(binary); err == nil {
		binary = resolved
	}

	// A binary still present on disk is bind-mounted as-is — no copy —
	// EXCEPT under dev hot-reload: the supervisor rebuilds in place, so the
	// path can be unlinked between this Stat and bwrap's exec.  Copying the
	// live inode removes the race entirely.
	if fi, err := os.Stat(binary); err == nil && !fi.IsDir() {
		if !hotreload.IsDevMode() {
			return binary, noop, nil
		}
	}

	// Path is gone (dev rebuild unlinked it). Copy the live inode out of
	// /proc/self/exe into a fresh file the sandbox can mount.
	src, err := os.Open("/proc/self/exe")
	if err != nil {
		return "", noop, fmt.Errorf("binary %q missing and /proc/self/exe unavailable: %w", binary, err)
	}
	defer src.Close()

	dst, err := os.CreateTemp("", "relay-bin-*")
	if err != nil {
		return "", noop, err
	}
	cleanup := func() { os.Remove(dst.Name()) }
	if _, err := io.Copy(dst, src); err != nil {
		dst.Close()
		cleanup()
		return "", noop, err
	}
	if err := dst.Close(); err != nil {
		cleanup()
		return "", noop, err
	}
	if err := os.Chmod(dst.Name(), 0o755); err != nil {
		cleanup()
		return "", noop, err
	}
	return dst.Name(), cleanup, nil
// §foot page/pkg/subagent/executor.go resolveBinary