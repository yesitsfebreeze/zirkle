// §head page/pkg/fault/fault_test.go:75-81 TestGuardIgnoresCleanReturn
// §sig func TestGuardIgnoresCleanReturn(t *testing.T)
	s := &memSink{}
	func() { defer Guard(s, "", "test.site") }()
	if s.len() != 0 {
		t.Fatalf("recorded %d faults on a clean return, want 0", s.len())
	}
// §foot page/pkg/fault/fault_test.go TestGuardIgnoresCleanReturn