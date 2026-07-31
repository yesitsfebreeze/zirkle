// §head page/pkg/store/store.go:142-160 Open
// §sig func Open(path string) (*SQLite, error)
	db, err := sql.Open("sqlite", path)
	if err != nil {
		return nil, err
	}
	if err := renameLegacy(db); err != nil {
		db.Close()
		return nil, err
	}
	if _, err := db.Exec(schema); err != nil {
		db.Close()
		return nil, err
	}
	if err := migrate(db); err != nil {
		db.Close()
		return nil, err
	}
	return &SQLite{db: db}, nil
// §foot page/pkg/store/store.go Open