// §head page/cmd/relay/main.go:654-664 openCompDB
// §sig func openCompDB() *sql.DB
	dir := dataDir()
	os.MkdirAll(dir, 0o755)
	dbPath := filepath.Join(dir, "relay.db")
	db, err := sql.Open("sqlite", dbPath)
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
	return db
// §foot page/cmd/relay/main.go openCompDB