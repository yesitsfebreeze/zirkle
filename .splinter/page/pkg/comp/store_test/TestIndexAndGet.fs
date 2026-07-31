// §head page/pkg/comp/store_test.go:24-64 TestIndexAndGet
// §sig func TestIndexAndGet(t *testing.T)
	s := testStore(t)
	shard := &Shard{
		Key:         "shards/check-ci.shard",
		Name:        "check-ci",
		Kind:        "tool",
		Description: "Check CI pipeline status",
		Tags:        []string{"ci", "check"},
		UseWhen:     "CI build is failing",
		NotWhen:     "no CI configured",
		Danger:      "none",
		Requires:    []string{"gh", "jq"},
		Run:         "check-ci",
		HasFM:       true,
		Body:        "Check CI pipeline for failures.",
		Justfile:    "[unix]\ncheck-ci:\n    gh run list",
	}
	if err := s.Index(shard); err != nil {
		t.Fatal(err)
	}

	got, err := s.Get("shards/check-ci.shard")
	if err != nil {
		t.Fatal(err)
	}
	if got.Name != "check-ci" {
		t.Errorf("Name = %q", got.Name)
	}
	if len(got.Tags) != 2 || got.Tags[0] != "ci" {
		t.Errorf("Tags = %v", got.Tags)
	}
	if len(got.Requires) != 2 || got.Requires[0] != "gh" {
		t.Errorf("Requires = %v", got.Requires)
	}
	if !got.HasFM {
		t.Error("HasFM should be true")
	}
	if got.Justfile == "" {
		t.Error("Justfile should not be empty")
	}
// §foot page/pkg/comp/store_test.go TestIndexAndGet