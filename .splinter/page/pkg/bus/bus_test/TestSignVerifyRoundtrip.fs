// §head page/pkg/bus/bus_test.go:23-39 TestSignVerifyRoundtrip
// §sig func TestSignVerifyRoundtrip(t *testing.T)
	id, err := GenerateIdentity()
	if err != nil {
		t.Fatal(err)
	}

	data := []byte("hello, amp local bus")
	sig := id.Sign(data)
	if !id.Verify(data, sig) {
		t.Fatal("signature verification failed")
	}

	// Tampered data must fail.
	if id.Verify([]byte("tampered"), sig) {
		t.Fatal("tampered data should not verify")
	}
// §foot page/pkg/bus/bus_test.go TestSignVerifyRoundtrip