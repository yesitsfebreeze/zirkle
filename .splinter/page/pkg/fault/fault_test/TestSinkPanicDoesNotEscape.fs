// §head page/pkg/fault/fault_test.go:107-109 TestSinkPanicDoesNotEscape
// §sig func TestSinkPanicDoesNotEscape(t *testing.T)
	Record(&memSink{boom: true}, "", "test.site", errors.New("original"))
// §foot page/pkg/fault/fault_test.go TestSinkPanicDoesNotEscape