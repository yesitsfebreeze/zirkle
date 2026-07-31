// §head page/pkg/scheduler/scheduler_test.go:47-68 TestAddWiresJob
// §sig func TestAddWiresJob(t *testing.T)
	s := New()
	tr := &recordingTrigger{}
	if err := s.Add("*/5 * * * *", "agent-42", tr); err != nil {
		t.Fatalf("add: %v", err)
	}
	entries := s.cron.Entries()
	if len(entries) != 1 {
		t.Fatalf("want 1 entry, got %d", len(entries))
	}
	job, ok := entries[0].Job.(cronJob)
	if !ok {
		t.Fatalf("job type = %T, want cronJob", entries[0].Job)
	}
	if job.agentID != "agent-42" {
		t.Fatalf("agentID = %q, want agent-42", job.agentID)
	}
	job.Run()
	if atomic.LoadInt32(&tr.fired) != 1 {
		t.Fatalf("fired = %d, want 1", tr.fired)
	}
// §foot page/pkg/scheduler/scheduler_test.go TestAddWiresJob