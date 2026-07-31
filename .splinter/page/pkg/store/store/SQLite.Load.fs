// §head page/pkg/store/store.go:255-267 SQLite.Load
// §sig func (s *SQLite) Load(id string) (*Pod, error)
	var o Pod
	var created, updated int64
	err := s.db.QueryRow(
		"SELECT id, prompt, mode, state, recap, created_at, updated_at FROM pod WHERE id = ?", id,
	).Scan(&o.ID, &o.Prompt, &o.Mode, &o.State, &o.Recap, &created, &updated)
	if err != nil {
		return nil, err
	}
	o.CreatedAt = time.Unix(created, 0)
	o.UpdatedAt = time.Unix(updated, 0)
	return &o, nil
// §foot page/pkg/store/store.go SQLite.Load