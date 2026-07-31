// §source page/pkg/config/save_test.go
package config

import (
	"os"
	"path/filepath"
	"strings"
	"testing"
)

const userFile = `# my config, my comments
[daemon]
port = 9842      # do not touch
socket = "/tmp/relay.sock"

[timeline]
# stale comment inside the block
enabled = true
frame = "day"

[log]
level = "debug"
`

// A tick in the TUI rewrites the [timeline] block and nothing else: the rest of
// the file is the user's, comments included.
func TestSaveTimelinePatchesOnlyItsSection(t *testing.T) {
// §.splinter/page/pkg/config/save_test/TestSaveTimelinePatchesOnlyItsSection.fs
}

// A config file that predates the section gains it at the end.
func TestSaveTimelineAppendsMissingSection(t *testing.T) {
// §.splinter/page/pkg/config/save_test/TestSaveTimelineAppendsMissingSection.fs
}

// Saving twice must not stack sections.
func TestSaveTimelineIsIdempotent(t *testing.T) {
// §.splinter/page/pkg/config/save_test/TestSaveTimelineIsIdempotent.fs
}

// An empty path is a caller bug, not a silent no-op.
func TestSaveTimelineRejectsEmptyPath(t *testing.T) {
// §.splinter/page/pkg/config/save_test/TestSaveTimelineRejectsEmptyPath.fs
}
