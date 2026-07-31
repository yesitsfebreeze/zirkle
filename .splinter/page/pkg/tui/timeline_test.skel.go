// §source page/pkg/tui/timeline_test.go
package tui

import (
	"strings"
	"testing"
	"time"
)

func tlNow() time.Time {
// §.splinter/page/pkg/tui/timeline_test/tlNow.fs
}

// A day_start of 04:00 must keep a 02:00 pod in the previous day's frame —
// the whole point of a configurable rollover.
func TestTimelineDayStartOffsetMovesRollover(t *testing.T) {
// §.splinter/page/pkg/tui/timeline_test/TestTimelineDayStartOffsetMovesRollover.fs
}

func TestTimelineDayStartGarbageFallsBackToMidnight(t *testing.T) {
// §.splinter/page/pkg/tui/timeline_test/TestTimelineDayStartGarbageFallsBackToMidnight.fs
}

func TestTimelineLabelsAreRelative(t *testing.T) {
// §.splinter/page/pkg/tui/timeline_test/TestTimelineLabelsAreRelative.fs
}

func TestTimelineFrameLabelsPerFrame(t *testing.T) {
// §.splinter/page/pkg/tui/timeline_test/TestTimelineFrameLabelsPerFrame.fs
}

// Headers open only where the frame changes, and only on depth-0 rows: a child
// never starts a new group even when it was created after midnight.
func TestTimelineHeadersOnlyAtFrameBoundaries(t *testing.T) {
// §.splinter/page/pkg/tui/timeline_test/TestTimelineHeadersOnlyAtFrameBoundaries.fs
}

func TestTimelineDisabledEmitsNothing(t *testing.T) {
// §.splinter/page/pkg/tui/timeline_test/TestTimelineDisabledEmitsNothing.fs
}

// The roll-up describes the frame, not the view: children and rows filtered out
// of vis still count.
func TestTimelineStatsCoverCollapsedAndFilteredPods(t *testing.T) {
// §.splinter/page/pkg/tui/timeline_test/TestTimelineStatsCoverCollapsedAndFilteredPods.fs
}

func TestTimelineHeaderLineCarriesStats(t *testing.T) {
// §.splinter/page/pkg/tui/timeline_test/TestTimelineHeaderLineCarriesStats.fs
}

// Headers are chrome: they must appear in the rendered tree without shifting
// what the cursor selects.
func TestTimelineHeadersRenderWithoutMovingSelection(t *testing.T) {
// §.splinter/page/pkg/tui/timeline_test/TestTimelineHeadersRenderWithoutMovingSelection.fs
}
