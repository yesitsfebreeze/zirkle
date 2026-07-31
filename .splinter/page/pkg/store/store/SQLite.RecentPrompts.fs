// §head page/pkg/store/store.go:375-393 SQLite.RecentPrompts
// §sig func (s *SQLite) RecentPrompts(limit int) ([]string, error)
	if limit <= 0 || limit > PromptHistoryLimit {
		limit = PromptHistoryLimit
	}
	rows, err := s.db.Query("SELECT prompt FROM prompt_history ORDER BY id DESC LIMIT ?", limit)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []string
	for rows.Next() {
		var p string
		if err := rows.Scan(&p); err != nil {
			return nil, err
		}
		out = append(out, p)
	}
	return out, rows.Err()
// §foot page/pkg/store/store.go SQLite.RecentPrompts