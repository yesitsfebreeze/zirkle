// §head page/pkg/comp/store.go:95-102 Store.Get
// §sig func (s *Store) Get(key string) (*Shard, error)
	row := s.db.QueryRow(`
SELECT key, name, kind, description, purpose, tags, path,
       use_when, not_when, danger, side_effects, requires,
       category, run, has_fm, body, justfile
FROM shard WHERE key = ?`, key)
	return scanShard(row)
// §foot page/pkg/comp/store.go Store.Get