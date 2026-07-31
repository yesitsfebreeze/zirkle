// §head page/pkg/store/store.go:310-317 SQLite.LoadCheckpoint
// §sig func (s *SQLite) LoadCheckpoint(id string, turn int) ([]byte, error)
	var state []byte
	err := s.db.QueryRow("SELECT state FROM checkpoint WHERE pod_id = ? AND turn = ?", id, turn).Scan(&state)
	if err != nil {
		return nil, err
	}
	return state, nil
// §foot page/pkg/store/store.go SQLite.LoadCheckpoint