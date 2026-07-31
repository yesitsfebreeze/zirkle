// §head page/pkg/webhook/webhook_test.go:260-277 TestDeliverPanicDoesNotHangHandler
// §sig func TestDeliverPanicDoesNotHangHandler(t *testing.T)
	w := New("s3cret", 0)
	w.Faults = &panicSink{}
	w.deliver = func(adapter.InMessage) { panic("boom") }

	done := make(chan struct{})
	go func() {
		defer close(done)
		req := httptest.NewRequest(http.MethodPost, "/hook/s3cret", strings.NewReader(`{}`))
		w.Handler().ServeHTTP(httptest.NewRecorder(), req)
	}()

	select {
	case <-done:
	case <-time.After(3 * time.Second):
		t.Fatal("handler hung after deliver panicked")
	}
// §foot page/pkg/webhook/webhook_test.go TestDeliverPanicDoesNotHangHandler