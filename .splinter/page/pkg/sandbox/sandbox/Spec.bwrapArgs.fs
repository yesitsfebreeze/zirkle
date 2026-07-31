// §head page/pkg/sandbox/sandbox.go:191-288 Spec.bwrapArgs
// §sig func (s Spec) bwrapArgs(dir string) ([]string, error)
	size := s.SizeMB
	if size <= 0 {
		size = 256
	}
	bytes := strconv.Itoa(size * 1024 * 1024)

	host := s.Hostname
	if host == "" {
		host = "pod"
	}

	args := []string{
		"--unshare-user",
		"--unshare-ipc",
		"--unshare-uts",
		"--unshare-cgroup-try",
		"--hostname", host,
		"--die-with-parent",
		"--new-session",
		"--clearenv",
	}
	if !s.SharePID {
		args = append(args, "--unshare-pid")
	}
	if !s.Net {
		args = append(args, "--unshare-net")
	}

	// Synthesized mounts go first so an explicit bind always wins: a tool
	// living under /tmp must survive the scratch tmpfs, not disappear under
	// it.  bwrap applies operations in order, so order is the policy.
	args = append(args,
		"--proc", "/proc",
		"--dev", "/dev",
		"--size", bytes, "--tmpfs", "/tmp",
	)

	tools := s.Tools
	if tools == nil {
		tools = DefaultTools
	}
	for _, t := range tools {
		if _, err := os.Stat(t); err != nil {
			continue // a tool the host does not have is not an error
		}
		args = append(args, "--ro-bind", t, t)
	}
	// Merged-usr hosts reach /usr through these; without them nothing on
	// PATH resolves and the sandbox looks broken rather than confined.
	for _, link := range [][2]string{
		{"usr/bin", "/bin"},
		{"usr/sbin", "/sbin"},
		{"usr/lib", "/lib"},
		{"usr/lib64", "/lib64"},
	} {
		args = append(args, "--symlink", link[0], link[1])
	}

	if s.Net {
		args = append(args, "--ro-bind-try", "/etc/resolv.conf", "/etc/resolv.conf")
	}

	if s.Ephemeral {
		args = append(args, "--size", bytes, "--tmpfs", Root)
	} else {
		args = append(args, "--bind", dir, Root)
	}

	for _, p := range s.RW {
		abs, err := filepath.Abs(p)
		if err != nil {
			return nil, fmt.Errorf("sandbox: rw path %q: %w", p, err)
		}
		args = append(args, "--bind", abs, abs)
	}

	// Everything bwrap synthesised to hold the mounts above — the root
	// tmpfs, the /etc it created to hang /etc/passwd off — is writable until
	// this line.  Sealing it last leaves exactly the writable set the spec
	// names: Root, /tmp, /dev, /proc and any RW hole.  Must stay last: it
	// only covers mounts that already exist.
	args = append(args, "--remount-ro", "/")

	args = append(args, "--chdir", Root)
	env := s.Env
	if env == nil {
		env = DefaultEnv
	}
	for _, kv := range env {
		key, value, found := strings.Cut(kv, "=")
		if !found {
			continue
		}
		args = append(args, "--setenv", key, value)
	}
	return args, nil
// §foot page/pkg/sandbox/sandbox.go Spec.bwrapArgs