// §source page/pkg/webhook/webhook.go
// Package webhook implements adapter.InputAdapter for HTTP webhook triggers.
//
//	POST /hook/<secret>  — validates secret, creates InMessage, calls deliver
//	X-Idempotency-Key    — dedup via in-memory map with 5min TTL
//	30s handler timeout  — returns 408 if deliver blocks
//	Empty secret         — webhook disabled, Run returns nil
package webhook

import (
	"context"
	"crypto/subtle"
	"encoding/json"
	"fmt"
	"io"
	"log/slog"
	"net/http"
	"strings"
	"sync"
	"time"

	"github.com/feb/relay/pkg/adapter"
	"github.com/feb/relay/pkg/fault"
)

const (
	defaultPort = 9842             // default listen port
	cleanupInt  = 1 * time.Minute  // how often cleanup goroutine runs
	dedupTTL    = 5 * time.Minute  // how long idempotency keys are remembered
	handlerTO   = 30 * time.Second // deliver timeout
)

// Webhook implements adapter.InputAdapter. It listens for POST /hook/<secret>
// and delivers each request as an adapter.InMessage.
type Webhook struct {
	secret  string
	port    int
	timeout time.Duration // deliver deadline; 0 = handlerTO

	// Faults records panics raised inside deliver. Nil is allowed: the panic
	// still reaches stderr, it just is not persisted.
	Faults fault.Sink

	mu      sync.RWMutex
	seen    map[string]time.Time // idempotency key → insertion time
	deliver func(adapter.InMessage)
}

// New creates a Webhook. If port <= 0, defaultPort (9842) is used.
func New(secret string, port int) *Webhook {
// §.splinter/page/pkg/webhook/webhook/New.fs
}

// Handler returns the http.Handler that serves the webhook endpoint.
// Used internally by Run and available for testing via httptest.NewServer.
func (w *Webhook) Handler() http.Handler {
// §.splinter/page/pkg/webhook/webhook/Webhook.Handler.fs
}

// Run implements adapter.InputAdapter. It blocks until ctx is cancelled,
// owning an http.Server on the configured port. If secret is empty it
// returns nil immediately after logging "webhook disabled".
func (w *Webhook) Run(ctx context.Context, deliver func(adapter.InMessage)) error {
// §.splinter/page/pkg/webhook/webhook/Webhook.Run.fs
}

// handleHook processes POST /hook/<secret>.
func (w *Webhook) handleHook(rw http.ResponseWriter, r *http.Request) {
// §.splinter/page/pkg/webhook/webhook/Webhook.handleHook.fs
}

// validateSecret performs a constant-time comparison.
func (w *Webhook) validateSecret(got string) bool {
// §.splinter/page/pkg/webhook/webhook/Webhook.validateSecret.fs
}

// isDuplicate returns true if ikey has been seen before.
func (w *Webhook) isDuplicate(ikey string) bool {
// §.splinter/page/pkg/webhook/webhook/Webhook.isDuplicate.fs
}

// cleanupLoop runs every cleanupInt and evicts entries older than dedupTTL.
func (w *Webhook) cleanupLoop(ctx context.Context) {
// §.splinter/page/pkg/webhook/webhook/Webhook.cleanupLoop.fs
}
