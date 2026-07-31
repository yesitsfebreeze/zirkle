// §head page/pkg/store/store.go:555-585 SQLite.ExecutionsByParents
// §sig func (s *SQLite) ExecutionsByParents(ids []string) (map[string][]*Execution, error)
	out := make(map[string][]*Execution, len(ids))
	if len(ids) == 0 {
		return out, nil
	}
	placeholders := strings.Repeat("?,", len(ids))
	placeholders = placeholders[:len(placeholders)-1]
	args := make([]any, len(ids))
	for i, id := range ids {
		args[i] = id
	}
	rows, err := s.db.Query(
		"SELECT id, parent_id, prompt, summary, output, success, tokens, model, created_at FROM execution WHERE parent_id IN ("+placeholders+") ORDER BY id ASC",
		args...,
	)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	for rows.Next() {
		var e Execution
		var success, created int64
		if err := rows.Scan(&e.ID, &e.ParentID, &e.Prompt, &e.Summary, &e.Output, &success, &e.Tokens, &e.Model, &created); err != nil {
			return nil, err
		}
		e.Success = success != 0
		e.CreatedAt = time.Unix(created, 0)
		out[e.ParentID] = append(out[e.ParentID], &e)
	}
	return out, rows.Err()
// §foot page/pkg/store/store.go SQLite.ExecutionsByParents