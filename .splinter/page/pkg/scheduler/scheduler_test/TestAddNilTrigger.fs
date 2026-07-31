// §head page/pkg/scheduler/scheduler_test.go:37-42 TestAddNilTrigger
// §sig func TestAddNilTrigger(t *testing.T)
	s := New()
	if err := s.Add("* * * * *", "a1", nil); err == nil {
		t.Fatal("expected error for nil trigger")
	}
// §foot page/pkg/scheduler/scheduler_test.go TestAddNilTrigger