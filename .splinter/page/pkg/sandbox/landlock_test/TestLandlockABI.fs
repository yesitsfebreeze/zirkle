// §head page/pkg/sandbox/landlock_test.go:13-19 TestLandlockABI
// §sig func TestLandlockABI(t *testing.T)
	abi := LandlockABI()
	t.Logf("Landlock ABI: %d", abi)
	if abi == 0 {
		t.Skip("Landlock unavailable on this host")
	}
// §foot page/pkg/sandbox/landlock_test.go TestLandlockABI