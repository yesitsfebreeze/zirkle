// §source page/pkg/tui/settings.go
package tui

import (
	"fmt"
	"strings"

	"github.com/charmbracelet/lipgloss"
)

// The settings screen (`:config`) is one flat list of rows. keys.go moves the
// cursor over it and mutates the row under the cursor; render.go draws it. Both
// read settingRows so a row can never exist in one and not the other.
type settingKind int

const (
	settingColor settingKind = iota
	settingToggle
	settingChoice
)

type settingRow struct {
	kind  settingKind
	label string
	on    bool     // settingToggle
	color string   // settingColor
	value string   // settingChoice
	opts  []string // settingChoice
}

// frameOptions are the timeline buckets, in cycle order.
var frameOptions = []string{"day", "week", "month", "hour"}

// settingRows is the live settings list: the two theme colors, then the pod
// list timeline — one tick box per roll-up part, plus the frame bucket.
func (m Model) settingRows() []settingRow {
// §.splinter/page/pkg/tui/settings/Model.settingRows.fs
}

// toggleSetting flips the tick box at row i and persists the timeline. A row
// that is not a tick box is left alone, so a stray key cannot corrupt state.
func (m *Model) toggleSetting(i int) {
// §.splinter/page/pkg/tui/settings/Model.toggleSetting.fs
}

// cycleFrame steps the frame bucket by dir and persists the timeline.
func (m *Model) cycleFrame(dir int) {
// §.splinter/page/pkg/tui/settings/Model.cycleFrame.fs
}

// persistTimeline writes the timeline settings back to the config file when a
// writer was injected. A failure surfaces in the status line rather than
// silently dropping the change.
func (m *Model) persistTimeline() {
// §.splinter/page/pkg/tui/settings/Model.persistTimeline.fs
}

func (m Model) renderConfig() string {
// §.splinter/page/pkg/tui/settings/Model.renderConfig.fs
}
