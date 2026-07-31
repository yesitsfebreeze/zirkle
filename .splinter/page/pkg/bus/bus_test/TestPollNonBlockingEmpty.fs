// §head page/pkg/bus/bus_test.go:169-187 TestPollNonBlockingEmpty
// §sig func TestPollNonBlockingEmpty(t *testing.T)
	dir := t.TempDir()
	spool := filepath.Join(dir, "spool")

	id, err := GenerateIdentity()
	if err != nil {
		t.Fatal(err)
	}
	b := New(id, spool)

	// Poll on non-existent inbox returns empty quickly.
	envs, err := b.Poll()
	if err != nil {
		t.Fatalf("Poll on empty: %v", err)
	}
	if len(envs) != 0 {
		t.Fatalf("expected 0 envelopes, got %d", len(envs))
	}
// §foot page/pkg/bus/bus_test.go TestPollNonBlockingEmpty