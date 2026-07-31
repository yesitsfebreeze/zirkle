// §head page/pkg/store/store.go:425-442 SQLite.SearchExecutions
// §sig func (s *SQLite) SearchExecutions(query string, limit int) ([]*Execution, error)
	if limit <= 0 {
		limit = 20
	}
	if strings.TrimSpace(query) == "" {
		return s.RecentExecutions(limit)
	}
	like := "%" + query + "%"
	rows, err := s.db.Query(
		"SELECT id, parent_id, prompt, summary, output, success, tokens, model, created_at FROM execution WHERE prompt LIKE ? OR summary LIKE ? OR output LIKE ? ORDER BY id DESC LIMIT ?",
		like, like, like, limit,
	)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	return scanExecutions(rows)
// §foot page/pkg/store/store.go SQLite.SearchExecutions