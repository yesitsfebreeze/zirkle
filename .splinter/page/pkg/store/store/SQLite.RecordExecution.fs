// §head page/pkg/store/store.go:411-421 SQLite.RecordExecution
// §sig func (s *SQLite) RecordExecution(e *Execution) error
	success := 0
	if e.Success {
		success = 1
	}
	_, err := s.db.Exec(
		"INSERT INTO execution (parent_id, prompt, summary, output, success, tokens, model, created_at) VALUES (?, ?, ?, ?, ?, ?, ?, ?)",
		e.ParentID, e.Prompt, e.Summary, e.Output, success, e.Tokens, e.Model, time.Now().Unix(),
	)
	return err
// §foot page/pkg/store/store.go SQLite.RecordExecution