// §head page/pkg/store/store.go:535-538 SQLite.DeleteConversation
// §sig func (s *SQLite) DeleteConversation(id string) error
	_, err := s.db.Exec("DELETE FROM conversation WHERE id = ?", id)
	return err
// §foot page/pkg/store/store.go SQLite.DeleteConversation