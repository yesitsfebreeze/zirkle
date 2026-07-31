// §head page/pkg/tui/alloc_test.go:56-65 TestVisibleScalesLinearly
// §sig func TestVisibleScalesLinearly(t *testing.T)
	small := testing.AllocsPerRun(100, func() { _ = benchModelCached(t, 10).visible() })
	large := testing.AllocsPerRun(100, func() { _ = benchModelCached(t, 100).visible() })

	// 10x the rows must not cost anywhere near 100x the allocations.
	if small > 0 && large/small > 30 {
		t.Fatalf("visible() allocations grew %.1fx for 10x rows — looks quadratic", large/small)
	}
	t.Logf("visible(): %.0f allocs at 10 rows, %.0f at 100", small, large)
// §foot page/pkg/tui/alloc_test.go TestVisibleScalesLinearly