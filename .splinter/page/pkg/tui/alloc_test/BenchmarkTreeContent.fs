// §head page/pkg/tui/alloc_test.go:86-92 BenchmarkTreeContent
// §sig func BenchmarkTreeContent(b *testing.B)
	m := benchModel(b, 50)
	b.ReportAllocs()
	for b.Loop() {
		_ = m.treeContent()
	}
// §foot page/pkg/tui/alloc_test.go BenchmarkTreeContent