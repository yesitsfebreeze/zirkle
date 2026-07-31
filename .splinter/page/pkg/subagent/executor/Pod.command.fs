// §head page/pkg/subagent/executor.go:241-246 Pod.command
// §sig func (o Pod) command() string
	if o.Command == "" {
		return "ssh"
	}
	return o.Command
// §foot page/pkg/subagent/executor.go Pod.command