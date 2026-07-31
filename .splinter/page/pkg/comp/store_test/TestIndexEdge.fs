// §head page/pkg/comp/store_test.go:104-120 TestIndexEdge
// §sig func TestIndexEdge(t *testing.T)
	s := testStore(t)
	if err := s.IndexEdge("a", "b"); err != nil {
		t.Fatal(err)
	}
	if err := s.IndexEdge("a", "b"); err != nil {
		t.Fatal(err) // OR IGNORE, no duplicate error
	}
	var count int
	err := s.db.QueryRow("SELECT COUNT(*) FROM edge WHERE src = 'a' AND dst = 'b'").Scan(&count)
	if err != nil {
		t.Fatal(err)
	}
	if count != 1 {
		t.Errorf("expected 1 edge, got %d", count)
	}
// §foot page/pkg/comp/store_test.go TestIndexEdge