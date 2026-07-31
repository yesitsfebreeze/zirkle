// §head page/pkg/sandbox/sandbox_test.go:34-38 TestProbeReportsHostCapability
// §sig func TestProbeReportsHostCapability(t *testing.T)
	if err := Probe(); err != nil && !strings.Contains(err.Error(), "sandbox unavailable") {
		t.Fatalf("unexpected probe error shape: %v", err)
	}
// §foot page/pkg/sandbox/sandbox_test.go TestProbeReportsHostCapability