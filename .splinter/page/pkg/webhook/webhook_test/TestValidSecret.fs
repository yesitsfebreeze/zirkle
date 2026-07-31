// §head page/pkg/webhook/webhook_test.go:19-60 TestValidSecret
// §sig func TestValidSecret(t *testing.T)
	w := New("secret123", 0)

	var calls int32
	w.deliver = func(msg adapter.InMessage) {
		atomic.AddInt32(&calls, 1)
		if msg.Source != "webhook" {
			t.Errorf("expected source webhook, got %s", msg.Source)
		}
		if msg.ChannelID != "secret123" {
			t.Errorf("expected ChannelID secret123, got %s", msg.ChannelID)
		}
		if msg.Prompt != "hello" {
			t.Errorf("expected prompt hello, got %s", msg.Prompt)
		}
	}

	ts := httptest.NewServer(w.Handler())
	defer ts.Close()

	resp, err := http.Post(ts.URL+"/hook/secret123", "application/json", bytes.NewReader([]byte("hello")))
	if err != nil {
		t.Fatal(err)
	}
	defer resp.Body.Close()

	if resp.StatusCode != http.StatusOK {
		t.Fatalf("expected 200, got %d", resp.StatusCode)
	}

	var body map[string]string
	if err := json.NewDecoder(resp.Body).Decode(&body); err != nil {
		t.Fatal(err)
	}
	if body["status"] != "ok" {
		t.Fatalf("expected status ok, got %v", body)
	}

	if n := atomic.LoadInt32(&calls); n != 1 {
		t.Fatalf("expected 1 deliver call, got %d", n)
	}
// §foot page/pkg/webhook/webhook_test.go TestValidSecret