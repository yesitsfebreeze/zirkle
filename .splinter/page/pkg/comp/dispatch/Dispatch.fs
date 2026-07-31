// §head page/pkg/comp/dispatch.go:120-157 Dispatch
// §sig func Dispatch(shard *Shard, vars map[string]string, args []string) (string, int, error)
	jf := PlatformStrip(shard.Justfile)
	if len(vars) > 0 {
		jf = Render(jf, vars)
	}
	tmp, err := os.CreateTemp("", "relay-shard-*.just")
	if err != nil {
		return "", 0, err
	}
	defer os.Remove(tmp.Name())
	if _, err := io.WriteString(tmp, jf); err != nil {
		return "", 0, err
	}
	tmp.Close()
	recipe := shard.Run
	if recipe == "" {
		recipe = firstRecipeName(jf)
	}
	if recipe == "" {
		return "", 0, nil
	}
	cmdArgs := []string{"--justfile", tmp.Name(), recipe}
	cmdArgs = append(cmdArgs, args...)
	cmd := exec.Command("just", cmdArgs...)
	var stdout, stderr bytes.Buffer
	cmd.Stdout = &stdout
	cmd.Stderr = &stderr
	err = cmd.Run()
	exitCode := 0
	if err != nil {
		if exitErr, ok := err.(*exec.ExitError); ok {
			exitCode = exitErr.ExitCode()
		} else {
			return "", 0, err
		}
	}
	return stdout.String() + stderr.String(), exitCode, nil
// §foot page/pkg/comp/dispatch.go Dispatch