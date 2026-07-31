// §source page/pkg/tui/multinput.go
package tui

import (
	"strings"

	"github.com/charmbracelet/bubbles/key"
	"github.com/charmbracelet/bubbles/textarea"
	tea "github.com/charmbracelet/bubbletea"
	"github.com/charmbracelet/lipgloss"
)

// BoundaryEvent signals that the cursor hit the top or bottom of the input
// while pressing up or down — the parent should use this to switch panes.
type BoundaryEvent int

const (
	BoundaryNone   BoundaryEvent = iota
	BoundaryTop                  // up pressed at line 0
	BoundaryBottom               // down pressed at last line
)

// MultiInput wraps textarea with boundary detection and scrollbar rendering.
type MultiInput struct {
	ta    textarea.Model
	ghost string // inline suggestion suffix, rendered dim after the last line
}

// ghostStyle renders the inline suggestion in a dim/muted color.
var ghostStyle = lipgloss.NewStyle().Foreground(lipgloss.Color("8"))

// NewMultiInput creates a multi-line input with the given width.
func NewMultiInput(width int) MultiInput {
// §.splinter/page/pkg/tui/multinput/NewMultiInput.fs
}

// Update passes a message to the textarea. If the up or down arrow is pressed
// at the top or bottom edge, the event is returned and the textarea is NOT
// updated (the parent decides what to do). Only the arrows navigate: inside a
// text buffer j and k are letters, and treating them as motion made every word
// containing one unwritable.
func (mi MultiInput) Update(msg tea.Msg) (MultiInput, tea.Cmd, BoundaryEvent) {
// §.splinter/page/pkg/tui/multinput/MultiInput.Update.fs
}

// View renders the textarea with a scrollbar on the right when content
// overflows the visible height. When ghost text is set, it is appended to the
// last visible line in a dim color — inline ghost text, not a separate row.
func (mi MultiInput) View() string {
// §.splinter/page/pkg/tui/multinput/MultiInput.View.fs
}

// SetHeight sets the visible row count.
func (mi *MultiInput) SetHeight(h int) {
// §.splinter/page/pkg/tui/multinput/MultiInput.SetHeight.fs
}

// SetWidth sets the content width.
func (mi *MultiInput) SetWidth(w int) {
// §.splinter/page/pkg/tui/multinput/MultiInput.SetWidth.fs
}

// Focus sets focus.
func (mi *MultiInput) Focus() tea.Cmd {
// §.splinter/page/pkg/tui/multinput/MultiInput.Focus.fs
}

// Blur removes focus.
func (mi *MultiInput) Blur() {
// §.splinter/page/pkg/tui/multinput/MultiInput.Blur.fs
}

// Focused reports focus state.
func (mi MultiInput) Focused() bool {
// §.splinter/page/pkg/tui/multinput/MultiInput.Focused.fs
}

// Value returns the full text content.
func (mi MultiInput) Value() string {
// §.splinter/page/pkg/tui/multinput/MultiInput.Value.fs
}

// SetValue replaces the text content.
func (mi *MultiInput) SetValue(s string) {
// §.splinter/page/pkg/tui/multinput/MultiInput.SetValue.fs
}

// Reset clears the content.
func (mi *MultiInput) Reset() {
// §.splinter/page/pkg/tui/multinput/MultiInput.Reset.fs
}

// SetCursor moves the cursor to the given column offset on the current line.
func (mi *MultiInput) SetCursor(n int) {
// §.splinter/page/pkg/tui/multinput/MultiInput.SetCursor.fs
}

// SetGhost sets the inline suggestion suffix rendered dim after the last
// line. Empty string clears it.
func (mi *MultiInput) SetGhost(g string) {
// §.splinter/page/pkg/tui/multinput/MultiInput.SetGhost.fs
}

// SetPlaceholder sets the textarea placeholder shown when the buffer is empty.
func (mi *MultiInput) SetPlaceholder(p string) {
// §.splinter/page/pkg/tui/multinput/MultiInput.SetPlaceholder.fs
}

// Height returns the visible row count.
func (mi MultiInput) Height() int {
// §.splinter/page/pkg/tui/multinput/MultiInput.Height.fs
}

// LineCount returns total content lines.
func (mi MultiInput) LineCount() int {
// §.splinter/page/pkg/tui/multinput/MultiInput.LineCount.fs
}

// Line returns the current 0-indexed line.
func (mi MultiInput) Line() int {
// §.splinter/page/pkg/tui/multinput/MultiInput.Line.fs
}

// Prompt returns the prompt string.
func (mi MultiInput) Prompt() string {
// §.splinter/page/pkg/tui/multinput/MultiInput.Prompt.fs
}

// SetPromptFunc sets a per-line prompt function. Only line 0 shows the icon.
func (mi *MultiInput) SetPromptFunc(width int, fn func(int) string) {
// §.splinter/page/pkg/tui/multinput/MultiInput.SetPromptFunc.fs
}
