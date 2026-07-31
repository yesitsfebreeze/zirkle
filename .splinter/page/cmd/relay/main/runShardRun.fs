// §head page/cmd/relay/main.go:890-913 runShardRun
// §sig func runShardRun(args []string)
	if len(args) < 1 {
		shardUsage()
	}
	name := args[0]
	shardArgs := args[1:]
	db := openCompDB()
	defer db.Close()
	s := comp.Open(db)
	if err := s.EnsureSchema(); err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
	shard := resolveShard(s, name)
	output, code, err := comp.Dispatch(shard, nil, shardArgs)
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: dispatch: %v\n", err)
		os.Exit(1)
	}
	fmt.Print(output)
	if code != 0 {
		os.Exit(code)
	}
// §foot page/cmd/relay/main.go runShardRun