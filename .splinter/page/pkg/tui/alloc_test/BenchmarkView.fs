// §head page/pkg/tui/alloc_test.go:78-84 BenchmarkView
// §sig func BenchmarkView(b *testing.B)
	m := benchModel(b, 50)
	b.ReportAllocs()
	for b.Loop() {
		_ = m.View()
	}
// §foot page/pkg/tui/alloc_test.go BenchmarkView