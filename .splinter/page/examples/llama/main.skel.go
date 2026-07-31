// §source page/examples/llama/main.go
// Example: prove pkg/llm data structures and Ollama client end-to-end with a
// real Llama model. No daemon, no TUI, no subagent — just the LLM pipe.
package main

import (
	"context"
	"encoding/json"
	"fmt"
	"io"
	"log"
	"net/http"
	"os"
	"strings"
	"time"

	"github.com/feb/relay/pkg/llm"
)

const defaultModel = "llama3.2:3b"

func main() {
// §.splinter/page/examples/llama/main/main.fs
}

// probeOllama checks that the daemon is reachable and the model is pulled,
// failing with actionable messages — same pattern as the smoke recipe.
func probeOllama(baseURL, model string) error {
// §.splinter/page/examples/llama/main/probeOllama.fs
}
