// §head page/pkg/store/store.go:542-551 SQLite.LatestCheckpoint
// §sig func (s *SQLite) LatestCheckpoint(id string) ([]byte, error)
	var state []byte
	err := s.db.QueryRow(
		"SELECT state FROM checkpoint WHERE pod_id = ? ORDER BY turn DESC LIMIT 1", id,
	).Scan(&state)
	if err == sql.ErrNoRows {
		return nil, nil
	}
	return state, err
// §foot page/pkg/store/store.go SQLite.LatestCheckpoint