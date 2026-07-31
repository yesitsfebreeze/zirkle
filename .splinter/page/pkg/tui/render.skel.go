// §source page/pkg/tui/render.go
package tui

import (
	"fmt"
	"os"
	"strings"
	"time"

	"github.com/charmbracelet/lipgloss"
)

// terminalContent renders the right pane: code + shell output from the
// current agent and sub-agents. Empty state shows a placeholder.
func (m Model) terminalContent() string {
// §.splinter/page/pkg/tui/render/Model.terminalContent.fs
}

// thoughtsContent renders the left pane (conversation): streaming agent
// text while a stream is live, otherwise the conversation log for the selected
// pod. A header (pod id · state) sits above it when a real pod is selected.
func (m Model) thoughtsContent() string {
// §.splinter/page/pkg/tui/render/Model.thoughtsContent.fs
}

func (m Model) chatContent() string {
// §.splinter/page/pkg/tui/render/Model.chatContent.fs
}

// treeContent renders the tree top-anchored (newest pod first, at top).
func (m Model) treeContent() string {
// §.splinter/page/pkg/tui/render/Model.treeContent.fs
}

// listContent renders the active list (pods or help) for the bottom viewport.
func (m Model) listContent() string {
// §.splinter/page/pkg/tui/render/Model.listContent.fs
}

func symbolForState(state string) string {
// §.splinter/page/pkg/tui/render/symbolForState.fs
}

func (m Model) renderTree() string {
// §.splinter/page/pkg/tui/render/Model.renderTree.fs
}

// View renders the dashboard: chat on top, input center, separator, status + pods below.
// The status line (version, pod count, load) lives with the pods, not the chat —
// it's pod metadata, not conversation context.
func (m Model) View() string {
// §.splinter/page/pkg/tui/render/Model.View.fs
}

// renderDivider returns the broadcast marquee row between input and the
// status line. When no broadcast is active it is empty: the status bar below is
// a strong enough separator on its own, so we don't draw a rule line.
func (m Model) renderDivider() string {
// §.splinter/page/pkg/tui/render/Model.renderDivider.fs
}

func (m Model) renderInput() string {
// §.splinter/page/pkg/tui/render/Model.renderInput.fs
}

// acceptSuggestion appends the ghost completion to the input and places the
// cursor at the end. Tab and right-arrow both route here.
func (m *Model) acceptSuggestion() {
// §.splinter/page/pkg/tui/render/Model.acceptSuggestion.fs
}

// renderSuggestion returns the inline completion preview: the typed text in
// the default foreground followed by the suggested suffix in muted, so it reads
// as a placeholder continuing the input — not a selectable list. Tab or
// right-arrow accepts (see handleKey).
func (m Model) renderSuggestion() string {
// §.splinter/page/pkg/tui/render/Model.renderSuggestion.fs
}

func trunc(s string, n int) string {
// §.splinter/page/pkg/tui/render/trunc.fs
}
