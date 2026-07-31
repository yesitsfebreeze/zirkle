// §head page/pkg/bus/bus_test.go:77-137 TestSendPollAckRoundtrip
// §sig func TestSendPollAckRoundtrip(t *testing.T)
	dir := t.TempDir()
	spool := filepath.Join(dir, "spool")

	alice, err := GenerateIdentity()
	if err != nil {
		t.Fatal(err)
	}
	bob, err := GenerateIdentity()
	if err != nil {
		t.Fatal(err)
	}

	aliceBus := New(alice, spool)
	bobBus := New(bob, spool)

	// Alice sends to Bob.
	env := Envelope{
		Subject:  "hello from alice",
		Priority: "normal",
		Payload:  []byte("meet at the watercooler"),
	}
	if err := aliceBus.Send(bob.Fingerprint(), env); err != nil {
		t.Fatalf("Send: %v", err)
	}

	// Bob polls and receives.
	envs, err := bobBus.Poll()
	if err != nil {
		t.Fatalf("Poll: %v", err)
	}
	if len(envs) != 1 {
		t.Fatalf("expected 1 envelope, got %d", len(envs))
	}

	got := envs[0]
	if got.Subject != "hello from alice" {
		t.Fatalf("expected subject 'hello from alice', got %q", got.Subject)
	}
	if string(got.Payload) != "meet at the watercooler" {
		t.Fatalf("expected payload 'meet at the watercooler', got %q", string(got.Payload))
	}
	if got.From != alice.Fingerprint() {
		t.Fatalf("expected from %q, got %q", alice.Fingerprint(), got.From)
	}
	if got.To != bob.Fingerprint() {
		t.Fatalf("expected to %q, got %q", bob.Fingerprint(), got.To)
	}

	// Ack and verify inbox is empty.
	if err := bobBus.Ack(got.ID); err != nil {
		t.Fatalf("Ack: %v", err)
	}
	envs, err = bobBus.Poll()
	if err != nil {
		t.Fatalf("Poll after ack: %v", err)
	}
	if len(envs) != 0 {
		t.Fatalf("expected 0 envelopes after ack, got %d", len(envs))
	}
// §foot page/pkg/bus/bus_test.go TestSendPollAckRoundtrip