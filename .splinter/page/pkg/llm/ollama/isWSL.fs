// §head page/pkg/llm/ollama.go:63-66 isWSL
// §sig func isWSL() bool
	_, err := os.Stat("/proc/sys/fs/binfmt_misc/WSLInterop")
	return err == nil
// §foot page/pkg/llm/ollama.go isWSL