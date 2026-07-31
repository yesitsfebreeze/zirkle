// §head page/cmd/relay/main.go:693-762 runShardCmd
// §sig func runShardCmd(args []string)
	if len(args) < 1 {
		shardUsage()
	}
	sub := args[0]
	if sub == "run" {
		runShardRun(args[1:])
		return
	}

	db := openCompDB()
	defer db.Close()
	s := comp.Open(db)
	if err := s.EnsureSchema(); err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}

	switch sub {
	case "search":
		if len(args) < 2 {
			shardUsage()
		}
		query := strings.Join(args[1:], " ")
		rows, err := s.Search(query)
		if err != nil {
			fmt.Fprintf(os.Stderr, "relay: %v\n", err)
			os.Exit(1)
		}
		for _, sh := range comp.Rank(rows, query) {
			fmt.Printf("%s [%s] — %s\n", sh.Name, sh.Kind, sh.Description)
		}
	case "show":
		if len(args) < 2 {
			shardUsage()
		}
		sh := resolveShard(s, args[1])
		fmt.Printf("%s [%s]\n%s\n", sh.Name, sh.Kind, sh.Body)
	case "list":
		all, err := s.All()
		if err != nil {
			fmt.Fprintf(os.Stderr, "relay: %v\n", err)
			os.Exit(1)
		}
		for _, sh := range all {
			fmt.Printf("%s [%s] — %s\n", sh.Name, sh.Kind, sh.Description)
		}
	case "index":
		if len(args) < 2 {
			shardUsage()
		}
		data, err := os.ReadFile(args[1])
		if err != nil {
			fmt.Fprintf(os.Stderr, "relay: %v\n", err)
			os.Exit(1)
		}
		sh, err := comp.Parse(args[1], string(data))
		if err != nil {
			fmt.Fprintf(os.Stderr, "relay: %v\n", err)
			os.Exit(1)
		}
		if err := s.Index(sh); err != nil {
			fmt.Fprintf(os.Stderr, "relay: %v\n", err)
			os.Exit(1)
		}
		fmt.Printf("indexed %s\n", sh.Name)
	default:
		shardUsage()
	}
// §foot page/cmd/relay/main.go runShardCmd