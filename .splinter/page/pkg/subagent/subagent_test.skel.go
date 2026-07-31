// §source page/pkg/subagent/subagent_test.go
package subagent

import (
	"context"
	"os"
	"testing"
	"time"
)

// TestMain intercepts --subagent invocations so the test binary can act as a
// subagent process when spawned by TestSpawnAndCollect.
func TestMain(m *testing.M) {
// §.splinter/page/pkg/subagent/subagent_test/TestMain.fs
}

func TestSpawnAndCollect(t *testing.T) {
// §.splinter/page/pkg/subagent/subagent_test/TestSpawnAndCollect.fs
}
