// §head page/pkg/store/store.go:319-324 SQLite.RecordFault
// §sig func (s *SQLite) RecordFault(podID, kind, site, msg, stack string) error
	_, err := s.db.Exec(
		"INSERT INTO fault (pod_id, kind, site, msg, stack, created_at) VALUES (?, ?, ?, ?, ?, ?)",
		podID, kind, site, msg, stack, time.Now().Unix())
	return err
// §foot page/pkg/store/store.go SQLite.RecordFault