// §source page/pkg/store/store.go
package store

import (
	"database/sql"
	"fmt"
	"strings"
	"time"

	_ "modernc.org/sqlite"
)

type Pod struct {
	ID        string
	Prompt    string
	Mode      string
	State     string
	Recap     string // LLM-generated one-line summary
	CreatedAt time.Time
	UpdatedAt time.Time
}

type ConversationRecord struct {
	ID           string
	State        string
	Intent       string
	ApprovedPlan string
	WorkerID     string
	Recap        string
	Output       string
	History      string
	CreatedAt    time.Time
	UpdatedAt    time.Time
}

const schema = `
CREATE TABLE IF NOT EXISTS pod (
    id         TEXT PRIMARY KEY,
    prompt     TEXT NOT NULL,
    mode       TEXT NOT NULL,
    state      TEXT NOT NULL DEFAULT 'created',
    recap      TEXT NOT NULL DEFAULT '',
    created_at INTEGER NOT NULL,
    updated_at INTEGER NOT NULL
);
CREATE TABLE IF NOT EXISTS checkpoint (
    pod_id    TEXT NOT NULL,
    turn       INTEGER NOT NULL,
    state      BLOB NOT NULL,
    created_at INTEGER NOT NULL,
    PRIMARY KEY (pod_id, turn)
);
CREATE TABLE IF NOT EXISTS fault (
    id         INTEGER PRIMARY KEY AUTOINCREMENT,
    pod_id     TEXT NOT NULL DEFAULT '',
    kind       TEXT NOT NULL,
    site       TEXT NOT NULL,
    msg        TEXT NOT NULL,
    stack      TEXT NOT NULL DEFAULT '',
    created_at INTEGER NOT NULL
);
CREATE TABLE IF NOT EXISTS prompt_history (
    id         INTEGER PRIMARY KEY AUTOINCREMENT,
    prompt     TEXT NOT NULL,
    created_at INTEGER NOT NULL
);
CREATE TABLE IF NOT EXISTS execution (
    id         INTEGER PRIMARY KEY AUTOINCREMENT,
    parent_id  TEXT NOT NULL DEFAULT '',
    prompt     TEXT NOT NULL,
    summary    TEXT NOT NULL DEFAULT '',
    output     TEXT NOT NULL DEFAULT '',
    success    INTEGER NOT NULL DEFAULT 0,
    tokens     INTEGER NOT NULL DEFAULT 0,
    model      TEXT NOT NULL DEFAULT '',
    created_at INTEGER NOT NULL
);
CREATE INDEX IF NOT EXISTS execution_created ON execution(created_at DESC);
CREATE TABLE IF NOT EXISTS conversation (
    id            TEXT PRIMARY KEY,
    state         TEXT NOT NULL DEFAULT 'created',
    intent        TEXT NOT NULL DEFAULT '{}',
    approved_plan TEXT NOT NULL DEFAULT '{}',
    worker_id     TEXT NOT NULL DEFAULT '',
    recap         TEXT NOT NULL DEFAULT '',
    output        TEXT NOT NULL DEFAULT '',
    history       TEXT NOT NULL DEFAULT '[]',
    created_at    INTEGER NOT NULL,
    updated_at    INTEGER NOT NULL
);
CREATE INDEX IF NOT EXISTS fault_created ON fault(created_at DESC);
PRAGMA user_version = 3;
`

// Fault is a recorded runtime error or panic. PodID is empty for daemon-level
// faults that belong to no single pod.
type Fault struct {
	ID        int64
	PodID     string
	Kind      string
	Site      string
	Msg       string
	Stack     string
	CreatedAt time.Time
}

type Store interface {
	Create(id, prompt, mode string) error
	Load(id string) (*Pod, error)
	Save(o *Pod) error
	List() ([]*Pod, error)
	Delete(id string) error
	Checkpoint(id string, turn int, state []byte) error
	LoadCheckpoint(id string, turn int) ([]byte, error)
	RecordFault(podID, kind, site, msg, stack string) error
	Faults(limit int) ([]*Fault, error)

	RecordPrompt(prompt string) error
	RecentPrompts(limit int) ([]string, error)

	RecordExecution(e *Execution) error
	SearchExecutions(query string, limit int) ([]*Execution, error)
	RecentExecutions(limit int) ([]*Execution, error)

	SaveConversation(c *ConversationRecord) error
	LoadConversation(id string) (*ConversationRecord, error)
	ListConversations() ([]*ConversationRecord, error)
	DeleteConversation(id string) error

	// LatestCheckpoint returns the newest saved turn state for a pod, or nil
	// when none exists. The TUI uses it to refill the conversation pane when an
	// old pod is selected.
	LatestCheckpoint(id string) ([]byte, error)
	// ExecutionsByParents returns subpod runs grouped by parent pod id, for
	// nesting subpods under their pod in the tree view.
	ExecutionsByParents(ids []string) (map[string][]*Execution, error)
}

type SQLite struct {
	db *sql.DB
}

func Open(path string) (*SQLite, error) {
// §.splinter/page/pkg/store/store/Open.fs
}

// addedColumns are columns introduced after a table first shipped. CREATE
// TABLE IF NOT EXISTS silently does nothing for a table that already exists,
// so a database created before a column was added never grows it and every
// query naming it fails with "no such column".
var addedColumns = []struct{ table, column, ddl string }{
	{"pod", "recap", "ALTER TABLE pod ADD COLUMN recap TEXT NOT NULL DEFAULT ''"},
}

// renameLegacy carries a database written before the oorb→relay rename, when
// one lifecycle row was an "oorb" rather than a "pod". It runs before the
// schema block: CREATE TABLE IF NOT EXISTS pod would otherwise create an empty
// table beside the real rows and the rename would then fail.
func renameLegacy(db *sql.DB) error {
// §.splinter/page/pkg/store/store/renameLegacy.fs
}

func hasTable(db *sql.DB, table string) (bool, error) {
// §.splinter/page/pkg/store/store/hasTable.fs
}

// migrate is driven by the columns actually present, not by user_version: the
// schema block bumped the version on databases it had not in fact migrated,
// so the stored version cannot be trusted to describe the real shape.
func migrate(db *sql.DB) error {
// §.splinter/page/pkg/store/store/migrate.fs
}

func hasColumn(db *sql.DB, table, column string) (bool, error) {
// §.splinter/page/pkg/store/store/hasColumn.fs
}

func (s *SQLite) Close() error {
// §.splinter/page/pkg/store/store/SQLite.Close.fs
}

func (s *SQLite) Create(id, prompt, mode string) error {
// §.splinter/page/pkg/store/store/SQLite.Create.fs
}

func (s *SQLite) Load(id string) (*Pod, error) {
// §.splinter/page/pkg/store/store/SQLite.Load.fs
}

func (s *SQLite) Save(o *Pod) error {
// §.splinter/page/pkg/store/store/SQLite.Save.fs
}

func (s *SQLite) List() ([]*Pod, error) {
// §.splinter/page/pkg/store/store/SQLite.List.fs
}

func (s *SQLite) Delete(id string) error {
// §.splinter/page/pkg/store/store/SQLite.Delete.fs
}

func (s *SQLite) Checkpoint(id string, turn int, state []byte) error {
// §.splinter/page/pkg/store/store/SQLite.Checkpoint.fs
}

func (s *SQLite) LoadCheckpoint(id string, turn int) ([]byte, error) {
// §.splinter/page/pkg/store/store/SQLite.LoadCheckpoint.fs
}

func (s *SQLite) RecordFault(podID, kind, site, msg, stack string) error {
// §.splinter/page/pkg/store/store/SQLite.RecordFault.fs
}

// Faults returns the most recent faults first. A limit <= 0 means 50.
func (s *SQLite) Faults(limit int) ([]*Fault, error) {
// §.splinter/page/pkg/store/store/SQLite.Faults.fs
}

// PromptHistoryLimit is how many dispatched prompts the log keeps. Older rows
// are dropped on insert, so frequency counts stay bounded to recent behaviour.
const PromptHistoryLimit = 256

// RecordPrompt appends one dispatched prompt and trims the log to the newest
// PromptHistoryLimit rows. Duplicates are kept: a repeated prompt is a repeated
// row, which is what makes "written often" countable.
func (s *SQLite) RecordPrompt(prompt string) error {
// §.splinter/page/pkg/store/store/SQLite.RecordPrompt.fs
}

// RecentPrompts returns dispatched prompts newest first, duplicates included.
func (s *SQLite) RecentPrompts(limit int) ([]string, error) {
// §.splinter/page/pkg/store/store/SQLite.RecentPrompts.fs
}

// Execution is one subpod run: the prompt it was given, the report it came
// back with, and whether it worked.  Executions are the agent system's
// memory — every pod in the workspace can search what every pod did before.
type Execution struct {
	ID        int64
	ParentID  string
	Prompt    string
	Summary   string
	Output    string
	Success   bool
	Tokens    int
	Model     string // which model ran this — the version for auditing
	CreatedAt time.Time
}

// RecordExecution appends one subpod run to the workspace history.
func (s *SQLite) RecordExecution(e *Execution) error {
// §.splinter/page/pkg/store/store/SQLite.RecordExecution.fs
}

// SearchExecutions finds past runs whose prompt, summary, or output matches
// query, newest first.  Empty query behaves like RecentExecutions.
func (s *SQLite) SearchExecutions(query string, limit int) ([]*Execution, error) {
// §.splinter/page/pkg/store/store/SQLite.SearchExecutions.fs
}

// RecentExecutions returns the newest runs regardless of content.
func (s *SQLite) RecentExecutions(limit int) ([]*Execution, error) {
// §.splinter/page/pkg/store/store/SQLite.RecentExecutions.fs
}

func scanExecutions(rows *sql.Rows) ([]*Execution, error) {
// §.splinter/page/pkg/store/store/scanExecutions.fs
}

func (s *SQLite) SaveConversation(c *ConversationRecord) error {
// §.splinter/page/pkg/store/store/SQLite.SaveConversation.fs
}

func (s *SQLite) LoadConversation(id string) (*ConversationRecord, error) {
// §.splinter/page/pkg/store/store/SQLite.LoadConversation.fs
}

func (s *SQLite) ListConversations() ([]*ConversationRecord, error) {
// §.splinter/page/pkg/store/store/SQLite.ListConversations.fs
}

func (s *SQLite) DeleteConversation(id string) error {
// §.splinter/page/pkg/store/store/SQLite.DeleteConversation.fs
}

// LatestCheckpoint returns the newest checkpoint state for a pod, or nil when
// the pod has no saved turns. The TUI refills the conversation pane from it.
func (s *SQLite) LatestCheckpoint(id string) ([]byte, error) {
// §.splinter/page/pkg/store/store/SQLite.LatestCheckpoint.fs
}

// ExecutionsByParents returns subpod runs keyed by parent pod id, ordered as
// they ran. Used to nest subpods under their pod in the tree view.
func (s *SQLite) ExecutionsByParents(ids []string) (map[string][]*Execution, error) {
// §.splinter/page/pkg/store/store/SQLite.ExecutionsByParents.fs
}
