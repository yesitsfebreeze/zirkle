// §head page/pkg/llm/ollama.go:33-39 wslGatewayIP
// §sig func wslGatewayIP() string
	data, err := os.ReadFile("/proc/net/route")
	if err != nil {
		return ""
	}
	return parseGatewayIP(string(data))
// §foot page/pkg/llm/ollama.go wslGatewayIP