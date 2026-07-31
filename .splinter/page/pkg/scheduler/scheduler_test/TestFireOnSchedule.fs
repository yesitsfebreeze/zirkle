// §head page/pkg/scheduler/scheduler_test.go:72-101 TestFireOnSchedule
// §sig func TestFireOnSchedule(t *testing.T)
	s := New(cron.WithSeconds())
	tr := &recordingTrigger{}
	// Fire every second.
	if err := s.Add("* * * * * *", "a1", tr); err != nil {
		t.Fatalf("add: %v", err)
	}
	s.Start()
	defer s.Stop()

	deadline := time.After(1500 * time.Millisecond)
	for {
		if atomic.LoadInt32(&tr.fired) >= 1 {
			break
		}
		select {
		case <-deadline:
			t.Fatalf("trigger did not fire within 1.5s (fired=%d)", tr.fired)
		default:
			time.Sleep(20 * time.Millisecond)
		}
	}

	tr.mu.Lock()
	got := tr.last
	tr.mu.Unlock()
	if got != "a1" {
		t.Fatalf("agentID = %q, want a1", got)
	}
// §foot page/pkg/scheduler/scheduler_test.go TestFireOnSchedule