// §source page/pkg/scheduler/scheduler_test.go
package scheduler

import (
	"context"
	"sync"
	"sync/atomic"
	"testing"
	"time"

	"github.com/robfig/cron/v3"
)

// recordingTrigger counts fires and captures the last agentID.
type recordingTrigger struct {
	mu    sync.Mutex
	fired int32
	last  string
	err   error
}

func (r *recordingTrigger) Fire(_ context.Context, agentID string) error {
// §.splinter/page/pkg/scheduler/scheduler_test/recordingTrigger.Fire.fs
}

func TestAddInvalidSpec(t *testing.T) {
// §.splinter/page/pkg/scheduler/scheduler_test/TestAddInvalidSpec.fs
}

func TestAddNilTrigger(t *testing.T) {
// §.splinter/page/pkg/scheduler/scheduler_test/TestAddNilTrigger.fs
}

// TestAddWiresJob: registering a valid spec installs exactly one cron entry
// whose job, when Run, fires the trigger with the registered agentID. This
// exercises the wiring deterministically without waiting for a real tick.
func TestAddWiresJob(t *testing.T) {
// §.splinter/page/pkg/scheduler/scheduler_test/TestAddWiresJob.fs
}

// TestFireOnSchedule: a 6-field (seconds) cron fires within ~1.5s, then
// Stop() halts further firing. Integration test — clock-dependent.
func TestFireOnSchedule(t *testing.T) {
// §.splinter/page/pkg/scheduler/scheduler_test/TestFireOnSchedule.fs
}

// TestStopBlocksNewFires: after Stop, a previously-registered schedule does
// not continue to fire.
func TestStopBlocksNewFires(t *testing.T) {
// §.splinter/page/pkg/scheduler/scheduler_test/TestStopBlocksNewFires.fs
}
