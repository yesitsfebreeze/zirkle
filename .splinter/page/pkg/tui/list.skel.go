// §source page/pkg/tui/list.go
package tui

import (
	"fmt"
	"strings"

	tea "github.com/charmbracelet/bubbletea"

	"github.com/feb/relay/pkg/keymap"
)

// keyMsgFor constructs a tea.KeyMsg from a key description string.
func keyMsgFor(s string) tea.KeyMsg {
// §.splinter/page/pkg/tui/list/keyMsgFor.fs
}

// HelpItem is one command row in the help pane.
type HelpItem struct {
	Key    string
	Desc   string
	Manual string
}

// helpLabel is what a row shows in its first column: the live key, or the live
// command name with the command-mode prefix drawn in front of it.
func (m Model) helpLabel(id string) string {
// §.splinter/page/pkg/tui/list/Model.helpLabel.fs
}

// helpListItems renders the help pane from the keymap registry, so every row
// shows the binding the user actually has and a new action cannot ship without
// a help row. The selected row carries the edit affordance, or the edit in
// progress.
func (m Model) helpListItems() []ListItem {
// §.splinter/page/pkg/tui/list/Model.helpListItems.fs
}

// listItems returns the active list depending on mode.
func (m Model) listItems() []ListItem {
// §.splinter/page/pkg/tui/list/Model.listItems.fs
}

// ListItem is one row in the generic list renderer.
type ListItem struct {
	Icon   string
	Title  string
	State  string
	Detail string
}

// RenderList renders items with cursor highlight and optional manual expansion.
func RenderList(items []ListItem, cursor, detail int, width int) string {
// §.splinter/page/pkg/tui/list/RenderList.fs
}
