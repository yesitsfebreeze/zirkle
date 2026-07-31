// §head page/pkg/webhook/webhook_test.go:90-149 TestIdempotency
// §sig func TestIdempotency(t *testing.T)
	w := New("secret123", 0)

	var calls int32
	w.deliver = func(msg adapter.InMessage) {
		atomic.AddInt32(&calls, 1)
	}

	ts := httptest.NewServer(w.Handler())
	defer ts.Close()

	url := ts.URL + "/hook/secret123"

	// First call with idempotency key.
	req1, err := http.NewRequest("POST", url, bytes.NewReader([]byte("first")))
	if err != nil {
		t.Fatal(err)
	}
	req1.Header.Set("X-Idempotency-Key", "key-001")

	resp1, err := http.DefaultClient.Do(req1)
	if err != nil {
		t.Fatal(err)
	}
	resp1.Body.Close()

	if resp1.StatusCode != http.StatusOK {
		t.Fatalf("first call expected 200, got %d", resp1.StatusCode)
	}

	// Second call with the SAME idempotency key.
	req2, err := http.NewRequest("POST", url, bytes.NewReader([]byte("second")))
	if err != nil {
		t.Fatal(err)
	}
	req2.Header.Set("X-Idempotency-Key", "key-001")

	resp2, err := http.DefaultClient.Do(req2)
	if err != nil {
		t.Fatal(err)
	}
	defer resp2.Body.Close()

	if resp2.StatusCode != http.StatusOK {
		t.Fatalf("second call expected 200, got %d", resp2.StatusCode)
	}

	var body map[string]string
	if err := json.NewDecoder(resp2.Body).Decode(&body); err != nil {
		t.Fatal(err)
	}
	if body["status"] != "skipped" {
		t.Fatalf("expected status=skipped on duplicate, got %v", body)
	}

	// deliver should have been called exactly once.
	if n := atomic.LoadInt32(&calls); n != 1 {
		t.Fatalf("expected 1 deliver call, got %d", n)
	}
// §foot page/pkg/webhook/webhook_test.go TestIdempotency