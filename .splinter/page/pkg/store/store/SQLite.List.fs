// §head page/pkg/store/store.go:277-295 SQLite.List
// §sig func (s *SQLite) List() ([]*Pod, error)
	rows, err := s.db.Query("SELECT id, prompt, mode, state, recap, created_at, updated_at FROM pod ORDER BY created_at ASC")
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []*Pod
	for rows.Next() {
		var o Pod
		var created, updated int64
		if err := rows.Scan(&o.ID, &o.Prompt, &o.Mode, &o.State, &o.Recap, &created, &updated); err != nil {
			return nil, err
		}
		o.CreatedAt = time.Unix(created, 0)
		o.UpdatedAt = time.Unix(updated, 0)
		out = append(out, &o)
	}
	return out, rows.Err()
// §foot page/pkg/store/store.go SQLite.List