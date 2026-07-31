// §head page/pkg/sandbox/seatbelt_test.go:52-61 TestGenerateSBPLNetAllowed
// §sig func TestGenerateSBPLNetAllowed(t *testing.T)
	s := Spec{Dir: "/x", Net: true}
	got := GenerateSBPL(s)
	if !strings.Contains(got, "(allow network*)") {
		t.Fatalf("missing network allow when Net=true:\n%s", got)
	}
	if strings.Contains(got, "(deny network*)") {
		t.Fatalf("unexpected network deny when Net=true:\n%s", got)
	}
// §foot page/pkg/sandbox/seatbelt_test.go TestGenerateSBPLNetAllowed