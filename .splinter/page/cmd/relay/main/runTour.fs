// §head page/cmd/relay/main.go:972-987 runTour
// §sig func runTour()
	kmPath, err := keymap.DefaultPath()
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: keymap: %v\n", err)
		os.Exit(1)
	}
	km, err := keymap.Load(kmPath)
	if err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
	if _, err := tui.RunTour(km, kmPath); err != nil {
		fmt.Fprintf(os.Stderr, "relay: %v\n", err)
		os.Exit(1)
	}
// §foot page/cmd/relay/main.go runTour