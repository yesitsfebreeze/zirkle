// §head page/pkg/webhook/webhook_test.go:63-87 TestBadSecret
// §sig func TestBadSecret(t *testing.T)
	w := New("secret123", 0)

	var calls int32
	w.deliver = func(msg adapter.InMessage) {
		atomic.AddInt32(&calls, 1)
	}

	ts := httptest.NewServer(w.Handler())
	defer ts.Close()

	resp, err := http.Post(ts.URL+"/hook/wrong-secret", "application/json", bytes.NewReader([]byte("hello")))
	if err != nil {
		t.Fatal(err)
	}
	defer resp.Body.Close()

	if resp.StatusCode != http.StatusUnauthorized {
		t.Fatalf("expected 401, got %d", resp.StatusCode)
	}

	if n := atomic.LoadInt32(&calls); n != 0 {
		t.Fatalf("expected 0 deliver calls, got %d", n)
	}
// §foot page/pkg/webhook/webhook_test.go TestBadSecret