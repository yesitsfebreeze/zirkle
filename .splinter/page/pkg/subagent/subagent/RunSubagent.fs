// §head page/pkg/subagent/subagent.go:133-203 RunSubagent
// §sig func RunSubagent(parentID, task, model string, maxTokens int)
	// Test mode — write canned result so Spawn tests don't need a real LLM.
	if os.Getenv("RELAY_SUBAGENT_RUN") == "1" {
		writeResult(Result{
			Success: true,
			Summary: "test summary",
			Output:  "test output",
			Tokens:  50,
		})
		os.Exit(0)
	}

	// If task was not passed via main.go (e.g. test binary that didn't
	// register the flags), parse them directly from os.Args.
	if task == "" {
		for i := 1; i < len(os.Args); i++ {
			switch os.Args[i] {
			case "--parent":
				if i+1 < len(os.Args) {
					parentID = os.Args[i+1]
					i++
				}
			case "--task":
				if i+1 < len(os.Args) {
					task = os.Args[i+1]
					i++
				}
			case "--model":
				if i+1 < len(os.Args) {
					model = os.Args[i+1]
					i++
				}
			case "--max-tokens":
				if i+1 < len(os.Args) {
					maxTokens, _ = strconv.Atoi(os.Args[i+1])
					i++
				}
			}
		}
	}

	// Apply Landlock as a second confinement layer inside bwrap.
	// Non-fatal if unavailable — bwrap is still the boundary.
	// Lock the OS thread so the restriction sticks to this thread only.
	if !Unconfined() {
		runtime.LockOSThread()
		if err := sandbox.ApplyLandlock(
			[]string{"/", "/tmp", "/proc", "/dev"},
			[]string{"/tmp"},
		); err != nil {
			fmt.Fprintf(os.Stderr, "subagent: landlock: %v\n", err)
		}
	}

	l, err := llm.New("", model)
	if err != nil {
		writeResult(Result{Success: false, Summary: err.Error(), Output: err.Error()})
		os.Exit(1)
	}

	ctx := context.Background()
	result := runSubpodLoop(ctx, l, model, task, maxTokens, false) // subprocess path: tool required

	writeResult(result)

	if result.Success {
		os.Exit(0)
	} else {
		os.Exit(1)
	}
// §foot page/pkg/subagent/subagent.go RunSubagent