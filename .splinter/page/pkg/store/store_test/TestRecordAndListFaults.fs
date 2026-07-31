// §head page/pkg/store/store_test.go:104-144 TestRecordAndListFaults
// §sig func TestRecordAndListFaults(t *testing.T)
	s, err := Open(filepath.Join(t.TempDir(), "t.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer s.Close()

	if err := s.RecordFault("pod-1", "panic", "pod.run", "boom", "stack here"); err != nil {
		t.Fatal(err)
	}
	if err := s.RecordFault("", "error", "daemon.webhook", "dial failed", ""); err != nil {
		t.Fatal(err)
	}

	faults, err := s.Faults(0)
	if err != nil {
		t.Fatal(err)
	}
	if len(faults) != 2 {
		t.Fatalf("want 2 faults, got %d", len(faults))
	}
	// Newest first: the daemon-level error was recorded last.
	if faults[0].Site != "daemon.webhook" {
		t.Errorf("faults[0].Site = %q, want daemon.webhook (newest first)", faults[0].Site)
	}
	var panicFault *Fault
	for _, f := range faults {
		if f.Kind == "panic" {
			panicFault = f
		}
	}
	if panicFault == nil {
		t.Fatal("panic fault not returned")
	}
	if panicFault.PodID != "pod-1" || panicFault.Stack != "stack here" {
		t.Errorf("got %+v, want pod-1 with its stack preserved", panicFault)
	}
	if panicFault.CreatedAt.IsZero() {
		t.Error("CreatedAt not populated")
	}
// §foot page/pkg/store/store_test.go TestRecordAndListFaults