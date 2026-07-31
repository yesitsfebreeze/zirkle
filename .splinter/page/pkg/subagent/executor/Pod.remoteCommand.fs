// §head page/pkg/subagent/executor.go:258-272 Pod.remoteCommand
// §sig func (o Pod) remoteCommand(cfg Config) string
	parts := []string{"RELAY_RESULT_STDOUT=1"}
	for _, kv := range o.Env {
		key, value, found := strings.Cut(kv, "=")
		if !found {
			continue
		}
		parts = append(parts, key+"="+shellQuote(value))
	}
	parts = append(parts, shellQuote(o.binary()))
	for _, a := range subagentArgs(cfg) {
		parts = append(parts, shellQuote(a))
	}
	return strings.Join(parts, " ")
// §foot page/pkg/subagent/executor.go Pod.remoteCommand