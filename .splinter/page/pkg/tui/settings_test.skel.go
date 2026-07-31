// §source page/pkg/tui/settings_test.go
package tui

import (
	"errors"
	"strings"
	"testing"
	"time"

	tea "github.com/charmbracelet/bubbletea"
)

var errFake = errors.New("config file is read-only")

func settingsModel(t *testing.T) Model {
// §.splinter/page/pkg/tui/settings_test/settingsModel.fs
}

// Every tick box the settings screen shows must flip exactly one timeline field.
func TestSettingsTickBoxesToggleTimelineFields(t *testing.T) {
// §.splinter/page/pkg/tui/settings_test/TestSettingsTickBoxesToggleTimelineFields.fs
}

// A tick must reach the config file through the injected writer.
func TestSettingsTickPersists(t *testing.T) {
// §.splinter/page/pkg/tui/settings_test/TestSettingsTickPersists.fs
}

// A failing writer must surface, not swallow, the error.
func TestSettingsTickReportsSaveFailure(t *testing.T) {
// §.splinter/page/pkg/tui/settings_test/TestSettingsTickReportsSaveFailure.fs
}

// The frame row cycles through the buckets rather than toggling.
func TestSettingsFrameRowCycles(t *testing.T) {
// §.splinter/page/pkg/tui/settings_test/TestSettingsFrameRowCycles.fs
}

// The cursor must reach the last row and stop there.
func TestSettingsCursorStopsAtLastRow(t *testing.T) {
// §.splinter/page/pkg/tui/settings_test/TestSettingsCursorStopsAtLastRow.fs
}

// Unticking a part drops it from the header line and leaves the rest.
func TestSettingsTicksControlHeaderParts(t *testing.T) {
// §.splinter/page/pkg/tui/settings_test/TestSettingsTicksControlHeaderParts.fs
}

// Ticking anything must not move the selection: headers stay out of it.
func TestSettingsTicksLeaveSelectionAlone(t *testing.T) {
// §.splinter/page/pkg/tui/settings_test/TestSettingsTicksLeaveSelectionAlone.fs
}

// The screen renders a tick box per toggle and the frame choice.
func TestSettingsScreenRendersTickBoxes(t *testing.T) {
// §.splinter/page/pkg/tui/settings_test/TestSettingsScreenRendersTickBoxes.fs
}
