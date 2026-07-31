// §head page/cmd/relay/main.go:915-970 runSpawnCmd
// §sig func runSpawnCmd(args []string, provider, model string)
	if len(args) < 3 || args[0] != "comp" {
		fmt.Fprintln(os.Stderr, "usage: relay spawn comp <name> <prompt>")
		os.Exit(1)
	}
	compName := args[1]
	prompt := strings.Join(args[2:], " ")
	dir := dataDir()
	os.MkdirAll(dir, 0o755)
	dbPath := filepath.Join(dir, "relay.db")
	db, err := sql.Open("sqlite", dbPath)
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
	defer db.Close()
	cs := comp.Open(db)
	if err := cs.EnsureSchema(); err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
	compRoot := filepath.Join(compsDir(), compName)
	compInst, err := comp.LoadComp(compRoot, cs)
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: load comp: %v\n", err)
		os.Exit(1)
	}
	podStore, err := store.Open(dbPath)
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
	defer podStore.Close()
	podID := fmt.Sprintf("pod-%x", time.Now().UnixNano())
	if err := podStore.Create(podID, prompt, "comp:"+compName); err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
	l, err := llm.New(provider, model)
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
	a := &agent.Agent{
		ID: podID, Prompt: prompt, Mode: "comp:" + compName,
		Budget: 100000,
		LLM:    l, Store: podStore,
		Comp: compInst,
	}
	resp, err := a.Run(context.Background())
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
	fmt.Println(resp)
// §foot page/cmd/relay/main.go runSpawnCmd