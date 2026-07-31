// §source page/pkg/tui/tui.go
// Package tui — job-orchestration dashboard. Tree of running pods on top,
// live chat context of the selected pod below. Input talks to the selected pod.
package tui

import (
	"context"
	"errors"
	"math"
	"os"
	"path/filepath"
	"strconv"
	"strings"
	"time"

	"github.com/charmbracelet/bubbles/viewport"
	tea "github.com/charmbracelet/bubbletea"
	"github.com/charmbracelet/lipgloss"
	"github.com/feb/relay/pkg/keymap"
	"github.com/feb/relay/pkg/llm"
	"github.com/feb/relay/pkg/plan"
)

// Broadcast ticker timing. The divider bar is a plain separator except for
// rare marquee messages scrolled across it — an emergency channel.
const (
	broadcastInterval   = 5 * time.Minute       // window between broadcast checks
	broadcastScrollStep = 80 * time.Millisecond // marquee scroll cadence
)

// Version is the relay binary version, shown in the header.
const Version = "v0.1.0"

// PodView is a decoupled read-model for one pod.
type PodView struct {
	ID           string
	Prompt       string
	Mode         string
	State        string
	Recap        string // LLM-generated one-line summary
	Depth        int    // indentation (0 = root)
	HasChildren  bool
	HasQuestions bool // pod is waiting for user input
	CreatedAt    time.Time
}

// Source supplies the pod list (flat, tree-order).
type Source interface {
	List() ([]PodView, error)
}

// ConversationSource is an optional Source capability: load the persisted
// conversation for a pod so selecting it refills the chat pane with its
// history. Sources backed by a store implement this; in-memory test sources
// can omit it.
type ConversationSource interface {
	Conversation(id string) ([]ChatMsg, error)
}

// Commander dispatches a new job (called when user presses Enter).
type Commander interface {
	Run(ctx context.Context, prompt string) (string, error)
}

// StreamCommander extends Commander with streaming dispatch. RunStream emits
// LLM tokens and tool-call events through events as they arrive; the channel
// is closed by the caller after RunStream returns. Returns the final content
// string and error.
type StreamCommander interface {
	Commander
	RunStream(ctx context.Context, prompt string, events chan<- llm.StreamEvent) (string, error)
}

// PlanCommander extends Commander with plan-then-dispatch capabilities.
type PlanCommander interface {
	Commander
	Plan(ctx context.Context, prompt string) (*plan.Conversation, error)
	Approve(ctx context.Context, convID string) (*plan.Conversation, error)
	RunWorker(ctx context.Context, convID string) (string, error)
	ReWork(ctx context.Context, convID string, correction string) (string, error)
	GetConversation(convID string) (*plan.Conversation, error)
}

type doneRun struct {
	prompt, response string
	err              error
}

// Input mode for the prompt area, determined by the first character typed.
type inputMode int

const (
	modeNormal  inputMode = iota // default dispatch
	modeSearch                   // first char /
	modeCommand                  // first char :
	modeHelp                     // first char ?
)

// ChatMsg is one user or agent line in the chat log.
type ChatMsg struct {
	Role    string // "user" or "agent"
	Content string
}

// agentStreamMsg carries one LLM stream event and the channel to read the
// next. The Update loop processes the event, appends to the panes, and
// returns a command that reads the next event — standard bubbletea streaming.
type agentStreamMsg struct {
	ev       llm.StreamEvent
	ch       <-chan llm.StreamEvent
	resultCh <-chan doneRun
	prompt   string
}

// Model is the Bubble Tea dashboard.
type Model struct {
	src            Source
	cmdr           Commander
	views          []PodView
	collapsed      map[int]bool
	cursor         int
	err            string
	input          MultiInput
	busy           bool
	vp             viewport.Model // pods pane (bottom)
	vpTerminal     viewport.Model // right pane: terminal output (code + shell from current agent and sub-agents)
	vpChat         viewport.Model // left pane: conversation (user + agent text)
	ready          bool
	detail         string        // id of pod whose detail row is shown, "" = none
	history        []string      // dispatched prompts, most recent first
	histIdx        int           // -1 = not browsing history; 0 = most recent
	hist           PromptHistory // persistent prompt log; nil = in-memory only
	suggestion     string        // autocomplete suffix for the current input, "" = none
	search         bool          // true = search mode
	searchQ        string        // search query
	pane           int           // 0 = pods, 1 = input
	mode           inputMode
	winH           int                // last reported terminal height, for dynamic relayout
	chat           []ChatMsg          // conversation log for the selected pod
	terminal       []string           // left-pane lines: tool calls, state changes, command output
	thoughts       string             // right-pane: accumulated streaming agent text
	streaming      bool               // true while a stream is active
	loadedID       string             // pod id whose conversation is currently in m.chat, "" = none
	scriptLines    []ScriptLine       // user statusline scripts, refreshed each tick
	cancelFn       context.CancelFunc // aborts the in-flight dispatch; nil when idle
	help           bool               // help pane visible
	helpCur        int                // cursor in help list
	helpDetail     int                // -1 = none, else index of item showing manual
	statTime       string             // wall clock, updated by tick
	statLoad       string             // 1-min load average, updated by tick
	broadcasts     <-chan string      // emergency-channel intake; nil = inert
	bc             *broadcastMsg      // active marquee, nil = idle separator
	config         bool               // settings screen visible
	configCur      int                // cursor in the settings row list (see settingRows)
	highlightColor string             // primary highlight color hex
	attentionColor string             // attention color hex
	km             keymap.Map         // live key bindings and command names
	kmPath         string             // where keymap edits persist; "" = memory only
	wiz            *WizardModel       // guided tour, nil = not showing
	helpEdit       int                // 0 = none, 1 = capturing a key, 2 = typing an alias
	helpBuf        string             // alias being typed in the help pane
	tl             TimelineConfig     // frame headers in the pod list
	tlSave         func(TimelineConfig) error // persists timeline ticks; nil = memory only
}

// broadcastMsg is one scrolling message on the divider bar.
type broadcastMsg struct {
	text string
	pos  int // left offset; starts at width (off-screen right), exits at -len
}

// promptIcon returns the prompt character for the current input mode.
func promptIcon(m inputMode) string {
// §.splinter/page/pkg/tui/tui/promptIcon.fs
}

// never disagree, which is what made the old value-derived mode fall apart.
func (m *Model) applyMode(mode inputMode) {
// §.splinter/page/pkg/tui/tui/Model.applyMode.fs
}

// syncInput mirrors the buffer into the state other panes read, after any edit.
// The buffer holds the query verbatim; the mode lives in m.mode.
func (m *Model) syncInput() {
// §.splinter/page/pkg/tui/tui/Model.syncInput.fs
}

// updatePrompt sets the input prompt to the mode icon plus a space.
func (m *Model) updatePrompt() {
// §.splinter/page/pkg/tui/tui/Model.updatePrompt.fs
}

// modePlaceholder is the empty-buffer hint for each mode: it teaches the
// mode-switching prefixes (the bit that makes the first-char convention
// discoverable without the wizard) and says what the mode expects.
func modePlaceholder(mode inputMode) string {
// §.splinter/page/pkg/tui/tui/modePlaceholder.fs
}

// New builds a dashboard with tree view + job dispatch input.
func New(src Source, cmdr Commander, broadcasts <-chan string) Model {
// §.splinter/page/pkg/tui/tui/New.fs
}

// WithHistory attaches a persistent prompt log. Without one the dashboard
// still completes from prompts typed in this session, but forgets on exit.
func (m Model) WithHistory(h PromptHistory) Model {
// §.splinter/page/pkg/tui/tui/Model.WithHistory.fs
}

// WithTimeline sets the pod-list frame headers. A zero-value config (Enabled
// false) renders the list with no headers at all.
func (m Model) WithTimeline(tl TimelineConfig) Model {
// §.splinter/page/pkg/tui/tui/Model.WithTimeline.fs
}

// WithTimelineSave attaches the writer that persists a tick in the settings
// screen back to the config file.
func (m Model) WithTimelineSave(save func(TimelineConfig) error) Model {
// §.splinter/page/pkg/tui/tui/Model.WithTimelineSave.fs
}

// WithTimelineSave attaches the writer that persists a settings-screen tick to
// the config file. Without one the ticks last for the session only, which is
// what the tests want.

// WithKeymap attaches the user's bindings and the file they persist to. An
// empty path keeps edits in memory, which is what the tests want. A map whose
// tour has not run opens the dashboard on the tour.
func (m Model) WithKeymap(km keymap.Map, path string) Model {
// §.splinter/page/pkg/tui/tui/Model.WithKeymap.fs
}

// Run launches the dashboard. Returns when user quits.
// Run starts the dashboard. themeColors overrides the ANSI-slot palette with
// truecolour hexes when [theme].custom is true in the config; nil or empty
// defers to the terminal's ANSI row. See WithTheme for the recognised keys.
// km carries the user's bindings; an untoured map opens the guided tour first.
// tlSave persists a settings-screen tick back to the config file; nil keeps the
// ticks in memory for the session.
func Run(src Source, cmdr Commander, broadcasts <-chan string, hist PromptHistory, themeColors map[string]string, km keymap.Map, kmPath string, tl TimelineConfig, tlSave func(TimelineConfig) error) error {
// §.splinter/page/pkg/tui/tui/Run.fs
}

// promptsMsg carries the persisted prompt log, newest first.
type promptsMsg []string

// loadPrompts reads the persisted prompt log into the model.
func (m Model) loadPrompts() tea.Cmd {
// §.splinter/page/pkg/tui/tui/Model.loadPrompts.fs
}

// recordPrompt persists one dispatched prompt off the UI goroutine.
func (m Model) recordPrompt(prompt string) tea.Cmd {
// §.splinter/page/pkg/tui/tui/Model.recordPrompt.fs
}

type refreshMsg []PodView

// conversationMsg carries the persisted conversation for a pod, loaded when
// the user selects it so the chat pane refills with that session's history.
type conversationMsg struct {
	id   string
	msgs []ChatMsg
	err  error
}

// statuslineMsg carries the latest user statusline-script output, refreshed on
// each tick so dropped-in scripts appear without a restart.
type statuslineMsg []ScriptLine

func (m Model) load() tea.Cmd {
// §.splinter/page/pkg/tui/tui/Model.load.fs
}

// dispatch returns the command that runs one prompt. When the commander
// implements StreamCommander, it starts a streaming goroutine and returns a
// command that reads the first event; subsequent events arrive via the
// agentStreamMsg → readNextStream chain in Update.
func (m Model) dispatch(prompt string) (tea.Cmd, context.CancelFunc) {
// §.splinter/page/pkg/tui/tui/Model.dispatch.fs
}

// histPrev walks one step back through the prompt log into the buffer.
func (m *Model) histPrev() {
// §.splinter/page/pkg/tui/tui/Model.histPrev.fs
}

// histNext walks one step forward, past the newest entry back to an empty box.
func (m *Model) histNext() {
// §.splinter/page/pkg/tui/tui/Model.histNext.fs
}

// setInput replaces the buffer and parks the cursor at its end.
func (m *Model) setInput(val string) {
// §.splinter/page/pkg/tui/tui/Model.setInput.fs
}

// submitInput dispatches the current input content and clears it. The buffer
// never holds a mode prefix, so its value is the prompt.
func (m Model) submitInput() (tea.Model, tea.Cmd) {
// §.splinter/page/pkg/tui/tui/Model.submitInput.fs
}

// runCommand resolves the first typed word through the keymap and runs the
// action it names. Reports handled=false when the word names nothing, or names
// an action the dashboard has no screen for, so the text stays a prompt.
func (m Model) runCommand(fields []string) (tea.Model, tea.Cmd, bool) {
// §.splinter/page/pkg/tui/tui/Model.runCommand.fs
}

// imagePath reports whether s looks like an image file path or URI.
func imagePath(s string) bool {
// §.splinter/page/pkg/tui/tui/imagePath.fs
}

// detectImagePaste checks the input for pasted image references after an
// update. Called when the input value may have changed from a paste event.
func (m Model) detectImagePaste() tea.Cmd {
// §.splinter/page/pkg/tui/tui/Model.detectImagePaste.fs
}

// relayout distributes terminal height: chat on top, pods + input below.
// Input grows with content up to half the bottom section; pods shrink to fit.
// relayout distributes terminal height: header on top, chat above input,
// separator, then pods below. Input grows from center, taking equally
// from chat (above) and pods (below).
func (m *Model) relayout() {
// §.splinter/page/pkg/tui/tui/Model.relayout.fs
}

type tickMsg time.Time

func tick() tea.Cmd {
// §.splinter/page/pkg/tui/tui/tick.fs
}

// broadcastTickMsg fires every broadcastInterval to check for a queued message.
type broadcastTickMsg time.Time

func broadcastTick() tea.Cmd {
// §.splinter/page/pkg/tui/tui/broadcastTick.fs
}

// scrollTickMsg advances an active marquee one rune left.
type scrollTickMsg struct{}

func scrollTick() tea.Cmd {
// §.splinter/page/pkg/tui/tui/scrollTick.fs
}

func readLoad() string {
// §.splinter/page/pkg/tui/tui/readLoad.fs
}

func (m Model) runningCount() int {
// §.splinter/page/pkg/tui/tui/Model.runningCount.fs
}

func (m Model) activeCount() int {
// §.splinter/page/pkg/tui/tui/Model.activeCount.fs
}

// Init starts the first load + tick.
func (m Model) Init() tea.Cmd {
// §.splinter/page/pkg/tui/tui/Model.Init.fs
}

// reverseGroups reverses the order of top-level pod groups, keeping children
// grouped after their parent. Newest pods appear first (at top of the list).
func reverseGroups(views []PodView) []PodView {
// §.splinter/page/pkg/tui/tui/reverseGroups.fs
}

// visible returns view-indices of non-collapsed items, optionally filtered by
// the active search query.
func (m Model) visible() []int {
// §.splinter/page/pkg/tui/tui/Model.visible.fs
}

func (m Model) matchSearch(idx int) bool {
// §.splinter/page/pkg/tui/tui/Model.matchSearch.fs
}

// selectedIdx maps cursor (visible row) to view index, or -1 when nothing is
// selectable.
func (m Model) selectedIdx() int {
// §.splinter/page/pkg/tui/tui/Model.selectedIdx.fs
}

// selectedID returns the id of the selected pod, or "" when none is selected
// (nothing selectable, or the "+ new" affordance row).
func (m Model) selectedID() string {
// §.splinter/page/pkg/tui/tui/Model.selectedID.fs
}

// detailIdx resolves the open detail pod's id to a views index, or -1 when
// none is open or the pod is no longer present (deleted / reordered out). The
// detail pointer is an id, not an index, so it survives reverseGroups
// reordering the tree between refreshes.
func (m Model) detailIdx() int {
// §.splinter/page/pkg/tui/tui/Model.detailIdx.fs
}

// loadConversation fetches the persisted conversation for the selected pod,
// but only when it differs from what's already loaded and no stream is live.
// Returns nil (no-op) when the source can't supply conversations or there's
// nothing new to load.
func (m Model) loadConversation() tea.Cmd {
// §.splinter/page/pkg/tui/tui/Model.loadConversation.fs
}

// collectStatuslines runs the user statusline scripts off the filesystem and
// returns their output. No-op (nil) when no script dir is configured. Env
// exposes the live dashboard state so scripts can render context without
// parsing the TUI.
func (m Model) collectStatuslines() tea.Cmd {
// §.splinter/page/pkg/tui/tui/Model.collectStatuslines.fs
}

// scriptLineCount is the number of user statusline rows currently rendered
// (above + below the input), so relayout can reserve space for them.
func (m Model) scriptLineCount() int {
// §.splinter/page/pkg/tui/tui/Model.scriptLineCount.fs
}

func boolStr(b bool) string {
// §.splinter/page/pkg/tui/tui/boolStr.fs
}

func selectedState(m Model) string {
// §.splinter/page/pkg/tui/tui/selectedState.fs
}
func (m *Model) clampCursor() {
// §.splinter/page/pkg/tui/tui/Model.clampCursor.fs
}

// Update handles messages.
func (m Model) Update(msg tea.Msg) (tea.Model, tea.Cmd) {
// §.splinter/page/pkg/tui/tui/Model.Update.fs
}

// readNextStream is the tea.Cmd that reads one event from the stream
// channel and converts it to an agentStreamMsg. When the channel closes,
// it reads the final doneRun from the result channel. The Update loop calls
// this after processing each event, creating the read-next-event chain.
func readNextStream(ch <-chan llm.StreamEvent, resultCh <-chan doneRun, prompt string) tea.Cmd {
// §.splinter/page/pkg/tui/tui/readNextStream.fs
}

// afterCursorMove re-renders both panes.
func (m Model) afterCursorMove() (tea.Model, tea.Cmd) {
// §.splinter/page/pkg/tui/tui/Model.afterCursorMove.fs
}

var errNoCommander = errors.New("no commander wired: cannot dispatch")

type errMsg string

func (e errMsg) Error() string {
// §.splinter/page/pkg/tui/tui/errMsg.Error.fs
}

func (m Model) Views() []PodView {
// §.splinter/page/pkg/tui/tui/Model.Views.fs
}
func (m Model) Err() string      {
// §.splinter/page/pkg/tui/tui/Model.Err.fs
}

// Reference hexes for the ANSI 0-8 palette (see theme.go). Styles never read
// these — they render through ANSI indices so the terminal's own first row
// wins. These exist for the showcase swatches and the generated terminal
// configs under themes/.
const (
	colorCarbon     = "#121212" // ANSI 0 — surface
	colorRed        = "#FF3D81" // ANSI 1 — failure
	colorLime       = "#C7F000" // ANSI 2 — primary
	colorChartreuse = "#C4FF4D" // ANSI 3 — warning
	colorBlue       = "#2F80ED" // ANSI 4 — notification
	colorViolet     = "#BA8CFF" // ANSI 5 — accent
	colorMist       = "#A2B1B1" // ANSI 6 — secondary text
	colorWhite      = "#FFFFFF" // ANSI 7 — default foreground
	colorRock       = "#737575" // ANSI 8 — muted
	colorBlack      = "#000000" // background — terminal ground
)

// Styles bind roles to ANSI slots. Bold plus an index is the whole vocabulary:
// no truecolor, no Faint (which renders inconsistently on near-black grounds).
var (
	onPrimary      = readableOn(ansiMagenta)
	surface        = lipgloss.Color(ansiBlack)
	rule           = lipgloss.Color(ansiGrey)
	fg             = lipgloss.Color(ansiWhite)
	muted          = lipgloss.Color(ansiGrey)
	secondary      = lipgloss.Color(ansiCyan)
	failure        = lipgloss.Color(ansiRed)
	attention      = lipgloss.Color(ansiBlue)
	attentionStyle = lipgloss.NewStyle().Bold(true).Foreground(attention)
	amber          = lipgloss.Color(ansiMagenta)
	headerStyle    = lipgloss.NewStyle().Bold(true).Foreground(fg)
	footerStyle    = lipgloss.NewStyle().Foreground(muted)
	selectedStyle  = lipgloss.NewStyle().Bold(true).Foreground(onPrimary).Background(amber)
	mutedStyle     = lipgloss.NewStyle().Foreground(muted)
	separatorStyle = lipgloss.NewStyle().Foreground(rule)
	activeStyle    = lipgloss.NewStyle().Bold(true).Foreground(amber)
	broadcastStyle = lipgloss.NewStyle().Bold(true).Foreground(onPrimary).Background(amber)
	errorStyle     = lipgloss.NewStyle().Bold(true).Foreground(failure)
	detailStyle    = lipgloss.NewStyle().Foreground(secondary)
)

// Presets cycled by :config. ANSI indices, not hexes — swapping the highlight
// to blue means "use slot 4", whatever slot 4 currently is.
var (
	highlightPresets = []string{ansiMagenta, ansiGreen, ansiBlue, ansiCyan, ansiYellow, ansiRed}
	attentionPresets = []string{ansiBlue, ansiMagenta, ansiRed, ansiYellow, ansiCyan, ansiGreen}
)

// normalizeColor accepts an ANSI index ("0"-"8", the theme's own vocabulary)
// or a truecolor hex with or without the leading #. An index stays an index:
// hexing it here would pin the dashboard to one palette and defeat the point.
func normalizeColor(c string) string {
// §.splinter/page/pkg/tui/tui/normalizeColor.fs
}

func isANSIIndex(c string) bool {
// §.splinter/page/pkg/tui/tui/isANSIIndex.fs
}

// themeName maps a color value back to its palette slot, so the settings
// screen can say "2 green" instead of an opaque index.
func themeName(c string) string {
// §.splinter/page/pkg/tui/tui/themeName.fs
}

// themeHex resolves a color value to the reference hex, for swatches that must
// paint an actual colour (the settings preview) rather than defer to the
// terminal.
func themeHex(c string) string {
// §.splinter/page/pkg/tui/tui/themeHex.fs
}

// contrastForeground picks a foreground that stays readable over bg, which may
// be an ANSI index or a #hex. ANSI indices resolve through the reference
// palette so the choice tracks :config swaps; a hex is parsed directly. The
// rule is the WCAG relative-luminance threshold: bg lighter than ~0.18 gets
// black text, darker gets white — the same cut the contrast ratio uses to
// decide which of black/white wins.
func contrastForeground(bg string) string {
// §.splinter/page/pkg/tui/tui/contrastForeground.fs
}

// parseHex splits a #RRGGBB string into 8-bit channels. Returns ok=false for
// anything malformed so callers can fall back to a safe default.
func parseHex(hex string) (r, g, b uint8, ok bool) {
// §.splinter/page/pkg/tui/tui/parseHex.fs
}

// relativeLuminance is the WCAG sRGB luminance: each channel linearised, then
// weighted by the perceptual coefficients.
func relativeLuminance(r, g, b uint8) float64 {
// §.splinter/page/pkg/tui/tui/relativeLuminance.fs
}

func linear(c uint8) float64 {
// §.splinter/page/pkg/tui/tui/linear.fs
}

func (m *Model) updateColors(h, a string) {
// §.splinter/page/pkg/tui/tui/Model.updateColors.fs
}

// rebuildBaseStyles reconstructs the styles that bind to the non-primary
// palette slots (header, footer, muted, separator, detail). updateColors
// does not touch these — they only change when WithTheme overrides a slot —n// so they have their own rebuild called from both paths.
func rebuildBaseStyles() {
// §.splinter/page/pkg/tui/tui/rebuildBaseStyles.fs
}

// WithTheme overlays truecolour hexes from the config [theme].colors map onto
// the palette. Recognised keys: foreground, on_primary, primary, attention,
// muted, secondary, failure, surface, rule. Missing keys keep the ANSI-slot
// default, so a custom theme can fix one colour or all of them. on_primary
// overrides the computed invert foreground (readableOn); absent it, the fg on
// primary is calculated from the primary's luminance. Nil/empty = no-op.
// applyThemeColors mutates the package style vars from a config [theme].colors
// map and returns the resolved primary/attention so callers with model state
// (WithTheme) can mirror it. Recognised keys: foreground, on_primary, primary,
// attention, muted, secondary, failure, surface, rule. Missing keys keep the
// ANSI-slot default. on_primary overrides the computed invert fg. Empty map is
// a no-op. Shared by WithTheme (daemon) and RunShowcase (gallery).
func applyThemeColors(colors map[string]string) (h, a string) {
// §.splinter/page/pkg/tui/tui/applyThemeColors.fs
}

func (m Model) WithTheme(colors map[string]string) Model {
// §.splinter/page/pkg/tui/tui/Model.WithTheme.fs
}

func (m *Model) cycleColor(setting int, dir int) {
// §.splinter/page/pkg/tui/tui/Model.cycleColor.fs
}
