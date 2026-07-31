// §head page/pkg/comp/rating.go:18-32 RecordResult
// §sig func RecordResult(db *sql.DB, shardKey string, success bool) error
	now := time.Now().Unix()
	if success {
		_, err := db.Exec(`INSERT INTO shard_rating (shard_id, successes, failures, last_used)
VALUES (?, 1, 0, ?)
ON CONFLICT(shard_id) DO UPDATE SET successes = successes + 1, last_used = ?`,
			shardKey, now, now)
		return err
	}
	_, err := db.Exec(`INSERT INTO shard_rating (shard_id, successes, failures, last_used)
VALUES (?, 0, 1, ?)
ON CONFLICT(shard_id) DO UPDATE SET failures = failures + 1, last_used = ?`,
		shardKey, now, now)
	return err
// §foot page/pkg/comp/rating.go RecordResult