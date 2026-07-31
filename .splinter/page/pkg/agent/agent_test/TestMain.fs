// §head page/pkg/agent/agent_test.go:140-148 TestMain
// §sig func TestMain(m *testing.M)
	for _, arg := range os.Args[1:] {
		if arg == "--subagent" {
			subagent.RunSubagent("", "", "", 0)
			return
		}
	}
	os.Exit(m.Run())
// §foot page/pkg/agent/agent_test.go TestMain