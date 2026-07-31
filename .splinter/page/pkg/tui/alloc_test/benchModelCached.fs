// §head page/pkg/tui/alloc_test.go:69-76 benchModelCached
// §sig func benchModelCached(t testing.TB, n int) Model
	if m, ok := modelCache[n]; ok {
		return m
	}
	m := benchModel(t, n)
	modelCache[n] = m
	return m
// §foot page/pkg/tui/alloc_test.go benchModelCached