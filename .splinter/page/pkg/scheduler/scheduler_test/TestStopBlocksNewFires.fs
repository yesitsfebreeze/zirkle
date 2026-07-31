// §head page/pkg/scheduler/scheduler_test.go:105-120 TestStopBlocksNewFires
// §sig func TestStopBlocksNewFires(t *testing.T)
	s := New(cron.WithSeconds())
	tr := &recordingTrigger{}
	if err := s.Add("* * * * * *", "a1", tr); err != nil {
		t.Fatalf("add: %v", err)
	}
	s.Start()
	<-s.Stop().Done() // wait for in-flight jobs to drain

	before := atomic.LoadInt32(&tr.fired)
	time.Sleep(1200 * time.Millisecond)
	after := atomic.LoadInt32(&tr.fired)
	if after > before {
		t.Fatalf("schedule fired after Stop: before=%d after=%d", before, after)
	}
// §foot page/pkg/scheduler/scheduler_test.go TestStopBlocksNewFires