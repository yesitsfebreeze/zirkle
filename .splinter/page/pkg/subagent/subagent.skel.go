// §source page/pkg/subagent/subagent.go
package subagent

import (
	"context"
	"encoding/json"
	"fmt"
	"os"
	"runtime"
	"strconv"
	"time"

	"github.com/feb/relay/pkg/llm"
	"github.com/feb/relay/pkg/sandbox"
)

// Config describes a subagent to spawn.
type Config struct {
	Prompt    string
	ParentID  string
	Timeout   time.Duration // default 60s
	Model     string        // empty = same as parent
	MaxTokens int           // default 0 (no limit passed)
	Executor  Executor      // nil = Local

	// ToolOptional lifts the requirement that the subpod call shell before
	// answering. Default (zero) is tool-required: a subpod must do real work.
	// Set true for research/reasoning-only pods that may answer from context.
	ToolOptional bool
}

// subpodPrompt is the system prompt injected into every subpod LLM call.
// The subpod is a single-purpose worker: run pod commands, return results.
const subpodPrompt = `You are a pod worker. Run commands, return results.

## Discovery loop

For every task:
1. SEARCH: relay shard search "<need>" — find matching pods
2. READ:   relay shard show "<name>"   — read prose before executing
3. DISPATCH: just runs the recipe — the pod's executable command
4. CHECK: inspect output, exit code
5. RECAP: end with SUMMARY: <one-line result>

## Pod format

Pods are markdown files with frontmatter + prose + just recipes.
Frontmatter fields: name, kind (tool|knowledge|workflow), description,
purpose, tags, use_when, not_when, danger, side_effects, requires, category, run.
The run field names the default recipe; omit for first recipe.
just recipes are fenced ` + "````just" + ` blocks extracted and executed via just.

## Tools

You have ONE tool: shell(command). It runs any shell command via /bin/sh -c
and returns stdout+stderr+exit status. Use it for everything:

- shell("ps aux")              — list processes
- shell("ls -la")              — inspect files
- shell("curl -s http://...")   — fetch from network
- shell("relay shard search \"<q>\"")  — find matching pods
- shell("relay shard show <name>")      — read pod prose + recipes
- shell("relay shard list")              — list all loaded pods
- shell("relay shard index <path>")      — index a new pod
- shell("relay history search \"<q>\"") — search past subpod runs
- shell("just <recipe>")               — dispatch pod recipes

Run the command, read the output, decide next step. Repeat until done.

## Rules

- Never dispatch blind — read the pod first
- Check requires before running — missing deps fail loud
- Veto via not_when — zeroes search score
- End every response with SUMMARY: <one-line result>
- Summary is stripped from output, stored as agent recap

## Failure protocol

1. Read pod prose — missed prerequisite?
2. Check requires — all deps installed?
3. Read error — fix root cause
4. Re-dispatch
5. Still failing → write fix or escalate`

// Result is written to fd 3 by the subagent and read by Spawn.
type Result struct {
	Success bool   `json:"success"`
	Summary string `json:"summary"`
	Output  string `json:"output"`
	Tokens  int    `json:"tokens"`
}

// Spawn runs a subagent and returns its Result.  Where it runs is the
// executor's business: cfg.Executor decides, and nil defers to
// DefaultExecutor — confined unless RELAY_SANDBOX says otherwise.  Running
// unconfined is therefore something a caller names, never something it drifts
// into.
func Spawn(ctx context.Context, cfg Config) (*Result, error) {
// §.splinter/page/pkg/subagent/subagent/Spawn.fs
}

// RunInline runs the subpod loop in-process — no subprocess, no bwrap, no
// binary re-exec.  The subpod gets a fresh LLM and its own message history
// (zero context bleed), runs the tool loop (shell + pod commands), and
// returns.  Use this when isolation is not required; Spawn when it is.
func RunInline(ctx context.Context, cfg Config) (*Result, error) {
// §.splinter/page/pkg/subagent/subagent/RunInline.fs
}

// RunSubagent is the entry point for a subagent process.  It parses its own
// flags from os.Args (because in test mode the test binary doesn't go through
// cmd/relay's flag.Parse), runs one LLM call, writes Result JSON to fd 3, and
// exits.  In test mode (RELAY_SUBAGENT_RUN=1) it writes a canned result.
func RunSubagent(parentID, task, model string, maxTokens int) {
// §.splinter/page/pkg/subagent/subagent/RunSubagent.fs
}

// writeResult writes a Result as JSON to file descriptor 3 (the pipe set up by
// a Local parent).  fd 3 does not survive an ssh hop, so a remote parent sets
// RELAY_RESULT_STDOUT=1 and the Result goes to stdout instead; stderr stays the
// log channel either way.
func writeResult(r Result) {
// §.splinter/page/pkg/subagent/subagent/writeResult.fs
}
