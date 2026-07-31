// §source page/pkg/llm/anthropic.go
package llm

import (
	"bufio"
	"bytes"
	"context"
	"encoding/json"
	"fmt"
	"net/http"
	"os"
	"strings"
	"time"
)

const defaultModel = "claude-sonnet-4-20250514"

type Anthropic struct {
	APIKey  string
	BaseURL string
	Model   string
	HTTP    *http.Client
}

func init() {
// §.splinter/page/pkg/llm/anthropic/init.fs
}

func NewAnthropic(apiKey, model string) *Anthropic {
// §.splinter/page/pkg/llm/anthropic/NewAnthropic.fs
}

type anthropicReq struct {
	Model     string          `json:"model"`
	MaxTokens int             `json:"max_tokens"`
	Messages  []anthropicMsg  `json:"messages"`
	System    string          `json:"system,omitempty"`
	Tools     []anthropicTool `json:"tools,omitempty"`
	Stream    bool            `json:"stream,omitempty"`
}

type anthropicTool struct {
	Name        string         `json:"name"`
	Description string         `json:"description"`
	InputSchema map[string]any `json:"input_schema"`
}

// anthropicMsg.Content is either a string (simple text) or
// []anthropicBlock (tool_use / tool_result). The wire API accepts both shapes.
type anthropicMsg struct {
	Role    string `json:"role"`
	Content any    `json:"content"`
}

type anthropicBlock struct {
	Type      string         `json:"type"`
	Text      string         `json:"text,omitempty"`         // text blocks
	ID        string         `json:"id,omitempty"`           // tool_use
	Name      string         `json:"name,omitempty"`         // tool_use
	Input     map[string]any `json:"input,omitempty"`        // tool_use
	ToolUseID string         `json:"tool_use_id,omitempty"`  // tool_result
	Result    string         `json:"content,omitempty"`      // tool_result content
}

type anthropicResp struct {
	Content []anthropicBlock `json:"content"`
	Usage   struct {
		InputTokens  int `json:"input_tokens"`
		OutputTokens int `json:"output_tokens"`
	} `json:"usage"`
}

func (a *Anthropic) Chat(ctx context.Context, req ChatRequest) (*ChatResponse, error) {
// §.splinter/page/pkg/llm/anthropic/Anthropic.Chat.fs
}

// ChatStream sends the request with stream=true and parses Anthropic SSE
// events into StreamEvent. Text deltas become Content events; tool_use blocks
// become ToolCall events; message_stop carries the final Usage.
func (a *Anthropic) ChatStream(ctx context.Context, req ChatRequest) <-chan StreamEvent {
// §.splinter/page/pkg/llm/anthropic/Anthropic.ChatStream.fs
}

// toAnthropicMsg translates an llm.Message to the Anthropic wire format.
// Simple text → string content. Tool-use assistant → content-block array
// with text + tool_use. Tool-result user → content-block array with tool_result.
func toAnthropicMsg(m Message) anthropicMsg {
// §.splinter/page/pkg/llm/anthropic/toAnthropicMsg.fs
}

func toAnthropicTools(tools []Tool) []anthropicTool {
// §.splinter/page/pkg/llm/anthropic/toAnthropicTools.fs
}