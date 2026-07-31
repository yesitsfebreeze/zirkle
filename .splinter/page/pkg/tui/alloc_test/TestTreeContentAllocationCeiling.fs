// §head page/pkg/tui/alloc_test.go:45-52 TestTreeContentAllocationCeiling
// §sig func TestTreeContentAllocationCeiling(t *testing.T)
	m := benchModel(t, 50)
	got := testing.AllocsPerRun(50, func() { _ = m.treeContent() })
	if got > maxTreeAllocs {
		t.Fatalf("treeContent allocated %.0f objects for 50 pods, ceiling %d", got, maxTreeAllocs)
	}
	t.Logf("treeContent: %.0f allocs for 50 pods", got)
// §foot page/pkg/tui/alloc_test.go TestTreeContentAllocationCeiling