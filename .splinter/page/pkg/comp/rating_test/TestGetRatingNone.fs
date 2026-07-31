// §head page/pkg/comp/rating_test.go:69-81 TestGetRatingNone
// §sig func TestGetRatingNone(t *testing.T)
	db := testDB(t)
	r, err := GetRating(db, "nonexistent")
	if err != nil {
		t.Fatal(err)
	}
	if r.Successes != 0 || r.Failures != 0 {
		t.Errorf("expected zero rating, got %+v", r)
	}
	if RatingScore(r) != 0 {
		t.Errorf("score should be 0, got %v", RatingScore(r))
	}
// §foot page/pkg/comp/rating_test.go TestGetRatingNone