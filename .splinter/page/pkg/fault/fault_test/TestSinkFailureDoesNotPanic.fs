// §head page/pkg/fault/fault_test.go:103-105 TestSinkFailureDoesNotPanic
// §sig func TestSinkFailureDoesNotPanic(t *testing.T)
	Record(&memSink{fail: errors.New("db down")}, "", "test.site", errors.New("original"))
// §foot page/pkg/fault/fault_test.go TestSinkFailureDoesNotPanic