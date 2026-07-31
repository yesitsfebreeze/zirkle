// §head page/pkg/comp/rating_test.go:10-21 testDB
// §sig func testDB(t *testing.T) *sql.DB
	t.Helper()
	db, err := sql.Open("sqlite", ":memory:")
	if err != nil {
		t.Fatal(err)
	}
	t.Cleanup(func() { db.Close() })
	if err := EnsureRatingSchema(db); err != nil {
		t.Fatal(err)
	}
	return db
// §foot page/pkg/comp/rating_test.go testDB