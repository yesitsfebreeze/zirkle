// §source page/pkg/sandbox/sandbox_test.go
package sandbox

import (
	"context"
	"os"
	"path/filepath"
	"strings"
	"testing"
	"time"
)

func requireSandbox(t *testing.T) {
// §.splinter/page/pkg/sandbox/sandbox_test/requireSandbox.fs
}

// run executes a shell snippet inside the sandbox and returns its combined
// output plus whether it succeeded.
func run(t *testing.T, s Spec, script string) (string, bool) {
// §.splinter/page/pkg/sandbox/sandbox_test/run.fs
}

func TestProbeReportsHostCapability(t *testing.T) {
// §.splinter/page/pkg/sandbox/sandbox_test/TestProbeReportsHostCapability.fs
}

func TestCommandRejectsEmptySpec(t *testing.T) {
// §.splinter/page/pkg/sandbox/sandbox_test/TestCommandRejectsEmptySpec.fs
}

func TestWritesInsideRootLand(t *testing.T) {
// §.splinter/page/pkg/sandbox/sandbox_test/TestWritesInsideRootLand.fs
}

// The whole point: a path outside the sandbox is not merely write-protected,
// it is not there at all.
func TestHostPathsAreInvisible(t *testing.T) {
// §.splinter/page/pkg/sandbox/sandbox_test/TestHostPathsAreInvisible.fs
}

func TestEscapeAttemptsAreDenied(t *testing.T) {
// §.splinter/page/pkg/sandbox/sandbox_test/TestEscapeAttemptsAreDenied.fs
}

func TestToolsAreReadOnly(t *testing.T) {
// §.splinter/page/pkg/sandbox/sandbox_test/TestToolsAreReadOnly.fs
}

func TestEphemeralRootDoesNotTouchHost(t *testing.T) {
// §.splinter/page/pkg/sandbox/sandbox_test/TestEphemeralRootDoesNotTouchHost.fs
}

func TestEphemeralRootIsSizeCapped(t *testing.T) {
// §.splinter/page/pkg/sandbox/sandbox_test/TestEphemeralRootIsSizeCapped.fs
}

func TestNetworkOffByDefault(t *testing.T) {
// §.splinter/page/pkg/sandbox/sandbox_test/TestNetworkOffByDefault.fs
}

func TestRWHoleIsExplicit(t *testing.T) {
// §.splinter/page/pkg/sandbox/sandbox_test/TestRWHoleIsExplicit.fs
}

func TestEnvIsNotInherited(t *testing.T) {
// §.splinter/page/pkg/sandbox/sandbox_test/TestEnvIsNotInherited.fs
}
