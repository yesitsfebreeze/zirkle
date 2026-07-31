// §source page/pkg/store/store_test.go
package store

import (
	"database/sql"
	"fmt"
	"path/filepath"
	"testing"
	"time"
)

func TestStoreLifecycle(t *testing.T) {
// §.splinter/page/pkg/store/store_test/TestStoreLifecycle.fs
}

func TestConversationLifecycle(t *testing.T) {
// §.splinter/page/pkg/store/store_test/TestConversationLifecycle.fs
}

func TestRecordAndListFaults(t *testing.T) {
// §.splinter/page/pkg/store/store_test/TestRecordAndListFaults.fs
}

func TestFaultsLimit(t *testing.T) {
// §.splinter/page/pkg/store/store_test/TestFaultsLimit.fs
}

// A database created before the recap column shipped must gain it on Open.
// CREATE TABLE IF NOT EXISTS does nothing for an existing table, so the real
// user database carried the original 6 columns while user_version claimed 2
// and every Save failed with "no such column: recap".
func TestOpenMigratesPreRecapDatabase(t *testing.T) {
// §.splinter/page/pkg/store/store_test/TestOpenMigratesPreRecapDatabase.fs
}

// A database written before the oorb→relay rename names the lifecycle table
// "oorb", the checkpoint key "oorb_id" and the fault key "orb_id". Open must
// carry it over instead of leaving the rows behind an unreachable name.
func TestOpenMigratesPreRenameDatabase(t *testing.T) {
// §.splinter/page/pkg/store/store_test/TestOpenMigratesPreRenameDatabase.fs
}

// Opening twice must be a no-op the second time.
func TestMigrationIsIdempotent(t *testing.T) {
// §.splinter/page/pkg/store/store_test/TestMigrationIsIdempotent.fs
}

func TestPromptHistory(t *testing.T) {
// §.splinter/page/pkg/store/store_test/TestPromptHistory.fs
}

func TestPromptHistoryTrimsToLimit(t *testing.T) {
// §.splinter/page/pkg/store/store_test/TestPromptHistoryTrimsToLimit.fs
}

// Executions are the workspace memory: a recorded subpod run must be findable
// by prompt, summary, or output content, newest first.
func TestExecutionRecordAndSearch(t *testing.T) {
// §.splinter/page/pkg/store/store_test/TestExecutionRecordAndSearch.fs
}
