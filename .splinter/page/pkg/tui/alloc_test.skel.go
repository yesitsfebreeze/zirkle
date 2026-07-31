// §source page/pkg/tui/alloc_test.go
package tui

import (
	"testing"

	tea "github.com/charmbracelet/bubbletea"
)

// The render path runs on every keystroke and every refresh tick, so an
// allocation blow-up there is felt as input lag. Ceilings sit at roughly 2x
// the measured baseline (View 99, treeContent 312 for 50 pods): loose enough
// to ride out a Go or lipgloss bump, tight enough to catch a copy added
// inside a loop or a per-row rebuild.
const (
	maxViewAllocs = 320
	maxTreeAllocs = 700
)

func benchModel(t testing.TB, n int) Model {
// §.splinter/page/pkg/tui/alloc_test/benchModel.fs
}

func TestViewAllocationCeiling(t *testing.T) {
// §.splinter/page/pkg/tui/alloc_test/TestViewAllocationCeiling.fs
}

func TestTreeContentAllocationCeiling(t *testing.T) {
// §.splinter/page/pkg/tui/alloc_test/TestTreeContentAllocationCeiling.fs
}

// visible() is called several times per render (renderTree, the status
// counter, selectedIdx), so it must stay cheap and must not be quadratic.
func TestVisibleScalesLinearly(t *testing.T) {
// §.splinter/page/pkg/tui/alloc_test/TestVisibleScalesLinearly.fs
}

var modelCache = map[int]Model{}

func benchModelCached(t testing.TB, n int) Model {
// §.splinter/page/pkg/tui/alloc_test/benchModelCached.fs
}

func BenchmarkView(b *testing.B) {
// §.splinter/page/pkg/tui/alloc_test/BenchmarkView.fs
}

func BenchmarkTreeContent(b *testing.B) {
// §.splinter/page/pkg/tui/alloc_test/BenchmarkTreeContent.fs
}
