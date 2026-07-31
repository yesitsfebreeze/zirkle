// §source page/pkg/agent/agent.go
package agent

import (
	"context"
	"encoding/json"
	"fmt"
	"strings"

	"github.com/feb/relay/pkg/comp"
	"github.com/feb/relay/pkg/llm"
	"github.com/feb/relay/pkg/store"
	"github.com/feb/relay/pkg/subagent"
)

const recapPrefix = "SUMMARY:"

// spawnTool is the LLM tool-use schema for subagent spawning. The model calls
// this tool instead of emitting SPAWN: text lines — real tool-use protocol,
// not a text-line convention.
var spawnTool = llm.Tool{
	Name:        "spawn",
	Description: "Fork a subpod that has shell + pod tools. The subpod runs commands and returns results.",
	InputSchema: map[string]any{
		"type": "object",
		"properties": map[string]any{
			"prompt": map[string]any{
				"type":        "string",
				"description": "The task prompt for the subagent.",
			},
		},
		"required": []string{"prompt"},
	},
}

// extractRecap pulls a one-line SUMMARY from the end of content, returning
// (recap, cleanedContent). Falls back to the first non-empty line when no
// SUMMARY line is found. Returns empty recap only when content is empty.
func extractRecap(content string) (string, string) {
// §.splinter/page/pkg/agent/agent/extractRecap.fs
}

const defaultBudget = 100000

// handleToolCall executes a tool call from the LLM. Currently only "spawn" is
// supported — it spawns a subagent and returns the result summary. Unknown
// tools return an error string so the model can self-correct.
func (a *Agent) handleToolCall(ctx context.Context, call *llm.ToolCall) string {
// §.splinter/page/pkg/agent/agent/Agent.handleToolCall.fs
}

type Agent struct {
	ID     string
	Prompt string
	Mode   string
	Model  string
	Budget int
	LLM    llm.LLM
	Store  store.Store

	Subagents map[string]*subagent.Config // named subagent configs (may be nil)

	Comp      *comp.Composition // optional: composition for warm shard dispatch
	Inventory string            // populated from Comp: list of indexed pods for system prompt

	Recap string // populated by Run — LLM-generated one-line summary

	msgs   []llm.Message
	turn   int
	tokens int
}

// Provision validates prerequisites without side effects or an LLM call. A
// bad config fails here with a clear error instead of a nil-pointer panic
// inside the loop. Budget is defaulted here so Run can assume it is set.
func (a *Agent) Provision() error {
// §.splinter/page/pkg/agent/agent/Agent.Provision.fs
}

func (a *Agent) Run(ctx context.Context) (string, error) {
// §.splinter/page/pkg/agent/agent/Agent.Run.fs
}

// RunStream is the streaming variant of Run. It emits LLM tokens and tool-call
// events through the events channel as they arrive; nil events = collect only,
// no streaming. Returns the final content string (SUMMARY stripped) and error.
func (a *Agent) RunStream(ctx context.Context, events chan<- llm.StreamEvent) (string, error) {
// §.splinter/page/pkg/agent/agent/Agent.RunStream.fs
}