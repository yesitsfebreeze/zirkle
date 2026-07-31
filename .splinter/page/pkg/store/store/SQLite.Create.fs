// §head page/pkg/store/store.go:246-253 SQLite.Create
// §sig func (s *SQLite) Create(id, prompt, mode string) error
	now := time.Now().Unix()
	_, err := s.db.Exec(
		"INSERT INTO pod (id, prompt, mode, recap, state, created_at, updated_at) VALUES (?, ?, ?, '', 'created', ?, ?)",
		id, prompt, mode, now, now,
	)
	return err
// §foot page/pkg/store/store.go SQLite.Create