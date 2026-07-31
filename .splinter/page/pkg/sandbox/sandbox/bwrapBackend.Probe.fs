// §head page/pkg/sandbox/sandbox.go:127-158 bwrapBackend.Probe
// §sig func (bwrapBackend) Probe() error
	if _, err := exec.LookPath("bwrap"); err != nil {
		return fmt.Errorf("%w: bwrap not found — apt install bubblewrap", ErrUnavailable)
	}

	dir, err := os.MkdirTemp("", "relay-probe-")
	if err != nil {
		return fmt.Errorf("%w: %v", ErrUnavailable, err)
	}
	defer os.RemoveAll(dir)

	ctx, cancel := context.WithTimeout(context.Background(), 10*time.Second)
	defer cancel()

	cmd, err := Spec{Dir: dir, Ephemeral: true, SizeMB: 8}.Command(ctx, "/usr/bin/true")
	if err != nil {
		return fmt.Errorf("%w: %v", ErrUnavailable, err)
	}
	if out, err := cmd.CombinedOutput(); err != nil {
		msg := fmt.Sprintf("bwrap cannot start (%v): %s",
			err, strings.TrimSpace(string(out)))
		// Ubuntu 24.04+ restricts unprivileged user namespaces via AppArmor.
		// The restriction is invisible to bwrap (it just fails), so name the fix.
		if data, rerr := os.ReadFile("/proc/sys/kernel/apparmor_restrict_unprivileged_userns"); rerr == nil {
			if strings.TrimSpace(string(data)) == "1" {
				msg += "\n  hint: sysctl kernel.apparmor_restrict_unprivileged_userns=0"
			}
		}
		return fmt.Errorf("%w: %s", ErrUnavailable, msg)
	}
	return nil
// §foot page/pkg/sandbox/sandbox.go bwrapBackend.Probe