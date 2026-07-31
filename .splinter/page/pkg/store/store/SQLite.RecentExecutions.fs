// §head page/pkg/store/store.go:445-458 SQLite.RecentExecutions
// §sig func (s *SQLite) RecentExecutions(limit int) ([]*Execution, error)
	if limit <= 0 {
		limit = 20
	}
	rows, err := s.db.Query(
		"SELECT id, parent_id, prompt, summary, output, success, tokens, model, created_at FROM execution ORDER BY id DESC LIMIT ?",
		limit,
	)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	return scanExecutions(rows)
// §foot page/pkg/store/store.go SQLite.RecentExecutions