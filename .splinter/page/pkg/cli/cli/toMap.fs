// §head page/pkg/cli/cli.go:179-190 toMap
// §sig func toMap(v any) map[string]any
	if v == nil {
		return nil
	}
	if m, ok := v.(map[string]any); ok {
		return m
	}
	data, _ := json.Marshal(v)
	var m map[string]any
	json.Unmarshal(data, &m)
	return m
// §foot page/pkg/cli/cli.go toMap