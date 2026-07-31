// §source page/pkg/bus/envelope.go
package bus

import (
	"crypto/ed25519"
	"encoding/base64"
	"encoding/hex"
	"encoding/json"
	"errors"
	"fmt"
)

// Envelope is a signed AMP message between agents.
// Signature covers the JSON of all fields except Signature itself.
type Envelope struct {
	// ID is set by Poll for Ack; not serialized.
	ID string `json:"-"`

	From        string `json:"from"`
	To          string `json:"to"`
	Subject     string `json:"subject"`
	Priority    string `json:"priority"`
	InReplyTo   string `json:"in_reply_to,omitempty"`
	Payload     []byte `json:"payload"`
	Signature   string `json:"signature,omitempty"`
	Fingerprint string `json:"fingerprint"`
}

// Sign signs the envelope with the given identity, setting Fingerprint and
// Signature.
func (env *Envelope) Sign(id *Identity) error {
// §.splinter/page/pkg/bus/envelope/Envelope.Sign.fs
}

// Verify checks the envelope's signature against its fingerprint.
// It returns true only when the signature is cryptographically valid.
func (env *Envelope) Verify() (bool, error) {
// §.splinter/page/pkg/bus/envelope/Envelope.Verify.fs
}
