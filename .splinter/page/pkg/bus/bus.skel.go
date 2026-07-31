// §source page/pkg/bus/bus.go
package bus

import (
	"encoding/json"
	"fmt"
	"os"
	"path/filepath"
	"strings"

	"github.com/google/uuid"
)

// Bus delivers signed envelopes between agents via local filesystem inboxes.
// Each agent identity maps to a directory under the spool root:
//
//	<spool>/<fingerprint>/  — inbox for that identity
type Bus struct {
	identity *Identity
	spool    string // root spool directory
	inbox    string // this identity's inbox
}

// New creates a Bus that signs with the given identity and stores messages
// under spoolDir. The inbox directory is created lazily on first Poll.
func New(identity *Identity, spoolDir string) *Bus {
// §.splinter/page/pkg/bus/bus/New.fs
}

// Send signs env for the sender identity and writes it to the recipient's
// inbox directory. to is the recipient's fingerprint (hex-encoded public key).
func (b *Bus) Send(to string, env Envelope) error {
// §.splinter/page/pkg/bus/bus/Bus.Send.fs
}

// Poll reads this bus identity's inbox, verifies every envelope, and returns
// the valid ones. Invalid envelopes (bad signature, unparseable) are skipped.
// Poll is non-blocking — returns empty slice immediately when inbox is empty
// or missing.
func (b *Bus) Poll() ([]Envelope, error) {
// §.splinter/page/pkg/bus/bus/Bus.Poll.fs
}

// Ack removes a processed envelope from the inbox by its ID (the filename
// stem returned by Poll).
func (b *Bus) Ack(id string) error {
// §.splinter/page/pkg/bus/bus/Bus.Ack.fs
}

// Identity returns the bus's identity.
func (b *Bus) Identity() *Identity {
// §.splinter/page/pkg/bus/bus/Bus.Identity.fs
}
