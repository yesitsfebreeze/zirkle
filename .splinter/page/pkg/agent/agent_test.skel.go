// §source page/pkg/agent/agent_test.go
package agent

import (
	"context"
	"io"
	"net/http"
	"net/http/httptest"
	"os"
	"path/filepath"
	"strings"
	"testing"

	"github.com/feb/relay/pkg/llm"
	"github.com/feb/relay/pkg/store"
	"github.com/feb/relay/pkg/subagent"
)

type fakeLLM struct{ reply string }

func (f *fakeLLM) Chat(ctx context.Context, req llm.ChatRequest) (*llm.ChatResponse, error) {
// §.splinter/page/pkg/agent/agent_test/fakeLLM.Chat.fs
}

func (f *fakeLLM) ChatStream(ctx context.Context, req llm.ChatRequest) <-chan llm.StreamEvent {
// §.splinter/page/pkg/agent/agent_test/fakeLLM.ChatStream.fs
}

// toolLLM returns a tool call on the first turn, then a text reply on the
// second. This exercises the agent's tool-call → tool-result → continue loop.
type toolLLM struct{ turn int }

func (f *toolLLM) Chat(ctx context.Context, req llm.ChatRequest) (*llm.ChatResponse, error) {
// §.splinter/page/pkg/agent/agent_test/toolLLM.Chat.fs
}

func (f *toolLLM) ChatStream(ctx context.Context, req llm.ChatRequest) <-chan llm.StreamEvent {
// §.splinter/page/pkg/agent/agent_test/toolLLM.ChatStream.fs
}

func TestAgentRunSingleTurn(t *testing.T) {
// §.splinter/page/pkg/agent/agent_test/TestAgentRunSingleTurn.fs
}

// TestProvisionRejectsBadConfig is the ponytail check for the provision/start
// split: a nil LLM or empty prompt fails at Provision before the loop could
// nil-deref on a.LLM.Chat. Fails if the guard ever regresses.
func TestProvisionRejectsBadConfig(t *testing.T) {
// §.splinter/page/pkg/agent/agent_test/TestProvisionRejectsBadConfig.fs
}

// TestMain intercepts --subagent so this test binary can act as the spawned
// subagent process (same trick as pkg/subagent's TestMain).
func TestMain(m *testing.M) {
// §.splinter/page/pkg/agent/agent_test/TestMain.fs
}

// handleToolCall is the tool-use replacement for the old SPAWN: text scanner.
// This drives the real spawn path and fails if the deadline is ever wrong
// again (handleSpawn once set Timeout: 60 — 60 nanoseconds, not 60 seconds).
func TestHandleToolCallSpawn(t *testing.T) {
// §.splinter/page/pkg/agent/agent_test/TestHandleToolCallSpawn.fs
}

func TestHandleToolCallRejectsEmptyPrompt(t *testing.T) {
// §.splinter/page/pkg/agent/agent_test/TestHandleToolCallRejectsEmptyPrompt.fs
}

func TestHandleToolCallRejectsUnknownTool(t *testing.T) {
// §.splinter/page/pkg/agent/agent_test/TestHandleToolCallRejectsUnknownTool.fs
}

// TestAgentRunToolCallLoop drives the full loop: first turn the fake LLM
// calls the spawn tool, the agent executes it (canned result), injects the
// tool-result, second turn the LLM returns final text.
func TestAgentRunToolCallLoop(t *testing.T) {
// §.splinter/page/pkg/agent/agent_test/TestAgentRunToolCallLoop.fs
}

// TestPodOverOllamaEndToEnd drives the whole path an `relay run` takes —
// llm.Ollama -> agent loop -> SUMMARY recap -> store checkpoint — against a
// stub Ollama daemon. It answers "is the system itself working" without
// needing a real model pulled.
func TestPodOverOllamaEndToEnd(t *testing.T) {
// §.splinter/page/pkg/agent/agent_test/TestPodOverOllamaEndToEnd.fs
}