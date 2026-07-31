// §head page/pkg/comp/store.go:81-92 Store.All
// §sig func (s *Store) All() ([]Shard, error)
	rows, err := s.db.Query(`
SELECT key, name, kind, description, purpose, tags, path,
       use_when, not_when, danger, side_effects, requires,
       category, run, has_fm, body, justfile
FROM shard`)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	return scanShards(rows)
// §foot page/pkg/comp/store.go Store.All