// §head page/pkg/webhook/webhook_test.go:237-257 TestDeliverPanicIsRecorded
// §sig func TestDeliverPanicIsRecorded(t *testing.T)
	sink := &panicSink{}
	w := New("s3cret", 0)
	w.Faults = sink
	w.deliver = func(adapter.InMessage) { panic("deliver exploded") }

	req := httptest.NewRequest(http.MethodPost, "/hook/s3cret", strings.NewReader(`{"a":1}`))
	rec := httptest.NewRecorder()
	w.Handler().ServeHTTP(rec, req)

	if sink.len() != 1 {
		t.Fatalf("want the panic recorded once, got %d records", sink.len())
	}
	got := sink.rows[0]
	if !strings.Contains(got, "panic") || !strings.Contains(got, "webhook.deliver") {
		t.Errorf("record = %q, want a panic at webhook.deliver", got)
	}
	if !strings.Contains(got, "deliver exploded") {
		t.Errorf("record = %q, want the panic message preserved", got)
	}
// §foot page/pkg/webhook/webhook_test.go TestDeliverPanicIsRecorded