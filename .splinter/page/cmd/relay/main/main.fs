// §head page/cmd/relay/main.go:197-251 main
// §sig func main()
	devMode := flag.Bool("dev", false, "enable hot reload dev mode (watches source files)")
	debug := flag.Bool("debug", false, "enable debug logging")
	socketPath := flag.String("socket", cli.DefaultSocketPath, "daemon unix socket path")
	isSubagent := flag.Bool("subagent", false, "run as a subagent process")
	parent := flag.String("parent", "", "parent pod id (subagent mode)")
	task := flag.String("task", "", "task prompt (subagent mode)")
	model := flag.String("model", "", "model override; empty = provider default (glm-5.2:cloud for ollama). RELAY_MODEL")
	maxTokens := flag.Int("max-tokens", 0, "token budget (subagent mode)")
	whSecret := flag.String("webhook-secret", os.Getenv("RELAY_WEBHOOK_SECRET"), "webhook shared secret; empty disables the listener")
	whPort := flag.Int("webhook-port", envPort("RELAY_WEBHOOK_PORT", 9842), "webhook listener port")
	provider := flag.String("provider", "", "llm provider: ollama (default) | anthropic; RELAY_LLM_PROVIDER")
	sandboxMode := flag.String("sandbox", os.Getenv(subagent.EnvSandbox),
		"subagent confinement: on (default) | off to run unconfined on this machine; "+subagent.EnvSandbox)
	flag.Parse()

	if (*devMode || os.Getenv("RELAY_DEV") == "1") && !hotreload.IsDevChild() {
		os.Setenv("RELAY_DEV", "1")
		if err := hotreload.Supervise(os.Args[1:]); err != nil {
			fmt.Fprintf(os.Stderr, "relay dev supervisor: %v\n", err)
			os.Exit(1)
		}
		return
	}

	// The flag is the front door and the env var is the transport: setting
	// it here means a subagent inherits the same policy without every
	// callsite having to pass it down.
	if *sandboxMode != "" {
		os.Setenv(subagent.EnvSandbox, *sandboxMode)
	}

	if *debug {
		log.SetFlags(log.Ltime | log.Lshortfile)
	} else {
		log.SetOutput(io.Discard)
	}

	// Subagent mode — fresh process, writes Result JSON to fd 3, exits
	if *isSubagent {
		subagent.RunSubagent(*parent, *task, *model, *maxTokens)
		return
	}

	args := flag.Args()

	// CLI subcommand mode — connect to running daemon
	if len(args) > 0 {
		runCLI(args, *socketPath, *provider, *model)
		return
	}

	// Daemon mode — open store, start socket listener, run TUI
	runDaemon(*socketPath, *whSecret, *whPort, *provider, *model)
// §foot page/cmd/relay/main.go main