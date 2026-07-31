// §head page/pkg/store/store.go:297-300 SQLite.Delete
// §sig func (s *SQLite) Delete(id string) error
	_, err := s.db.Exec("DELETE FROM pod WHERE id = ?", id)
	return err
// §foot page/pkg/store/store.go SQLite.Delete