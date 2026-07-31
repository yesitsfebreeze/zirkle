// §head page/pkg/comp/store_test.go:10-22 testStore
// §sig func testStore(t *testing.T) *Store
	t.Helper()
	db, err := sql.Open("sqlite", ":memory:")
	if err != nil {
		t.Fatal(err)
	}
	t.Cleanup(func() { db.Close() })
	s := Open(db)
	if err := s.EnsureSchema(); err != nil {
		t.Fatal(err)
	}
	return s
// §foot page/pkg/comp/store_test.go testStore