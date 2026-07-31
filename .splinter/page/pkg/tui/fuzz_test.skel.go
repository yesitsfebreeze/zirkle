// §source page/pkg/tui/fuzz_test.go
package tui

import (
	"testing"

	tea "github.com/charmbracelet/bubbletea"
)

// keyFor maps a fuzz byte onto the key space the model actually branches on,
// so the fuzzer spends its budget on reachable arms instead of random runes.
func keyFor(b byte) tea.KeyMsg {
// §.splinter/page/pkg/tui/fuzz_test/keyFor.fs
}

// checkInvariants asserts what must hold after every Update, whatever the
// model has been through. A violation here is a bug even when nothing panics.
func checkInvariants(t *testing.T, m Model) {
// §.splinter/page/pkg/tui/fuzz_test/checkInvariants.fs
}

// FuzzModelUpdate drives arbitrary key sequences through Update from both a
// cold (no views) and a populated model. The cold path is the one that shipped
// a crash: views stay empty until the first refreshMsg lands, and the arrow
// keys indexed views[0] on an empty slice.
func FuzzModelUpdate(f *testing.F) {
// §.splinter/page/pkg/tui/fuzz_test/FuzzModelUpdate.fs
}

// FuzzModelMessages drives the non-key message paths: resize (including
// degenerate sizes) and refreshes that shrink the list under a parked cursor.
func FuzzModelMessages(f *testing.F) {
// §.splinter/page/pkg/tui/fuzz_test/FuzzModelMessages.fs
}
