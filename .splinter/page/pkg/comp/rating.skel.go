// §source page/pkg/comp/rating.go
package comp

import (
	"database/sql"
	"time"
)

func EnsureRatingSchema(db *sql.DB) error {
// §.splinter/page/pkg/comp/rating/EnsureRatingSchema.fs
}

func RecordResult(db *sql.DB, shardKey string, success bool) error {
// §.splinter/page/pkg/comp/rating/RecordResult.fs
}

type Rating struct {
	Successes int
	Failures  int
	LastUsed  int64
}

func GetRating(db *sql.DB, shardKey string) (*Rating, error) {
// §.splinter/page/pkg/comp/rating/GetRating.fs
}

func RatingScore(r *Rating) float64 {
// §.splinter/page/pkg/comp/rating/RatingScore.fs
}
