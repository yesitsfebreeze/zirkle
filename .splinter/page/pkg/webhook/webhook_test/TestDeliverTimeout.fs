// §head page/pkg/webhook/webhook_test.go:182-215 TestDeliverTimeout
// §sig func TestDeliverTimeout(t *testing.T)
	w := New("secret123", 0)
	w.timeout = 30 * time.Millisecond

	release := make(chan struct{})
	var calls int32
	w.deliver = func(adapter.InMessage) {
		atomic.AddInt32(&calls, 1)
		<-release
	}

	ts := httptest.NewServer(w.Handler())
	defer ts.Close()

	req, _ := http.NewRequest(http.MethodPost, ts.URL+"/hook/secret123", bytes.NewReader([]byte("slow")))
	req.Header.Set("X-Idempotency-Key", "k-timeout")
	resp, err := http.DefaultClient.Do(req)
	if err != nil {
		t.Fatal(err)
	}
	defer resp.Body.Close()

	if resp.StatusCode != http.StatusRequestTimeout {
		t.Fatalf("status = %d, want 408", resp.StatusCode)
	}
	if w.isDuplicate("k-timeout") {
		t.Error("timed-out key was recorded; a retry would be wrongly deduped")
	}
	close(release)

	if got := atomic.LoadInt32(&calls); got != 1 {
		t.Errorf("deliver called %d times, want 1", got)
	}
// §foot page/pkg/webhook/webhook_test.go TestDeliverTimeout