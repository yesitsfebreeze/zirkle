// §head page/cmd/relay/main.go:262-342 runCLI
// §sig func runCLI(args []string, socketPath, provider, model string)
	cmd := args[0]

	// Local commands — no daemon connection needed.
	switch cmd {
	case "init":
		if len(args) < 2 {
			fmt.Fprintln(os.Stderr, "usage: relay init <git-url>")
			os.Exit(1)
		}
		runInit(args[1])
		return
	case "shard":
		runShardCmd(args[1:])
		return
	case "history":
		runHistoryCmd(args[1:])
		return
	case "sessions":
		runSessionsCmd(args[1:])
		return
	case "spawn":
		runSpawnCmd(args[1:], provider, model)
		return
	case "showcase":
		if err := tui.RunShowcase(); err != nil {
			fmt.Fprintln(os.Stderr, "relay:", err)
			os.Exit(1)
		}
		return
	case "tour":
		runTour()
		return
	}

	// Daemon commands — connect to running daemon.
	c := cli.New(socketPath)
	if err := c.Dial(); err != nil {
		fmt.Fprintln(os.Stderr, "relay:", err)
		os.Exit(1)
	}
	defer c.Close()

	switch cmd {
	case "run":
		if len(args) < 2 {
			fmt.Fprintln(os.Stderr, "usage: relay run <prompt>")
			os.Exit(1)
		}
		if err := c.Run(strings.Join(args[1:], " ")); err != nil {
			fmt.Fprintln(os.Stderr, "relay:", err)
			os.Exit(1)
		}
	case "ps":
		if err := c.List(); err != nil {
			fmt.Fprintln(os.Stderr, "relay:", err)
			os.Exit(1)
		}
	case "kill":
		if len(args) < 2 {
			fmt.Fprintln(os.Stderr, "usage: relay kill <id>")
			os.Exit(1)
		}
		if err := c.Kill(args[1]); err != nil {
			fmt.Fprintln(os.Stderr, "relay:", err)
			os.Exit(1)
		}
	case "logs":
		if len(args) < 2 {
			fmt.Fprintln(os.Stderr, "usage: relay logs <id>")
			os.Exit(1)
		}
		if err := c.Logs(args[1]); err != nil {
			fmt.Fprintln(os.Stderr, "relay:", err)
			os.Exit(1)
		}
	default:
		fmt.Fprintf(os.Stderr, "relay: unknown command: %s\n", cmd)
		os.Exit(1)
	}
// §foot page/cmd/relay/main.go runCLI