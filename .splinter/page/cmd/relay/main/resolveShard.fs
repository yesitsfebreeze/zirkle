// §head page/cmd/relay/main.go:771-786 resolveShard
// §sig func resolveShard(s *comp.Store, name string) *comp.Shard
	if sh, err := s.Get(name); err == nil {
		return sh
	}
	rows, err := s.Search(name)
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
	ranked := comp.Rank(rows, name)
	if len(ranked) == 0 {
		fmt.Fprintf(os.Stderr, "relay: no shard matching %q\n", name)
		os.Exit(1)
	}
	return &ranked[0]
// §foot page/cmd/relay/main.go resolveShard