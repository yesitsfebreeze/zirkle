// §head page/pkg/sandbox/sandbox_test.go:155-162 TestNetworkOffByDefault
// §sig func TestNetworkOffByDefault(t *testing.T)
	requireSandbox(t)

	out, _ := run(t, Spec{Dir: t.TempDir()}, "ip -o link show 2>/dev/null || cat /proc/net/dev")
	if strings.Contains(out, "eth0") {
		t.Fatalf("sandbox kept a host network interface: %s", out)
	}
// §foot page/pkg/sandbox/sandbox_test.go TestNetworkOffByDefault