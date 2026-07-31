// §head page/pkg/comp/store.go:57-72 Store.Index
// §sig func (s *Store) Index(shard *Shard) error
	_, err := s.db.Exec(`
INSERT OR REPLACE INTO shard
    (key, name, kind, description, purpose, tags, path,
     use_when, not_when, danger, side_effects, requires,
     category, run, has_fm, body, justfile)
VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`,
		shard.Key, shard.Name, shard.Kind, shard.Description, shard.Purpose,
		strings.Join(shard.Tags, ","), shard.Path,
		shard.UseWhen, shard.NotWhen, shard.Danger, shard.SideEffects,
		strings.Join(shard.Requires, ","),
		shard.Category, shard.Run, boolToInt(shard.HasFM),
		shard.Body, shard.Justfile,
	)
	return err
// §foot page/pkg/comp/store.go Store.Index