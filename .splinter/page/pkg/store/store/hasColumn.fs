// §head page/pkg/store/store.go:235-242 hasColumn
// §sig func hasColumn(db *sql.DB, table, column string) (bool, error)
	rows, err := db.Query("SELECT 1 FROM pragma_table_info(?) WHERE name = ?", table, column)
	if err != nil {
		return false, err
	}
	defer rows.Close()
	return rows.Next(), rows.Err()
// §foot page/pkg/store/store.go hasColumn