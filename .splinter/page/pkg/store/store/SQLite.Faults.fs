// §head page/pkg/store/store.go:327-348 SQLite.Faults
// §sig func (s *SQLite) Faults(limit int) ([]*Fault, error)
	if limit <= 0 {
		limit = 50
	}
	rows, err := s.db.Query(
		"SELECT id, pod_id, kind, site, msg, stack, created_at FROM fault ORDER BY created_at DESC, id DESC LIMIT ?", limit)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []*Fault
	for rows.Next() {
		var f Fault
		var created int64
		if err := rows.Scan(&f.ID, &f.PodID, &f.Kind, &f.Site, &f.Msg, &f.Stack, &created); err != nil {
			return nil, err
		}
		f.CreatedAt = time.Unix(created, 0)
		out = append(out, &f)
	}
	return out, rows.Err()
// §foot page/pkg/store/store.go SQLite.Faults