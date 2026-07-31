// §head page/pkg/store/store.go:302-308 SQLite.Checkpoint
// §sig func (s *SQLite) Checkpoint(id string, turn int, state []byte) error
	_, err := s.db.Exec(
		"INSERT OR REPLACE INTO checkpoint (pod_id, turn, state, created_at) VALUES (?, ?, ?, ?)",
		id, turn, state, time.Now().Unix(),
	)
	return err
// §foot page/pkg/store/store.go SQLite.Checkpoint