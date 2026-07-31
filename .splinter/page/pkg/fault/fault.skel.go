// §source page/pkg/fault/fault.go
// Package fault records runtime errors and panics so a crash leaves a trace
// instead of a silence. Recording never fails a caller: a fault is already a
// bad day, and a fault-recording bug must not become a second one.
package fault

import (
	"fmt"
	"log"
	"os"
	"runtime/debug"
	"sync"
)

const (
	KindPanic = "panic"
	KindError = "error"
)

// Sink persists faults. pkg/store implements it.
type Sink interface {
	RecordFault(podID, kind, where, msg, stack string) error
}

// stderr is deliberately its own logger: the daemon sends the normal log to
// io.Discard when --debug is off, and a crash must still reach the operator.
var (
	stderr = log.New(os.Stderr, "relay: ", log.LstdFlags)
	mu     sync.Mutex
)

// Guard recovers a panic, records it, and lets the goroutine die quietly
// instead of taking the process with it. Use as the first defer in every
// goroutine: defer fault.Guard(sink, podID, "webhook.deliver")
func Guard(sink Sink, podID, where string) {
// §.splinter/page/pkg/fault/fault/Guard.fs
}

// Recovered records a panic the caller already recovered itself, for sites
// that must run their own cleanup before handing the fault over. Guard is the
// common path; this is the seam for callers that cannot delegate recovery.
func Recovered(sink Sink, podID, where, msg, stack string) {
// §.splinter/page/pkg/fault/fault/Recovered.fs
}

// Record stores a non-fatal runtime error. Nil errors are ignored so callers
// can hand it a result unconditionally.
func Record(sink Sink, podID, where string, err error) {
// §.splinter/page/pkg/fault/fault/Record.fs
}

// persist swallows sink failures: it reports them to stderr and moves on. A
// dead database must not turn a recorded panic into an unrecorded one.
func persist(sink Sink, podID, kind, where, msg, stack string) {
// §.splinter/page/pkg/fault/fault/persist.fs
}
