// §head page/pkg/fault/fault_test.go:91-100 TestRecordStoresError
// §sig func TestRecordStoresError(t *testing.T)
	s := &memSink{}
	Record(s, "pod-2", "test.site", errors.New("disk on fire"))
	if s.len() != 1 {
		t.Fatalf("want 1 fault, got %d", s.len())
	}
	if s.rows[0].kind != KindError || s.rows[0].msg != "disk on fire" {
		t.Errorf("got %+v, want an error fault carrying the message", s.rows[0])
	}
// §foot page/pkg/fault/fault_test.go TestRecordStoresError