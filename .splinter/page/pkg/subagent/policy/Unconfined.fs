// §head page/pkg/subagent/policy.go:54-60 Unconfined
// §sig func Unconfined() bool
	switch os.Getenv(EnvSandbox) {
	case "off", "0", "false", "no":
		return true
	}
	return false
// §foot page/pkg/subagent/policy.go Unconfined