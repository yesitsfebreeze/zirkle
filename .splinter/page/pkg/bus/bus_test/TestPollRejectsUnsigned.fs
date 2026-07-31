// §head page/pkg/bus/bus_test.go:139-167 TestPollRejectsUnsigned
// §sig func TestPollRejectsUnsigned(t *testing.T)
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

	bobBus := New(bob, spool)

	// Write an unsigned envelope directly.
	raw := `{"from":"` + alice.Fingerprint() + `","to":"` + bob.Fingerprint() + `","subject":"unsigned","payload":"bm8gc2lnbmF0dXJl"}`
	inbox := bobBus.inbox
	os.MkdirAll(inbox, 0700)                                              //nolint:errcheck
	os.WriteFile(filepath.Join(inbox, "unsigned.env"), []byte(raw), 0600) //nolint:errcheck

	envs, err := bobBus.Poll()
	if err != nil {
		t.Fatalf("Poll: %v", err)
	}
	if len(envs) != 0 {
		t.Fatalf("expected 0 envelopes (unsigned rejected), got %d", len(envs))
	}
// §foot page/pkg/bus/bus_test.go TestPollRejectsUnsigned