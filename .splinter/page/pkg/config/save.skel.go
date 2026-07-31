// §source page/pkg/config/save.go
package config

import (
	"fmt"
	"os"
	"strings"
)

// SaveTimeline writes the [timeline] section back to the config file, leaving
// every other line byte-identical: the file is the user's, so a tick in the TUI
// must not reformat their config or drop their comments elsewhere. Comments
// inside the [timeline] block itself are replaced along with the keys. A missing
// file is created from the embedded default first, and a missing section is
// appended.
func SaveTimeline(path string, tl TimelineConfig) error {
// §.splinter/page/pkg/config/save/SaveTimeline.fs
}
