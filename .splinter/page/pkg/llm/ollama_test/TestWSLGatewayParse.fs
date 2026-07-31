// §head page/pkg/llm/ollama_test.go:190-214 TestWSLGatewayParse
// §sig func TestWSLGatewayParse(t *testing.T)
	// Synthetic /proc/net/route content: default route via 172.27.176.1
	// (01B01BAC little-endian).
	input := "Iface\tDestination\tGateway \tFlags\n" +
		"eth0\t00000000\t01B01BAC\t0003\n" +
		"eth0\t00B01BAC\t00000000\t0001\n"

	gw := parseGatewayIP(input)
	if gw != "172.27.176.1" {
		t.Errorf("gateway = %q, want 172.27.176.1", gw)
	}

	// No default route → empty.
	if got := parseGatewayIP("eth0\t00B01BAC\t00000000\t0001\n"); got != "" {
		t.Errorf("no default route gave %q, want empty", got)
	}

	// Garbage → empty, no panic.
	if got := parseGatewayIP("garbage"); got != "" {
		t.Errorf("garbage gave %q, want empty", got)
	}
	if got := parseGatewayIP(""); got != "" {
		t.Errorf("empty input gave %q, want empty", got)
	}
// §foot page/pkg/llm/ollama_test.go TestWSLGatewayParse