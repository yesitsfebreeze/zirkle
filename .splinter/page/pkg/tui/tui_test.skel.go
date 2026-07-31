// §source page/pkg/tui/tui_test.go
package tui

import (
	"context"
	"strings"
	"sync"
	"testing"
	"time"

	tea "github.com/charmbracelet/bubbletea"
	"github.com/feb/relay/pkg/plan"
)

type mockSource struct {
	views []PodView
	err   error
}

func (m mockSource) List() ([]PodView, error) {
// §.splinter/page/pkg/tui/tui_test/mockSource.List.fs
}

func testViews() []PodView {
// §.splinter/page/pkg/tui/tui_test/testViews.fs
}

func TestRefreshPopulatesViews(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestRefreshPopulatesViews.fs
}

func TestCollapse(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestCollapse.fs
}

func TestEmptyListView(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestEmptyListView.fs
}

// Left on an empty input folds the selected subtree in place. It used to flip
// the model into a nav mode that reset the cursor to the top and blurred the
// input; there is no mode now, only the highlighted row.
func TestLeftFoldsSelectedSubtree(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestLeftFoldsSelectedSubtree.fs
}

// The highlight is rendered into the viewport content, so a fold or a cursor
// move must push fresh content — otherwise the key press looks like a no-op.
func TestFoldRerendersViewport(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestFoldRerendersViewport.fs
}

// Arrow keys with text in the input belong to the text cursor, not the tree.
func TestLeftWithTextDoesNotFold(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestLeftWithTextDoesNotFold.fs
}

// A row is always highlighted, and the newest pod (top row) starts selected.
func TestSelectionHighlightsNewestRow(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestSelectionHighlightsNewestRow.fs
}

// Up/down move the highlight regardless of what the input holds.
func TestUpDownMoveSelection(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestUpDownMoveSelection.fs
}

// Up at the top of the pod list exits to the input pane.
func TestUpAtTopExitsToInput(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestUpAtTopExitsToInput.fs
}

func TestViewportHeightLeavesNoGap(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestViewportHeightLeavesNoGap.fs
}

func TestTreeTopAnchored(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestTreeTopAnchored.fs
}

// Arrows before the first refreshMsg lands leave views empty. selectedIdx used
// to return 0 there, so ←/→ indexed views[0] on an empty slice:
// "index out of range [0] with length 0".
func TestNavKeysOnEmptyViewsDoNotPanic(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestNavKeysOnEmptyViewsDoNotPanic.fs
}

func TestSelectedIdxReportsEmpty(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestSelectedIdxReportsEmpty.fs
}

func TestSelectedIdxClampsPastEnd(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestSelectedIdxClampsPastEnd.fs
}

type recordingCommander struct {
	mu      sync.Mutex
	prompts []string
}

func (c *recordingCommander) Run(ctx context.Context, prompt string) (string, error) {
// §.splinter/page/pkg/tui/tui_test/recordingCommander.Run.fs
}

// Enter must clear the input and mark the model busy. submit() did both on a
// value receiver, so the writes landed on a discarded copy: the typed text
// stayed on screen and busy never set, which read as "Enter does nothing".
func TestEnterClearsInputAndMarksBusy(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestEnterClearsInputAndMarksBusy.fs
}

func TestEnterOnEmptyInputDoesNothing(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestEnterOnEmptyInputDoesNothing.fs
}

// doneRun must clear busy, or the input locks up after the first job.
func TestDoneRunClearsBusy(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestDoneRunClearsBusy.fs
}

// Escape pops one level of navigation toward the input (home) per turn.
func TestEscapePopsNavStack(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestEscapePopsNavStack.fs
}

// A nil commander must surface an error, not panic.
func TestDispatchWithoutCommander(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestDispatchWithoutCommander.fs
}

func TestConfigScreenAndColorUpdates(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestConfigScreenAndColorUpdates.fs
}

func TestTickRefreshAndDetailPersistence(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestTickRefreshAndDetailPersistence.fs
}

func TestPlusNewButtonPreservedAtTop(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestPlusNewButtonPreservedAtTop.fs
}

func TestEnterOnLeafPodEntersChat(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestEnterOnLeafPodEntersChat.fs
}

func TestSymbolForState(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestSymbolForState.fs
}

type mockPlanCommander struct {
	recordingCommander
	planCalled    bool
	approveCalled bool
	reworkCalled  bool
}

func (m *mockPlanCommander) Plan(ctx context.Context, prompt string) (*plan.Conversation, error) {
// §.splinter/page/pkg/tui/tui_test/mockPlanCommander.Plan.fs
}

func (m *mockPlanCommander) Approve(ctx context.Context, convID string) (*plan.Conversation, error) {
// §.splinter/page/pkg/tui/tui_test/mockPlanCommander.Approve.fs
}

func (m *mockPlanCommander) RunWorker(ctx context.Context, convID string) (string, error) {
// §.splinter/page/pkg/tui/tui_test/mockPlanCommander.RunWorker.fs
}

func (m *mockPlanCommander) ReWork(ctx context.Context, convID string, correction string) (string, error) {
// §.splinter/page/pkg/tui/tui_test/mockPlanCommander.ReWork.fs
}

func (m *mockPlanCommander) GetConversation(convID string) (*plan.Conversation, error) {
// §.splinter/page/pkg/tui/tui_test/mockPlanCommander.GetConversation.fs
}

func TestPlanCommanderFlow(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestPlanCommanderFlow.fs
}


func TestSubmitSetsTerminalAndThoughts(t *testing.T) {
// §.splinter/page/pkg/tui/tui_test/TestSubmitSetsTerminalAndThoughts.fs
}
