// §head page/pkg/fault/fault_test.go:35-58 TestGuardRecordsPanicWithStack
// §sig func TestGuardRecordsPanicWithStack(t *testing.T)
	s := &memSink{}
	func() {
		defer Guard(s, "pod-1", "test.site")
		panic("boom")
	}()

	if s.len() != 1 {
		t.Fatalf("want 1 fault recorded, got %d", s.len())
	}
	got := s.rows[0]
	if got.kind != KindPanic {
		t.Errorf("kind = %q, want %q", got.kind, KindPanic)
	}
	if got.podID != "pod-1" || got.site != "test.site" {
		t.Errorf("podID/site = %q/%q, want pod-1/test.site", got.podID, got.site)
	}
	if got.msg != "boom" {
		t.Errorf("msg = %q, want boom", got.msg)
	}
	if !strings.Contains(got.stack, "TestGuardRecordsPanicWithStack") {
		t.Errorf("stack does not name the panicking test:\n%s", got.stack)
	}
// §foot page/pkg/fault/fault_test.go TestGuardRecordsPanicWithStack