// §head page/pkg/llm/ollama.go:45-59 parseGatewayIP
// §sig func parseGatewayIP(routeData string) string
	for _, line := range strings.Split(routeData, "\n")[1:] {
		fields := strings.Fields(line)
		if len(fields) < 3 || fields[1] != "00000000" {
			continue
		}
		gw, err := hex.DecodeString(fields[2])
		if err != nil || len(gw) != 4 {
			return ""
		}
		// /proc/net/route stores the IP little-endian: reverse the bytes.
		return fmt.Sprintf("%d.%d.%d.%d", gw[3], gw[2], gw[1], gw[0])
	}
	return ""
// §foot page/pkg/llm/ollama.go parseGatewayIP