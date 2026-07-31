// §head page/pkg/fault/fault_test.go:83-89 TestRecordIgnoresNilError
// §sig func TestRecordIgnoresNilError(t *testing.T)
	s := &memSink{}
	Record(s, "", "test.site", nil)
	if s.len() != 0 {
		t.Fatalf("recorded %d faults for a nil error, want 0", s.len())
	}
// §foot page/pkg/fault/fault_test.go TestRecordIgnoresNilError