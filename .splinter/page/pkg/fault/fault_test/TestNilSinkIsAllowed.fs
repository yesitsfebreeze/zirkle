// §head page/pkg/fault/fault_test.go:111-117 TestNilSinkIsAllowed
// §sig func TestNilSinkIsAllowed(t *testing.T)
	func() {
		defer Guard(nil, "", "test.site")
		panic("boom")
	}()
	Record(nil, "", "test.site", errors.New("x"))
// §foot page/pkg/fault/fault_test.go TestNilSinkIsAllowed