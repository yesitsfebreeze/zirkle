// §source page/pkg/webhook/fuzz_test.go
package webhook

import (
	"bytes"
	"net/http"
	"net/http/httptest"
	"net/url"
	"testing"

	"github.com/feb/relay/pkg/adapter"
)

// The webhook is the only surface reachable by an unauthenticated stranger.
// No path, body, or header combination may panic the handler or leak a
// delivery past a bad secret.
func FuzzHandler(f *testing.F) {
// §.splinter/page/pkg/webhook/fuzz_test/FuzzHandler.fs
}

// httptest.NewRequest panics on targets it cannot parse (a space reads as an
// HTTP version, a stray % as a bad escape). That is a harness limit, not a
// handler bug, so let net/url decide and skip what it rejects rather than
// hand-rolling a character rule that keeps missing cases.
func isValidRequestTarget(p string) bool {
// §.splinter/page/pkg/webhook/fuzz_test/isValidRequestTarget.fs
}

func isValidHeaderValue(s string) bool {
// §.splinter/page/pkg/webhook/fuzz_test/isValidHeaderValue.fs
}
