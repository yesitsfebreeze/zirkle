// §source page/pkg/subagent/policy_test.go
package subagent

import (
	"context"
	"strings"
	"testing"
	"time"

	"github.com/feb/relay/pkg/sandbox"
)

func TestDefaultIsConfined(t *testing.T) {
// §.splinter/page/pkg/subagent/policy_test/TestDefaultIsConfined.fs
}

func TestUnconfinedIsOptIn(t *testing.T) {
// §.splinter/page/pkg/subagent/policy_test/TestUnconfinedIsOptIn.fs
}

// The escape hatch is a real path, not just a type switch: a subagent runs
// through it end to end.
func TestUnconfinedSpawnRuns(t *testing.T) {
// §.splinter/page/pkg/subagent/policy_test/TestUnconfinedSpawnRuns.fs
}

// An unsandboxable host must fail loudly rather than quietly running the agent
// with no boundary at all.
func TestUnsandboxableHostRefusesToDowngrade(t *testing.T) {
// §.splinter/page/pkg/subagent/policy_test/TestUnsandboxableHostRefusesToDowngrade.fs
}

func TestForwardedEnvReachesSandbox(t *testing.T) {
// §.splinter/page/pkg/subagent/policy_test/TestForwardedEnvReachesSandbox.fs
}
