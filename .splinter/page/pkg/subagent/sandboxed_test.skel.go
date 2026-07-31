// §source page/pkg/subagent/sandboxed_test.go
package subagent

import (
	"context"
	"os"
	"path/filepath"
	"strings"
	"testing"
	"time"

	"github.com/feb/relay/pkg/sandbox"
)

func requireSandbox(t *testing.T) {
// §.splinter/page/pkg/subagent/sandboxed_test/requireSandbox.fs
}

// TestSandboxedRunEndToEnd drives the real subagent inside a real sandbox: the
// test binary stands in for the relay binary, gets bind-mounted read-only at its
// own host path, and writes its Result to stdout because fd 3 does not exist in
// there either.
func TestSandboxedRunEndToEnd(t *testing.T) {
// §.splinter/page/pkg/subagent/sandboxed_test/TestSandboxedRunEndToEnd.fs
}

// The sandboxed subagent must not be able to touch the host, and its work must
// land in the spec's directory rather than anywhere it chooses.
func TestSandboxedSubagentCannotReachHost(t *testing.T) {
// §.splinter/page/pkg/subagent/sandboxed_test/TestSandboxedSubagentCannotReachHost.fs
}

func TestSandboxedTimesOut(t *testing.T) {
// §.splinter/page/pkg/subagent/sandboxed_test/TestSandboxedTimesOut.fs
}

func TestSandboxedReportsMissingSandbox(t *testing.T) {
// §.splinter/page/pkg/subagent/sandboxed_test/TestSandboxedReportsMissingSandbox.fs
}
