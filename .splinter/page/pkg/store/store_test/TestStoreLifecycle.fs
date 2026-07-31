// §head page/pkg/store/store_test.go:11-55 TestStoreLifecycle
// §sig func TestStoreLifecycle(t *testing.T)
	s, err := Open(filepath.Join(t.TempDir(), "test.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer s.Close()

	if err := s.Create("a1", "hello", "smart"); err != nil {
		t.Fatal(err)
	}
	o, err := s.Load("a1")
	if err != nil {
		t.Fatal(err)
	}
	if o.Prompt != "hello" || o.Mode != "smart" || o.State != "created" {
		t.Fatalf("unexpected relay: %+v", o)
	}
	o.State = "running"
	if err := s.Save(o); err != nil {
		t.Fatal(err)
	}
	if o2, _ := s.Load("a1"); o2.State != "running" {
		t.Fatalf("state not saved: %s", o2.State)
	}
	state := []byte("checkpoint-data")
	if err := s.Checkpoint("a1", 1, state); err != nil {
		t.Fatal(err)
	}
	got, err := s.LoadCheckpoint("a1", 1)
	if err != nil {
		t.Fatal(err)
	}
	if string(got) != string(state) {
		t.Fatalf("checkpoint mismatch: %q", got)
	}
	if list, _ := s.List(); len(list) != 1 {
		t.Fatalf("list len: %d", len(list))
	}
	if err := s.Delete("a1"); err != nil {
		t.Fatal(err)
	}
	if list, _ := s.List(); len(list) != 0 {
		t.Fatalf("list after delete: %d", len(list))
	}
// §foot page/pkg/store/store_test.go TestStoreLifecycle