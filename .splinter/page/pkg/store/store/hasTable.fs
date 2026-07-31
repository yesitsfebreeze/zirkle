// §head page/pkg/store/store.go:207-214 hasTable
// §sig func hasTable(db *sql.DB, table string) (bool, error)
	rows, err := db.Query("SELECT 1 FROM sqlite_master WHERE type = 'table' AND name = ?", table)
	if err != nil {
		return false, err
	}
	defer rows.Close()
	return rows.Next(), rows.Err()
// §foot page/pkg/store/store.go hasTable