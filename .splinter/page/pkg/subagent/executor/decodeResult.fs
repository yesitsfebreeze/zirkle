// §head page/pkg/subagent/executor.go:303-316 decodeResult
// §sig func decodeResult(out []byte) (*Result, error)
	lines := strings.Split(string(out), "\n")
	for i := len(lines) - 1; i >= 0; i-- {
		line := strings.TrimSpace(lines[i])
		if !strings.HasPrefix(line, "{") {
			continue
		}
		var res Result
		if err := json.Unmarshal([]byte(line), &res); err == nil {
			return &res, nil
		}
	}
	return nil, errors.New("no result JSON on stdout")
// §foot page/pkg/subagent/executor.go decodeResult