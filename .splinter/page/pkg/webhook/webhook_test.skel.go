// §source page/pkg/webhook/webhook_test.go
package webhook

import (
	"bytes"
	"context"
	"encoding/json"
	"net/http"
	"net/http/httptest"
	"strings"
	"sync"
	"sync/atomic"
	"testing"
	"time"

	"github.com/feb/relay/pkg/adapter"
)

// TestValidSecret verifies POST /hook/<valid-secret> returns 200 and calls deliver.
func TestValidSecret(t *testing.T) {
// §.splinter/page/pkg/webhook/webhook_test/TestValidSecret.fs
}

// TestBadSecret verifies POST /hook/<bad-secret> returns 401 and does NOT call deliver.
func TestBadSecret(t *testing.T) {
// §.splinter/page/pkg/webhook/webhook_test/TestBadSecret.fs
}

// TestIdempotency verifies that the same X-Idempotency-Key skips the second delivery.
func TestIdempotency(t *testing.T) {
// §.splinter/page/pkg/webhook/webhook_test/TestIdempotency.fs
}

// TestEmptySecret verifies that Run returns nil immediately when secret is empty.
func TestEmptySecret(t *testing.T) {
// §.splinter/page/pkg/webhook/webhook_test/TestEmptySecret.fs
}

// TestMethodNotAllowed verifies non-POST requests get 405.
func TestMethodNotAllowed(t *testing.T) {
// §.splinter/page/pkg/webhook/webhook_test/TestMethodNotAllowed.fs
}

// TestDeliverTimeout covers the spec's "30s handler deadline" arm: when
// deliver blocks past the deadline the request gets 408, and because the key
// was never recorded a later retry is still accepted rather than deduped away.
func TestDeliverTimeout(t *testing.T) {
// §.splinter/page/pkg/webhook/webhook_test/TestDeliverTimeout.fs
}

type panicSink struct {
	mu   sync.Mutex
	rows []string
}

func (p *panicSink) RecordFault(podID, kind, site, msg, stack string) error {
// §.splinter/page/pkg/webhook/webhook_test/panicSink.RecordFault.fs
}

func (p *panicSink) len() int {
// §.splinter/page/pkg/webhook/webhook_test/panicSink.len.fs
}

// A panic inside deliver used to be swallowed by a bare recover(): the request
// returned 200 and the crash left no trace anywhere. It must now be recorded.
func TestDeliverPanicIsRecorded(t *testing.T) {
// §.splinter/page/pkg/webhook/webhook_test/TestDeliverPanicIsRecorded.fs
}

// The handler must not block for its full deadline when deliver panics.
func TestDeliverPanicDoesNotHangHandler(t *testing.T) {
// §.splinter/page/pkg/webhook/webhook_test/TestDeliverPanicDoesNotHangHandler.fs
}
