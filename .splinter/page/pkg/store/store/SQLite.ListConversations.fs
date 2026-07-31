// §head page/pkg/store/store.go:512-533 SQLite.ListConversations
// §sig func (s *SQLite) ListConversations() ([]*ConversationRecord, error)
	rows, err := s.db.Query(`
SELECT id, state, intent, approved_plan, worker_id, recap, output, history, created_at, updated_at
FROM conversation ORDER BY created_at ASC
`)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []*ConversationRecord
	for rows.Next() {
		var c ConversationRecord
		var created, updated int64
		if err := rows.Scan(&c.ID, &c.State, &c.Intent, &c.ApprovedPlan, &c.WorkerID, &c.Recap, &c.Output, &c.History, &created, &updated); err != nil {
			return nil, err
		}
		c.CreatedAt = time.Unix(created, 0)
		c.UpdatedAt = time.Unix(updated, 0)
		out = append(out, &c)
	}
	return out, rows.Err()
// §foot page/pkg/store/store.go SQLite.ListConversations