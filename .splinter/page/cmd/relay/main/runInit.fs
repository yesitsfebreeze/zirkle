// §head page/cmd/relay/main.go:666-691 runInit
// §sig func runInit(gitURL string)
	base := filepath.Base(gitURL)
	base = strings.TrimSuffix(base, ".git")
	dest := filepath.Join(compsDir(), base)
	os.MkdirAll(compsDir(), 0o755)
	clone := exec.Command("git", "clone", gitURL, dest)
	clone.Stdout = os.Stdout
	clone.Stderr = os.Stderr
	if err := clone.Run(); err != nil {
		fmt.Fprintf(os.Stderr, "relay: git clone: %v\n", err)
		os.Exit(1)
	}
	db := openCompDB()
	defer db.Close()
	s := comp.Open(db)
	if err := s.EnsureSchema(); err != nil {
		fmt.Fprintf(os.Stderr, "relay: schema: %v\n", err)
		os.Exit(1)
	}
	if _, err := comp.LoadComp(dest, s); err != nil {
		fmt.Fprintf(os.Stderr, "relay: load: %v\n", err)
		os.Exit(1)
	}
	all, _ := s.All()
	fmt.Printf("cloned %s, %d shards indexed\n", base, len(all))
// §foot page/cmd/relay/main.go runInit