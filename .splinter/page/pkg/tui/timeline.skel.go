// §source page/pkg/tui/timeline.go
package tui

import (
	"fmt"
	"strconv"
	"strings"
	"time"
)

// TimelineConfig controls the non-selectable frame headers in the pod list: a
// rule row that marks where one time frame ends and the next begins, carrying
// the roll-up for every pod inside it. Headers exist at render time only —
// they never enter the selection space, so navigation is unaffected.
type TimelineConfig struct {
	Enabled  bool
	Frame    string // "day" (default) | "week" | "month" | "hour"
	DayStart string // "HH:MM" — where the day frame rolls over

	// Roll-up parts, each a tick box in the settings screen. All off leaves
	// the bare frame label, which is still a full-width rule row.
	ShowCount  bool
	ShowStates bool
	ShowSpan   bool
}

// DefaultTimeline is day frames rolling over at midnight, carrying every
// roll-up part.
func DefaultTimeline() TimelineConfig {
// §.splinter/page/pkg/tui/timeline/DefaultTimeline.fs
}

// dayStartMinutes parses DayStart into minutes past midnight. An unparsable or
// out-of-range value is midnight, so a typo in the config file degrades to the
// default instead of hiding the list.
func (c TimelineConfig) dayStartMinutes() int {
// §.splinter/page/pkg/tui/timeline/TimelineConfig.dayStartMinutes.fs
}

// frameStart truncates t to the start of the frame it belongs to. For day and
// week frames the DayStart offset applies: a timestamp before the rollover
// belongs to the previous frame.
func (c TimelineConfig) frameStart(t time.Time) time.Time {
// §.splinter/page/pkg/tui/timeline/TimelineConfig.frameStart.fs
}

// shiftDay returns the start of t's day-frame given the rollover offset.
func shiftDay(t time.Time, off time.Duration) time.Time {
// §.splinter/page/pkg/tui/timeline/shiftDay.fs
}

// frameLabel names a frame relative to now, so the operator reads "today"
// rather than a date they have to convert.
func (c TimelineConfig) frameLabel(start, now time.Time) string {
// §.splinter/page/pkg/tui/timeline/TimelineConfig.frameLabel.fs
}

// TimelineHeader is the roll-up for one time frame: every pod whose CreatedAt
// falls inside it, at any depth, collapsed or filtered out included — the
// header describes the frame, not the current view.
type TimelineHeader struct {
	Start   time.Time
	Label   string
	Total   int
	Symbols map[string]int // status symbol -> count, keyed as in symbolForState
	Span    time.Duration  // first to last CreatedAt inside the frame

	first, last time.Time
}

// symbolOrder fixes the tally order so the header does not reshuffle between
// refreshes as map iteration varies.
var symbolOrder = []string{"■", "▶", "●", "✕"}

// Line renders the header as a muted rule row: label, pod count, per-state
// tallies, span, then rule glyphs out to width.
func (h TimelineHeader) Line(width int, c TimelineConfig) string {
// §.splinter/page/pkg/tui/timeline/TimelineHeader.Line.fs
}

// shortDur formats a span without the seconds noise of time.Duration.String.
func shortDur(d time.Duration) string {
// §.splinter/page/pkg/tui/timeline/shortDur.fs
}

// timelineHeaders maps a visible-row index to the header drawn above it. A
// header opens before a depth-0 row whose frame differs from the previous
// depth-0 row's; children stay inside their parent's group. Returns nil when
// disabled, which is what keeps the render path free of the feature.
func timelineHeaders(views []PodView, vis []int, c TimelineConfig, now time.Time) map[int]TimelineHeader {
// §.splinter/page/pkg/tui/timeline/timelineHeaders.fs
}
