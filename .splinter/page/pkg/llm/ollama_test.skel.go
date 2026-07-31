// §source page/pkg/llm/ollama_test.go
package llm

import (
	"context"
	"encoding/json"
	"io"
	"net/http"
	"net/http/httptest"
	"strings"
	"testing"
)

func TestOllamaChat(t *testing.T) {
// §.splinter/page/pkg/llm/ollama_test/TestOllamaChat.fs
}

func TestOllamaModelNotPulled(t *testing.T) {
// §.splinter/page/pkg/llm/ollama_test/TestOllamaModelNotPulled.fs
}

func TestOllamaDaemonDown(t *testing.T) {
// §.splinter/page/pkg/llm/ollama_test/TestOllamaDaemonDown.fs
}

func TestOllamaDefaults(t *testing.T) {
// §.splinter/page/pkg/llm/ollama_test/TestOllamaDefaults.fs
}

func TestOllamaToolCall(t *testing.T) {
// §.splinter/page/pkg/llm/ollama_test/TestOllamaToolCall.fs
}

// TestOllamaToolResultRoundTrip verifies that tool-use and tool-result
// messages serialize correctly: the assistant message carries tool_calls,
// the tool result becomes role "tool" with the result as content.
func TestOllamaToolResultRoundTrip(t *testing.T) {
// §.splinter/page/pkg/llm/ollama_test/TestOllamaToolResultRoundTrip.fs
}

func TestWSLGatewayParse(t *testing.T) {
// §.splinter/page/pkg/llm/ollama_test/TestWSLGatewayParse.fs
}

func TestProviderSelection(t *testing.T) {
// §.splinter/page/pkg/llm/ollama_test/TestProviderSelection.fs
}

func TestProviderEnvOverride(t *testing.T) {
// §.splinter/page/pkg/llm/ollama_test/TestProviderEnvOverride.fs
}

func TestOllamaChatStream(t *testing.T) {
// §.splinter/page/pkg/llm/ollama_test/TestOllamaChatStream.fs
}

func TestOllamaChatStreamError(t *testing.T) {
// §.splinter/page/pkg/llm/ollama_test/TestOllamaChatStreamError.fs
}