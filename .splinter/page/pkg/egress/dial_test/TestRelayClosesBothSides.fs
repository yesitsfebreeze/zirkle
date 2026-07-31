// §head page/pkg/egress/dial_test.go:131-140 TestRelayClosesBothSides
// §sig func TestRelayClosesBothSides(t *testing.T)
	a1, a2 := net.Pipe()
	b1, b2 := net.Pipe()
	go Relay(a2, b1)

	a1.Close()
	if _, err := io.ReadAll(b2); err != nil {
		t.Fatalf("far side did not close cleanly: %v", err)
	}
// §foot page/pkg/egress/dial_test.go TestRelayClosesBothSides