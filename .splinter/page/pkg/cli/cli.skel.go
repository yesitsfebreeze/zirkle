// §source page/pkg/cli/cli.go
// Package cli provides a unix-socket JSON-RPC client for the relay daemon.
package cli

import (
	"bufio"
	"encoding/json"
	"fmt"
	"net"
	"os"
	"text/tabwriter"
)

const DefaultSocketPath = "/tmp/relay.sock"

// Client connects to the relay daemon over a unix socket.
type Client struct {
	SocketPath string
	conn       net.Conn
}

type jsonRequest struct {
	ID     int            `json:"id"`
	Method string         `json:"method"`
	Params map[string]any `json:"params,omitempty"`
}

type jsonResponse struct {
	ID     int    `json:"id"`
	Result any    `json:"result,omitempty"`
	Error  string `json:"error,omitempty"`
}

type jsonStream struct {
	ID   int    `json:"id"`
	Type string `json:"type"` // "line", "done", "error"
	Data string `json:"data"`
}

// New creates a Client. An empty socketPath defaults to DefaultSocketPath.
func New(socketPath string) *Client {
// §.splinter/page/pkg/cli/cli/New.fs
}

// Dial connects to the daemon unix socket.
func (c *Client) Dial() error {
// §.splinter/page/pkg/cli/cli/Client.Dial.fs
}

// Close closes the connection.
func (c *Client) Close() error {
// §.splinter/page/pkg/cli/cli/Client.Close.fs
}

func (c *Client) sendRequest(method string, params map[string]any) error {
// §.splinter/page/pkg/cli/cli/Client.sendRequest.fs
}

// Call sends a JSON-RPC request and returns the decoded result for
// non-streaming commands (e.g. list, kill).
func (c *Client) Call(method string, params any) (any, error) {
// §.splinter/page/pkg/cli/cli/Client.Call.fs
}

// Run sends a prompt to the daemon and streams the response to stdout.
func (c *Client) Run(prompt string) error {
// §.splinter/page/pkg/cli/cli/Client.Run.fs
}

// List prints active pods to stdout.
func (c *Client) List() error {
// §.splinter/page/pkg/cli/cli/Client.List.fs
}

// Kill stops a pod by ID.
func (c *Client) Kill(id string) error {
// §.splinter/page/pkg/cli/cli/Client.Kill.fs
}

// Logs tails the conversation of a pod to stdout.
func (c *Client) Logs(id string) error {
// §.splinter/page/pkg/cli/cli/Client.Logs.fs
}

func toMap(v any) map[string]any {
// §.splinter/page/pkg/cli/cli/toMap.fs
}
