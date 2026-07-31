// §head page/pkg/store/store_test.go:308-317 TestMigrationIsIdempotent
// §sig func TestMigrationIsIdempotent(t *testing.T)
	path := filepath.Join(t.TempDir(), "x.db")
	for i := 0; i < 3; i++ {
		s, err := Open(path)
		if err != nil {
			t.Fatalf("Open #%d: %v", i, err)
		}
		s.Close()
	}
// §foot page/pkg/store/store_test.go TestMigrationIsIdempotent