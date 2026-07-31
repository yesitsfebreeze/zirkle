// §head page/pkg/scheduler/scheduler_test.go:29-35 TestAddInvalidSpec
// §sig func TestAddInvalidSpec(t *testing.T)
	s := New()
	tr := &recordingTrigger{}
	if err := s.Add("not-a-cron", "a1", tr); err == nil {
		t.Fatal("expected error for invalid cron spec")
	}
// §foot page/pkg/scheduler/scheduler_test.go TestAddInvalidSpec