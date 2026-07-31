// §source page/cmd/relay/main.go
package main

import (
	"context"
	"database/sql"
	"encoding/json"
	"flag"
	"fmt"
	"io"
	"log"
	"net"
	"os"
	"os/exec"
	"path/filepath"
	"runtime/debug"
	"strconv"
	"strings"
	"time"

	"github.com/feb/relay/pkg/adapter"
	"github.com/feb/relay/pkg/agent"
	"github.com/feb/relay/pkg/cli"
	"github.com/feb/relay/pkg/comp"
	"github.com/feb/relay/pkg/config"
	"github.com/feb/relay/pkg/fault"
	"github.com/feb/relay/pkg/hotreload"
	"github.com/feb/relay/pkg/keymap"
	"github.com/feb/relay/pkg/llm"
	"github.com/feb/relay/pkg/store"
	"github.com/feb/relay/pkg/subagent"
	"github.com/feb/relay/pkg/tui"
	"github.com/feb/relay/pkg/webhook"
)

type podSource struct{ store store.Store }

var mockTree = []tui.PodView{
	{ID: "nightwatch", Prompt: "monitor infra alerts", Mode: "smart", State: "running", Recap: "Watching alerts — 3 critical, 12 warn", Depth: 0, HasChildren: true},
	{ID: "check-disk", Prompt: "disk usage >80%?", Mode: "quick", State: "running", Recap: "checking /dev/sda1 (85%)", Depth: 1, HasChildren: false},
	{ID: "check-cpu", Prompt: "CPU avg >70%?", Mode: "quick", State: "done", Recap: "CPU at 22% — all clear", Depth: 1, HasChildren: false},
	{ID: "release-bot", Prompt: "tag & deploy staging", Mode: "smart", State: "done", Recap: "v1.4.2 deployed to staging", Depth: 0, HasChildren: true},
	{ID: "build-arm", Prompt: "compile for arm64", Mode: "rush", State: "running", Recap: "cross-compiling — 3 min left", Depth: 1, HasChildren: false},
	{ID: "smoke-test", Prompt: "run smoke suite", Mode: "quick", State: "created", Recap: "awaiting build artifact", Depth: 1, HasChildren: false},
	{ID: "doc-gen", Prompt: "generate API docs", Mode: "quick", State: "failed", Recap: "parse error in schema.yaml", Depth: 1, HasChildren: true},
	{ID: "fix-intro", Prompt: "intro paragraph", Mode: "quick", State: "created", Recap: "in queue", Depth: 2, HasChildren: false},
	{ID: "helpdesk", Prompt: "triage user tickets", Mode: "smart", State: "running", Recap: "3 open — highest: #4291 login failure", Depth: 0, HasChildren: false},
}

func (s *podSource) List() ([]tui.PodView, error) {
// §.splinter/page/cmd/relay/main/podSource.List.fs
}

// Conversation refills the chat pane for a selected pod from its latest saved
// checkpoint. Subpod rows (id "subpod:N") have no conversation of their own.
func (s *podSource) Conversation(id string) ([]tui.ChatMsg, error) {
// §.splinter/page/cmd/relay/main/podSource.Conversation.fs
}

type podCommander struct {
	store store.Store
	llm   llm.LLM
}

func (c *podCommander) Run(ctx context.Context, prompt string) (out string, rerr error) {
// §.splinter/page/cmd/relay/main/podCommander.Run.fs
}

func (c *podCommander) RunStream(ctx context.Context, prompt string, events chan<- llm.StreamEvent) (string, error) {
// §.splinter/page/cmd/relay/main/podCommander.RunStream.fs
}

func (c *podCommander) runWithStream(ctx context.Context, prompt string, events chan<- llm.StreamEvent) (out string, rerr error) {
// §.splinter/page/cmd/relay/main/podCommander.runWithStream.fs
}

func main() {
// §.splinter/page/cmd/relay/main/main.fs
}

func envPort(key string, def int) int {
// §.splinter/page/cmd/relay/main/envPort.fs
}

func runCLI(args []string, socketPath, provider, model string) {
// §.splinter/page/cmd/relay/main/runCLI.fs
}

func runDaemon(socketPath, whSecret string, whPort int, provider, model string) {
// §.splinter/page/cmd/relay/main/runDaemon.fs
}

// ── daemon RPC handler ──────────────────────────────────────────────────────

type rpcRequest struct {
	ID     int            `json:"id"`
	Method string         `json:"method"`
	Params map[string]any `json:"params,omitempty"`
}

type rpcResponse struct {
	ID     int    `json:"id"`
	Result any    `json:"result,omitempty"`
	Error  string `json:"error,omitempty"`
}

type rpcStream struct {
	ID   int    `json:"id"`
	Type string `json:"type"` // "line", "done", "error"
	Data string `json:"data"`
}

func serveDaemon(lis net.Listener, s store.Store, l llm.LLM) {
// §.splinter/page/cmd/relay/main/serveDaemon.fs
}

func handleConn(conn net.Conn, s store.Store, l llm.LLM) {
// §.splinter/page/cmd/relay/main/handleConn.fs
}

func handleRun(conn net.Conn, id int, params map[string]any, s store.Store, l llm.LLM) {
// §.splinter/page/cmd/relay/main/handleRun.fs
}

func handleList(conn net.Conn, id int, s store.Store) {
// §.splinter/page/cmd/relay/main/handleList.fs
}

func handleKill(conn net.Conn, id int, params map[string]any, s store.Store) {
// §.splinter/page/cmd/relay/main/handleKill.fs
}

func handleLogs(conn net.Conn, id int, params map[string]any, s store.Store) {
// §.splinter/page/cmd/relay/main/handleLogs.fs
}

// ── helpers ─────────────────────────────────────────────────────────────────

func sendRPCResult(conn net.Conn, id int, result any) {
// §.splinter/page/cmd/relay/main/sendRPCResult.fs
}

func sendRPCError(conn net.Conn, id int, errMsg string) {
// §.splinter/page/cmd/relay/main/sendRPCError.fs
}

func sendStream(conn net.Conn, id int, typ, data string) {
// §.splinter/page/cmd/relay/main/sendStream.fs
}

// ── composition CLI commands ────────────────────────────────────────────────

// dataDir is where the pod library lives.  Delegates to comp.DataDir:
// RELAY_DATA_DIR wins, then ./.relay (workspace), then ~/.relay (global).
func dataDir() string {
// §.splinter/page/cmd/relay/main/dataDir.fs
}

func compsDir() string {
// §.splinter/page/cmd/relay/main/compsDir.fs
}

func openCompDB() *sql.DB {
// §.splinter/page/cmd/relay/main/openCompDB.fs
}

func runInit(gitURL string) {
// §.splinter/page/cmd/relay/main/runInit.fs
}

func runShardCmd(args []string) {
// §.splinter/page/cmd/relay/main/runShardCmd.fs
}

func shardUsage() {
// §.splinter/page/cmd/relay/main/shardUsage.fs
}

// resolveShard finds a shard by name or key — name matches go through
// search+rank, exact key hits go direct.
func resolveShard(s *comp.Store, name string) *comp.Shard {
// §.splinter/page/cmd/relay/main/resolveShard.fs
}

// runHistoryCmd searches the workspace execution memory.  Every subpod run
// is recorded by the planner; any pod can ask what was done before.  This is
// the recall half of "execution is memory".
func runHistoryCmd(args []string) {
// §.splinter/page/cmd/relay/main/runHistoryCmd.fs
}

// runSessionsCmd inspects every pod in the workspace and the subpod runs each
// spawned — the audit surface. Shows prompt, state, model, success. A
// self-audit subpod runs this via shell and checks behavior against expectations.
func runSessionsCmd(args []string) {
// §.splinter/page/cmd/relay/main/runSessionsCmd.fs
}

// truncStr clips a string to n visible runes with an ellipsis.
func truncStr(s string, n int) string {
// §.splinter/page/cmd/relay/main/truncStr.fs
}

func runShardRun(args []string) {
// §.splinter/page/cmd/relay/main/runShardRun.fs
}

func runSpawnCmd(args []string, provider, model string) {
// §.splinter/page/cmd/relay/main/runSpawnCmd.fs
}

func runTour() {
// §.splinter/page/cmd/relay/main/runTour.fs
}
