// §head page/pkg/store/store_test.go:57-102 TestConversationLifecycle
// §sig func TestConversationLifecycle(t *testing.T)
	s, err := Open(filepath.Join(t.TempDir(), "conv.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer s.Close()

	rec := &ConversationRecord{
		ID:           "c1",
		State:        "planning",
		Intent:       `{"prompt":"do task"}`,
		ApprovedPlan: "{}",
		WorkerID:     "w1",
		Recap:        "planning task",
		Output:       "",
		History:      "[]",
		CreatedAt:    time.Now(),
		UpdatedAt:    time.Now(),
	}

	if err := s.SaveConversation(rec); err != nil {
		t.Fatalf("SaveConversation failed: %v", err)
	}

	got, err := s.LoadConversation("c1")
	if err != nil {
		t.Fatalf("LoadConversation failed: %v", err)
	}
	if got.ID != "c1" || got.State != "planning" || got.WorkerID != "w1" {
		t.Fatalf("loaded unexpected conversation: %+v", got)
	}

	list, err := s.ListConversations()
	if err != nil || len(list) != 1 {
		t.Fatalf("ListConversations failed, len = %d: %v", len(list), err)
	}

	if err := s.DeleteConversation("c1"); err != nil {
		t.Fatalf("DeleteConversation failed: %v", err)
	}

	listAfter, _ := s.ListConversations()
	if len(listAfter) != 0 {
		t.Fatalf("expected 0 conversations after delete, got %d", len(listAfter))
	}
// §foot page/pkg/store/store_test.go TestConversationLifecycle