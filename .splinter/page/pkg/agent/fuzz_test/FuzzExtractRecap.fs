// §head page/pkg/agent/fuzz_test.go:7-28 FuzzExtractRecap
// §sig func FuzzExtractRecap(f *testing.F)
	f.Add("plain text")
	f.Add("SUMMARY: done")
	f.Add("line\nSUMMARY: done\nmore")
	f.Add("SUMMARY:")
	f.Add("")
	f.Add("SUMMARY: \n\n\nSUMMARY: second")
	f.Add("\n\n\n")

	f.Fuzz(func(t *testing.T, content string) {
		recap, rest := extractRecap(content)

		if len(recap)+len(rest) > len(content)+len(recapPrefix) {
			t.Fatalf("extractRecap grew input: %d + %d from %d", len(recap), len(rest), len(content))
		}
		// A second pass must be stable: nothing left to extract.
		recap2, rest2 := extractRecap(rest)
		if recap2 != "" && rest2 == rest {
			t.Fatalf("extractRecap not idempotent: still found %q", recap2)
		}
	})
// §foot page/pkg/agent/fuzz_test.go FuzzExtractRecap