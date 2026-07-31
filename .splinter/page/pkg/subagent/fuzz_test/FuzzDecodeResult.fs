// §head page/pkg/subagent/fuzz_test.go:9-24 FuzzDecodeResult
// §sig func FuzzDecodeResult(f *testing.F)
	f.Add([]byte(`{"success":true,"summary":"ok","output":"x","tokens":5}`))
	f.Add([]byte(`{"success":false}`))
	f.Add([]byte(`garbage`))
	f.Add([]byte(``))
	f.Add([]byte(`{`))
	f.Add([]byte("banner noise\n{\"success\":true}"))
	f.Add([]byte(`{"tokens":-9223372036854775808}`))

	f.Fuzz(func(t *testing.T, b []byte) {
		res, err := decodeResult(b)
		if err == nil && res == nil {
			t.Fatal("decodeResult returned nil result with nil error")
		}
	})
// §foot page/pkg/subagent/fuzz_test.go FuzzDecodeResult