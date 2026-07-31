// §head page/pkg/store/store_test.go:146-165 TestFaultsLimit
// §sig func TestFaultsLimit(t *testing.T)
	s, err := Open(filepath.Join(t.TempDir(), "t.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer s.Close()

	for i := 0; i < 5; i++ {
		if err := s.RecordFault("", "error", "site", "msg", ""); err != nil {
			t.Fatal(err)
		}
	}
	faults, err := s.Faults(2)
	if err != nil {
		t.Fatal(err)
	}
	if len(faults) != 2 {
		t.Fatalf("want 2 faults with limit=2, got %d", len(faults))
	}
// §foot page/pkg/store/store_test.go TestFaultsLimit