// §head page/pkg/subagent/subagent_test.go:12-31 TestMain
// §sig func TestMain(m *testing.M)
	for _, arg := range os.Args[1:] {
		if arg == "--subagent" {
			// RELAY_SUBAGENT_SLEEP makes the stand-in subagent outlive
			// its deadline so timeout handling can be exercised.
			if d := os.Getenv("RELAY_SUBAGENT_SLEEP"); d != "" {
				if wait, err := time.ParseDuration(d); err == nil {
					time.Sleep(wait)
				}
				os.Exit(0)
			}
			// The parent test sets RELAY_SUBAGENT_RUN=1, which tells
			// RunSubagent to use a canned result instead of calling a
			// real LLM.
			RunSubagent("", "", "", 0)
			return
		}
	}
	os.Exit(m.Run())
// §foot page/pkg/subagent/subagent_test.go TestMain