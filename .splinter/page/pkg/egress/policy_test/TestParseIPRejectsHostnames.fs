// §head page/pkg/egress/policy_test.go:63-77 TestParseIPRejectsHostnames
// §sig func TestParseIPRejectsHostnames(t *testing.T)
	for _, host := range []string{
		"example.com",
		"1.2.3.4.5",
		"256.0.0.1",
		"1.2.3.400",
		"0x1.0x2.0x3.0x4.0x5",
		"09.0.0.1", // 9 is not an octal digit sequence in a leading-zero part
		"",
	} {
		if _, ok := parseIP(host); ok {
			t.Errorf("parseIP(%q) parsed as an IP, want hostname", host)
		}
	}
// §foot page/pkg/egress/policy_test.go TestParseIPRejectsHostnames