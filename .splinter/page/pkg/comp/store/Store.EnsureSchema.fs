// §head page/pkg/comp/store.go:20-54 Store.EnsureSchema
// §sig func (s *Store) EnsureSchema() error
	_, err := s.db.Exec(`
CREATE TABLE IF NOT EXISTS shard (
    key         TEXT PRIMARY KEY,
    name        TEXT,
    kind        TEXT,
    description TEXT,
    purpose     TEXT,
    tags        TEXT,
    path        TEXT,
    use_when    TEXT,
    not_when    TEXT,
    danger      TEXT,
    side_effects TEXT,
    requires    TEXT,
    category    TEXT,
    run         TEXT,
    has_fm      INTEGER,
    body        TEXT,
    justfile    TEXT
);
CREATE TABLE IF NOT EXISTS edge (
    src TEXT,
    dst TEXT,
    PRIMARY KEY(src, dst)
);
CREATE TABLE IF NOT EXISTS shard_rating (
    shard_id   TEXT PRIMARY KEY,
    successes  INTEGER NOT NULL DEFAULT 0,
    failures   INTEGER NOT NULL DEFAULT 0,
    last_used  INTEGER NOT NULL DEFAULT 0
);
`)
	return err
// §foot page/pkg/comp/store.go Store.EnsureSchema