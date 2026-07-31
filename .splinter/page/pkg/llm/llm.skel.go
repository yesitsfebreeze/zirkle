// §source page/pkg/llm/llm.go
package llm

import "context"

type Message struct {
	Role       string
	Content    string
	ToolUse    *ToolCall   // assistant: a tool call the model made
	ToolResult *ToolResult // user: the result sent back for a tool call
}

type ToolCall struct {
	ID    string
	Name  string
	Input map[string]any
}

// ToolResult carries the result of a tool call back to the model. The ID
// matches the ToolCall.ID the provider returned (Anthropic requires it;
// Ollama matches by position).
type ToolResult struct {
	ID      string
	Content string
}

type Tool struct {
	Name        string
	Description string
	InputSchema map[string]any
}

type Usage struct {
	InputTokens  int
	OutputTokens int
}

type ChatRequest struct {
	Model     string
	Messages  []Message
	Tools     []Tool
	MaxTokens int
}

type ChatResponse struct {
	Message Message
	Usage   Usage
}

// StreamEvent is one chunk from a streaming LLM response. Content is a text
// delta; ToolCall is set when the model invokes a tool mid-stream; Done marks
// the terminal event (Usage populated, Err set on failure). A consumer reads
// until Done is true.
type StreamEvent struct {
	Content    string
	ToolCall   *ToolCall
	ToolOutput string // tool/shell result text destined for the terminal pane
	Done       bool
	Usage      *Usage
	Err        error
}

type LLM interface {
	Chat(ctx context.Context, req ChatRequest) (*ChatResponse, error)
	// ChatStream returns a channel of StreamEvent. The caller reads until the
	// channel closes; the final event before close has Done=true. ctx cancel
	// aborts the stream and closes the channel.
	ChatStream(ctx context.Context, req ChatRequest) <-chan StreamEvent
}