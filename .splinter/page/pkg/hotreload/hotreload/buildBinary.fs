// §head page/pkg/hotreload/hotreload.go:273-280 buildBinary
// §sig func buildBinary(goBin, outputPath string) error
	cmd := exec.Command(goBin, "build", "-o", outputPath, "./cmd/relay")
	out, err := cmd.CombinedOutput()
	if err != nil {
		return fmt.Errorf("%s\n%s", err, string(out))
	}
	return nil
// §foot page/pkg/hotreload/hotreload.go buildBinary