// §head page/pkg/store/store.go:269-275 SQLite.Save
// §sig func (s *SQLite) Save(o *Pod) error
	_, err := s.db.Exec(
		"UPDATE pod SET prompt = ?, mode = ?, state = ?, recap = ?, updated_at = ? WHERE id = ?",
		o.Prompt, o.Mode, o.State, o.Recap, time.Now().Unix(), o.ID,
	)
	return err
// §foot page/pkg/store/store.go SQLite.Save