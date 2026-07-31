// §head page/cmd/relay/main.go:791-823 runHistoryCmd
// §sig func runHistoryCmd(args []string)
	query := ""
	if len(args) > 0 {
		if args[0] == "search" {
			args = args[1:]
		}
		query = strings.Join(args, " ")
	}

	dbPath := filepath.Join(dataDir(), "relay.db")
	s, err := store.Open(dbPath)
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
	execs, err := s.SearchExecutions(query, 20)
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
	if len(execs) == 0 {
		fmt.Println("no executions found")
		return
	}
	for _, e := range execs {
		status := "ok"
		if !e.Success {
			status = "FAIL"
		}
		fmt.Printf("[%s] %s %s\n  → %s\n",
			e.CreatedAt.Format("2006-01-02 15:04"), status, e.Prompt, e.Summary)
	}
// §foot page/cmd/relay/main.go runHistoryCmd