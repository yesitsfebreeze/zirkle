// §head page/cmd/relay/main.go:828-879 runSessionsCmd
// §sig func runSessionsCmd(args []string)
	dbPath := filepath.Join(dataDir(), "relay.db")
	s, err := store.Open(dbPath)
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}

	pods, err := s.List()
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
	if len(pods) == 0 {
		fmt.Println("no pods in this workspace")
		return
	}

	fmt.Printf("%-20s %-8s %-10s %s\n", "ID", "STATE", "CREATED", "PROMPT")
	for _, o := range pods {
		p := truncStr(o.Prompt, 40)
		fmt.Printf("%-20s %-8s %-10s %s\n", o.ID, o.State, o.CreatedAt.Format("01-02 15:04"), p)
		if o.Recap != "" {
			fmt.Printf("  recap: %s\n", truncStr(o.Recap, 70))
		}
	}

	fmt.Println("\n--- subpod runs ---")
	execs, err := s.RecentExecutions(50)
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
	if len(execs) == 0 {
		fmt.Println("no subpod runs recorded")
		return
	}
	fmt.Printf("%-8s %-10s %-8s %-22s %s\n", "OK", "TIME", "TOK", "MODEL", "PROMPT")
	ok, fail := 0, 0
	for _, e := range execs {
		status := "ok"
		if !e.Success {
			status = "FAIL"
			fail++
		} else {
			ok++
		}
		fmt.Printf("%-8s %-10s %-8d %-22s %s\n",
			status, e.CreatedAt.Format("01-02 15:04"), e.Tokens, e.Model, truncStr(e.Prompt, 30))
	}
	fmt.Printf("\n%d ok, %d fail across %d runs\n", ok, fail, len(execs))
// §foot page/cmd/relay/main.go runSessionsCmd