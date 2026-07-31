// §head page/pkg/subagent/policy.go:34-51 DefaultSpec
// §sig func DefaultSpec() sandbox.Spec
	ollamaHost := os.Getenv("OLLAMA_HOST")
	if ollamaHost == "" {
		ollamaHost = "127.0.0.1:11434"
	}
	host, _, err := net.SplitHostPort(ollamaHost)
	if err != nil {
		host = ollamaHost // assume hostname without port
	}
	return sandbox.Spec{
		Ephemeral: true,
		SizeMB:    256,
		Net:       false,
		Egress: &egress.Policy{
			AllowedDomains: []string{host},
		},
	}
// §foot page/pkg/subagent/policy.go DefaultSpec