// §head page/pkg/fault/fault_test.go:62-73 TestGuardStopsThePanic
// §sig func TestGuardStopsThePanic(t *testing.T)
	s := &memSink{}
	survived := false
	func() {
		defer func() { survived = true }()
		defer Guard(s, "", "test.site")
		panic("boom")
	}()
	if !survived {
		t.Fatal("panic escaped Guard")
	}
// §foot page/pkg/fault/fault_test.go TestGuardStopsThePanic