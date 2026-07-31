// §head page/pkg/store/store.go:475-495 SQLite.SaveConversation
// §sig func (s *SQLite) SaveConversation(c *ConversationRecord) error
	now := time.Now().Unix()
	created := c.CreatedAt.Unix()
	if created <= 0 {
		created = now
	}
	_, err := s.db.Exec(`
INSERT INTO conversation (id, state, intent, approved_plan, worker_id, recap, output, history, created_at, updated_at)
VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
ON CONFLICT(id) DO UPDATE SET
  state=excluded.state,
  intent=excluded.intent,
  approved_plan=excluded.approved_plan,
  worker_id=excluded.worker_id,
  recap=excluded.recap,
  output=excluded.output,
  history=excluded.history,
  updated_at=excluded.updated_at
`, c.ID, c.State, c.Intent, c.ApprovedPlan, c.WorkerID, c.Recap, c.Output, c.History, created, now)
	return err
// §foot page/pkg/store/store.go SQLite.SaveConversation