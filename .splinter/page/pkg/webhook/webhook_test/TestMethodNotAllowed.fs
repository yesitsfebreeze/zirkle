// §head page/pkg/webhook/webhook_test.go:161-177 TestMethodNotAllowed
// §sig func TestMethodNotAllowed(t *testing.T)
	w := New("secret123", 0)
	w.deliver = func(msg adapter.InMessage) {}

	ts := httptest.NewServer(w.Handler())
	defer ts.Close()

	resp, err := http.Get(ts.URL + "/hook/secret123")
	if err != nil {
		t.Fatal(err)
	}
	defer resp.Body.Close()

	if resp.StatusCode != http.StatusMethodNotAllowed {
		t.Fatalf("expected 405, got %d", resp.StatusCode)
	}
// §foot page/pkg/webhook/webhook_test.go TestMethodNotAllowed