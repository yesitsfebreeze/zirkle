// §head page/pkg/comp/rating.go:40-51 GetRating
// §sig func GetRating(db *sql.DB, shardKey string) (*Rating, error)
	var r Rating
	err := db.QueryRow(`SELECT successes, failures, last_used FROM shard_rating WHERE shard_id = ?`, shardKey).
		Scan(&r.Successes, &r.Failures, &r.LastUsed)
	if err == sql.ErrNoRows {
		return &Rating{}, nil
	}
	if err != nil {
		return nil, err
	}
	return &r, nil
// §foot page/pkg/comp/rating.go GetRating