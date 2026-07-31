// §source page/pkg/fault/fault_test.go
package fault

import (
	"errors"
	"strings"
	"sync"
	"testing"
)

type memSink struct {
	mu   sync.Mutex
	rows []row
	fail error
	boom bool
}

type row struct{ podID, kind, site, msg, stack string }

func (m *memSink) RecordFault(podID, kind, site, msg, stack string) error {
// §.splinter/page/pkg/fault/fault_test/memSink.RecordFault.fs
}

func (m *memSink) len() int {
// §.splinter/page/pkg/fault/fault_test/memSink.len.fs
}

func TestGuardRecordsPanicWithStack(t *testing.T) {
// §.splinter/page/pkg/fault/fault_test/TestGuardRecordsPanicWithStack.fs
}

// Guard must stop the panic: the caller returns normally instead of taking the
// process down.
func TestGuardStopsThePanic(t *testing.T) {
// §.splinter/page/pkg/fault/fault_test/TestGuardStopsThePanic.fs
}

func TestGuardIgnoresCleanReturn(t *testing.T) {
// §.splinter/page/pkg/fault/fault_test/TestGuardIgnoresCleanReturn.fs
}

func TestRecordIgnoresNilError(t *testing.T) {
// §.splinter/page/pkg/fault/fault_test/TestRecordIgnoresNilError.fs
}

func TestRecordStoresError(t *testing.T) {
// §.splinter/page/pkg/fault/fault_test/TestRecordStoresError.fs
}

// A fault is already a bad day. A broken sink must not become a second one.
func TestSinkFailureDoesNotPanic(t *testing.T) {
// §.splinter/page/pkg/fault/fault_test/TestSinkFailureDoesNotPanic.fs
}

func TestSinkPanicDoesNotEscape(t *testing.T) {
// §.splinter/page/pkg/fault/fault_test/TestSinkPanicDoesNotEscape.fs
}

func TestNilSinkIsAllowed(t *testing.T) {
// §.splinter/page/pkg/fault/fault_test/TestNilSinkIsAllowed.fs
}
