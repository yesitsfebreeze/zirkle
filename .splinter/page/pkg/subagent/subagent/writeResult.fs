// §head page/pkg/subagent/subagent.go:209-220 writeResult
// §sig func writeResult(r Result)
	if os.Getenv("RELAY_RESULT_STDOUT") == "1" {
		json.NewEncoder(os.Stdout).Encode(r)
		return
	}
	pipe := os.NewFile(3, "pipe")
	if pipe == nil {
		return
	}
	defer pipe.Close()
	json.NewEncoder(pipe).Encode(r)
// §foot page/pkg/subagent/subagent.go writeResult