// §head page/pkg/webhook/fuzz_test.go:16-48 FuzzHandler
// §sig func FuzzHandler(f *testing.F)
	f.Add("/hook/s3cret", []byte(`{"a":1}`), "key-1")
	f.Add("/hook/wrong", []byte(`{}`), "")
	f.Add("/hook/", []byte(``), "")
	f.Add("/", []byte(`x`), "k")
	f.Add("/hook/s3cret/extra", []byte(`{}`), "k")
	f.Add("/hook/%00", []byte("\x00\xff"), "\x00")

	f.Fuzz(func(t *testing.T, path string, body []byte, ikey string) {
		if !isValidRequestTarget(path) {
			t.Skip()
		}

		delivered := 0
		w := New("s3cret", 0)
		w.Faults = nil
		w.deliver = func(adapter.InMessage) { delivered++ }

		req := httptest.NewRequest(http.MethodPost, path, bytes.NewReader(body))
		if ikey != "" && isValidHeaderValue(ikey) {
			req.Header.Set("X-Idempotency-Key", ikey)
		}
		rec := httptest.NewRecorder()
		w.Handler().ServeHTTP(rec, req)

		if rec.Code == http.StatusOK && path != "/hook/s3cret" && delivered > 0 {
			t.Fatalf("delivered on path %q with code 200 — secret bypassed", path)
		}
		if rec.Code != http.StatusOK && delivered > 0 {
			t.Fatalf("delivered %d times despite status %d on %q", delivered, rec.Code, path)
		}
	})
// §foot page/pkg/webhook/fuzz_test.go FuzzHandler