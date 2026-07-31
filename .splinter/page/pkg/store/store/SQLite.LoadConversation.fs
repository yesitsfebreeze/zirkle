// §head page/pkg/store/store.go:497-510 SQLite.LoadConversation
// §sig func (s *SQLite) LoadConversation(id string) (*ConversationRecord, error)
	var c ConversationRecord
	var created, updated int64
	err := s.db.QueryRow(`
SELECT id, state, intent, approved_plan, worker_id, recap, output, history, created_at, updated_at
FROM conversation WHERE id = ?
`, id).Scan(&c.ID, &c.State, &c.Intent, &c.ApprovedPlan, &c.WorkerID, &c.Recap, &c.Output, &c.History, &created, &updated)
	if err != nil {
		return nil, err
	}
	c.CreatedAt = time.Unix(created, 0)
	c.UpdatedAt = time.Unix(updated, 0)
	return &c, nil
// §foot page/pkg/store/store.go SQLite.LoadConversation