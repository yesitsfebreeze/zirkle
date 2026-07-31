// §head page/pkg/store/store.go:219-233 migrate
// §sig func migrate(db *sql.DB) error
	for _, c := range addedColumns {
		has, err := hasColumn(db, c.table, c.column)
		if err != nil {
			return err
		}
		if has {
			continue
		}
		if _, err := db.Exec(c.ddl); err != nil {
			return fmt.Errorf("migrate %s.%s: %w", c.table, c.column, err)
		}
	}
	return nil
// §foot page/pkg/store/store.go migrate