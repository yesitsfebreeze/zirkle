// §head page/pkg/comp/rating.go:8-16 EnsureRatingSchema
// §sig func EnsureRatingSchema(db *sql.DB) error
	_, err := db.Exec(`CREATE TABLE IF NOT EXISTS shard_rating (
    shard_id  TEXT PRIMARY KEY,
    successes  INTEGER NOT NULL DEFAULT 0,
    failures   INTEGER NOT NULL DEFAULT 0,
    last_used  INTEGER NOT NULL DEFAULT 0
);`)
	return err
// §foot page/pkg/comp/rating.go EnsureRatingSchema