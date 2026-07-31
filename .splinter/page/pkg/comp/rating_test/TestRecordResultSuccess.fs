// §head page/pkg/comp/rating_test.go:23-38 TestRecordResultSuccess
// §sig func TestRecordResultSuccess(t *testing.T)
	db := testDB(t)
	if err := RecordResult(db, "shards/x.shard", true); err != nil {
		t.Fatal(err)
	}
	r, err := GetRating(db, "shards/x.shard")
	if err != nil {
		t.Fatal(err)
	}
	if r.Successes != 1 || r.Failures != 0 {
		t.Errorf("expected 1/0, got %d/%d", r.Successes, r.Failures)
	}
	if RatingScore(r) != 1.0 {
		t.Errorf("score = %v", RatingScore(r))
	}
// §foot page/pkg/comp/rating_test.go TestRecordResultSuccess