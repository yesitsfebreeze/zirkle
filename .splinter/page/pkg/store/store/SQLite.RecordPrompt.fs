// §head page/pkg/store/store.go:357-372 SQLite.RecordPrompt
// §sig func (s *SQLite) RecordPrompt(prompt string) error
	if strings.TrimSpace(prompt) == "" {
		return nil
	}
	if _, err := s.db.Exec(
		"INSERT INTO prompt_history (prompt, created_at) VALUES (?, ?)",
		prompt, time.Now().Unix(),
	); err != nil {
		return err
	}
	_, err := s.db.Exec(
		"DELETE FROM prompt_history WHERE id NOT IN (SELECT id FROM prompt_history ORDER BY id DESC LIMIT ?)",
		PromptHistoryLimit,
	)
	return err
// §foot page/pkg/store/store.go SQLite.RecordPrompt