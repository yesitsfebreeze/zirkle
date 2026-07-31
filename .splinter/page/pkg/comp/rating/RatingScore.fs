// §head page/pkg/comp/rating.go:53-59 RatingScore
// §sig func RatingScore(r *Rating) float64
	total := r.Successes + r.Failures
	if total == 0 {
		return 0
	}
	return float64(r.Successes-r.Failures) / float64(total)
// §foot page/pkg/comp/rating.go RatingScore