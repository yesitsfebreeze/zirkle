// §head page/pkg/bus/bus_test.go:9-21 TestGenerateIdentity
// §sig func TestGenerateIdentity(t *testing.T)
	id, err := GenerateIdentity()
	if err != nil {
		t.Fatalf("GenerateIdentity: %v", err)
	}
	if len(id.PublicKey()) != 32 {
		t.Fatalf("expected 32-byte public key, got %d bytes", len(id.PublicKey()))
	}
	fp := id.Fingerprint()
	if len(fp) != 64 {
		t.Fatalf("expected 64-char hex fingerprint, got %d chars", len(fp))
	}
// §foot page/pkg/bus/bus_test.go TestGenerateIdentity