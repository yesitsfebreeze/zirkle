// §head page/pkg/comp/rating_test.go:40-52 TestRecordResultFailure
// §sig func TestRecordResultFailure(t *testing.T)
	db := testDB(t)
	if err := RecordResult(db, "shards/x.shard", false); err != nil {
		t.Fatal(err)
	}
	r, _ := GetRating(db, "shards/x.shard")
	if r.Successes != 0 || r.Failures != 1 {
		t.Errorf("expected 0/1, got %d/%d", r.Successes, r.Failures)
	}
	if RatingScore(r) != -1.0 {
		t.Errorf("score = %v", RatingScore(r))
	}
// §foot page/pkg/comp/rating_test.go TestRecordResultFailure