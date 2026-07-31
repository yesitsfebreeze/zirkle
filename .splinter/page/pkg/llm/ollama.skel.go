// §source page/pkg/llm/ollama.go
package llm

import (
	"bytes"
	"context"
	"encoding/hex"
	"encoding/json"
	"errors"
	"fmt"
	"io"
	"net"
	"net/http"
	"os"
	"strings"
	"time"
)

const (
	defaultOllamaURL   = "http://localhost:11434"
	defaultOllamaModel = "glm-5.2:cloud"
)

type Ollama struct {
	BaseURL string
	Model   string
	HTTP    *http.Client
}

// wslGatewayIP returns the default-route gateway IP from /proc/net/route
// (little-endian hex), or empty if it cannot be parsed. On WSL this is the
// Windows host where Ollama typically runs — localhost inside WSL cannot
// reach it.
func wslGatewayIP() string {
// §.splinter/page/pkg/llm/ollama/wslGatewayIP.fs
}

// parseGatewayIP extracts the default-route gateway IP from /proc/net/route
// content. The gateway field is little-endian hex (e.g. 01B01BAC →
// 172.27.176.1). Returns empty if there is no default route or the field
// cannot be parsed.
func parseGatewayIP(routeData string) string {
// §.splinter/page/pkg/llm/ollama/parseGatewayIP.fs
}

// isWSL reports whether we're running inside WSL by checking for the
// WSLInterop binfmt_misc entry.
func isWSL() bool {
// §.splinter/page/pkg/llm/ollama/isWSL.fs
}

// defaultOllamaURLForHost returns the best default Ollama URL for this machine.
// On WSL, Ollama runs on the Windows host (the default gateway), not
// localhost — auto-discover it so the daemon dispatches pods without
// requiring OLLAMA_HOST to be set manually each session.
func defaultOllamaURLForHost() string {
// §.splinter/page/pkg/llm/ollama/defaultOllamaURLForHost.fs
}

// NewOllama builds a client for a local Ollama daemon. An empty baseURL falls
// back to OLLAMA_HOST, then the auto-discovered default (WSL gateway on WSL,
// localhost elsewhere); an empty model to qwen3.5:0.8b.
func init() {
// §.splinter/page/pkg/llm/ollama/init.fs
}

func NewOllama(baseURL, model string) *Ollama {
// §.splinter/page/pkg/llm/ollama/NewOllama.fs
}

type ollamaReq struct {
	Model    string        `json:"model"`
	Messages []ollamaMsg   `json:"messages"`
	Stream   bool          `json:"stream"`
	Tools    []ollamaTool  `json:"tools,omitempty"`
	Options  ollamaOptions `json:"options,omitempty"`
}

type ollamaTool struct {
	Type     string         `json:"type"` // always "function"
	Function ollamaToolSpec `json:"function"`
}

type ollamaToolSpec struct {
	Name        string         `json:"name"`
	Description string         `json:"description"`
	Parameters  map[string]any `json:"parameters"`
}

type ollamaOptions struct {
	NumPredict int `json:"num_predict,omitempty"`
}

type ollamaMsg struct {
	Role      string           `json:"role"`
	Content   string           `json:"content"`
	ToolCalls []ollamaToolCall `json:"tool_calls,omitempty"`
}

type ollamaToolCall struct {
	Function ollamaToolCallFn `json:"function"`
}

type ollamaToolCallFn struct {
	Name      string         `json:"name"`
	Arguments map[string]any `json:"arguments"`
}

type ollamaResp struct {
	Message         ollamaMsg `json:"message"`
	PromptEvalCount int       `json:"prompt_eval_count"`
	EvalCount       int       `json:"eval_count"`
	Error           string    `json:"error"`
}

// ollamaStreamResp is one NDJSON line from a streaming /api/chat response.
type ollamaStreamResp struct {
	Message         ollamaMsg `json:"message"`
	Done            bool      `json:"done"`
	PromptEvalCount int       `json:"prompt_eval_count"`
	EvalCount       int       `json:"eval_count"`
	Error           string    `json:"error"`
}

func (o *Ollama) Chat(ctx context.Context, req ChatRequest) (*ChatResponse, error) {
// §.splinter/page/pkg/llm/ollama/Ollama.Chat.fs
}

// ChatStream sends the request with stream=true and emits each NDJSON chunk as
// a StreamEvent. The channel closes after the terminal event (done=true or
// error). ctx cancellation aborts the HTTP request and closes the channel.
func (o *Ollama) ChatStream(ctx context.Context, req ChatRequest) <-chan StreamEvent {
// §.splinter/page/pkg/llm/ollama/Ollama.ChatStream.fs
}

// toOllamaMsg translates an llm.Message to the Ollama wire format. Tool-use
// assistant messages get tool_calls; tool-result user messages become
// role "tool" with the result as content (Ollama matches by position, not ID).
func toOllamaMsg(m Message) ollamaMsg {
// §.splinter/page/pkg/llm/ollama/toOllamaMsg.fs
}

func toOllamaTools(tools []Tool) []ollamaTool {
// §.splinter/page/pkg/llm/ollama/toOllamaTools.fs
}