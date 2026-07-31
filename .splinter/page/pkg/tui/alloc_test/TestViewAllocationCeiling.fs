// §head page/pkg/tui/alloc_test.go:36-43 TestViewAllocationCeiling
// §sig func TestViewAllocationCeiling(t *testing.T)
	m := benchModel(t, 50)
	got := testing.AllocsPerRun(50, func() { _ = m.View() })
	if got > maxViewAllocs {
		t.Fatalf("View allocated %.0f objects for 50 pods, ceiling %d — a regression in the render path", got, maxViewAllocs)
	}
	t.Logf("View: %.0f allocs for 50 pods", got)
// §foot page/pkg/tui/alloc_test.go TestViewAllocationCeiling