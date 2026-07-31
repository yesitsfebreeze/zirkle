// §head page/pkg/store/store.go:174-205 renameLegacy
// §sig func renameLegacy(db *sql.DB) error
	legacy, err := hasTable(db, "oorb")
	if err != nil {
		return err
	}
	current, err := hasTable(db, "pod")
	if err != nil {
		return err
	}
	if legacy && !current {
		if _, err := db.Exec("ALTER TABLE oorb RENAME TO pod"); err != nil {
			return fmt.Errorf("rename table oorb: %w", err)
		}
	}
	for _, c := range []struct{ table, from, to string }{
		{"checkpoint", "oorb_id", "pod_id"},
		{"fault", "orb_id", "pod_id"},
	} {
		has, err := hasColumn(db, c.table, c.from)
		if err != nil {
			return err
		}
		if !has {
			continue
		}
		ddl := fmt.Sprintf("ALTER TABLE %s RENAME COLUMN %s TO %s", c.table, c.from, c.to)
		if _, err := db.Exec(ddl); err != nil {
			return fmt.Errorf("rename %s.%s: %w", c.table, c.from, err)
		}
	}
	return nil
// §foot page/pkg/store/store.go renameLegacy