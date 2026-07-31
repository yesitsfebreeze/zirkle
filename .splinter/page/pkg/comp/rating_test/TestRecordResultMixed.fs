// §head page/pkg/comp/rating_test.go:54-67 TestRecordResultMixed
// §sig func TestRecordResultMixed(t *testing.T)
	db := testDB(t)
	RecordResult(db, "s", true)
	RecordResult(db, "s", true)
	RecordResult(db, "s", false)
	r, _ := GetRating(db, "s")
	if r.Successes != 2 || r.Failures != 1 {
		t.Errorf("expected 2/1, got %d/%d", r.Successes, r.Failures)
	}
	score := RatingScore(r)
	if score < 0.3 || score > 0.4 {
		t.Errorf("score = %v", score)
	}
// §foot page/pkg/comp/rating_test.go TestRecordResultMixed