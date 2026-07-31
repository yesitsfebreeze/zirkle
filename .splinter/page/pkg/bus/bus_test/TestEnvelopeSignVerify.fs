// §head page/pkg/bus/bus_test.go:41-75 TestEnvelopeSignVerify
// §sig func TestEnvelopeSignVerify(t *testing.T)
	id, err := GenerateIdentity()
	if err != nil {
		t.Fatal(err)
	}

	env := Envelope{
		From:    id.Fingerprint(),
		To:      "recipient-fingerprint",
		Subject: "test",
		Payload: []byte("message body"),
	}

	if err := env.Sign(id); err != nil {
		t.Fatalf("Sign: %v", err)
	}
	if env.Signature == "" {
		t.Fatal("signature is empty after Sign")
	}

	ok, err := env.Verify()
	if err != nil {
		t.Fatalf("Verify error: %v", err)
	}
	if !ok {
		t.Fatal("valid envelope did not verify")
	}

	// Tamper signature.
	env.Signature = "AAAA"
	ok, err = env.Verify()
	if err == nil && ok {
		t.Fatal("tampered signature should not verify")
	}
// §foot page/pkg/bus/bus_test.go TestEnvelopeSignVerify