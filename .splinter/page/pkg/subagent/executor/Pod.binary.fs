// §head page/pkg/subagent/executor.go:248-253 Pod.binary
// §sig func (o Pod) binary() string
	if o.Binary == "" {
		return "relay"
	}
	return o.Binary
// §foot page/pkg/subagent/executor.go Pod.binary