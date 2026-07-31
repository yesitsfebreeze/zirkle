// §head page/pkg/comp/store.go:106-120 Store.Search
// §sig func (s *Store) Search(query string) ([]Shard, error)
	like := "%" + query + "%"
	rows, err := s.db.Query(`
SELECT key, name, kind, description, purpose, tags, path,
       use_when, not_when, danger, side_effects, requires,
       category, run, has_fm, body, justfile
FROM shard
WHERE name LIKE ? OR description LIKE ? OR tags LIKE ? OR use_when LIKE ?`,
		like, like, like, like)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	return scanShards(rows)
// §foot page/pkg/comp/store.go Store.Search