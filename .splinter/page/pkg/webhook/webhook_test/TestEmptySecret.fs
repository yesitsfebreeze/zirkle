// §head page/pkg/webhook/webhook_test.go:152-158 TestEmptySecret
// §sig func TestEmptySecret(t *testing.T)
	w := New("", 0)
	err := w.Run(context.Background(), nil)
	if err != nil {
		t.Fatalf("expected nil error for empty secret, got %v", err)
	}
// §foot page/pkg/webhook/webhook_test.go TestEmptySecret