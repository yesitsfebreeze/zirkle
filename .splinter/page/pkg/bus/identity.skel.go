// §source page/pkg/bus/identity.go
// Package bus implements the AMP Local Bus — Ed25519-signed envelopes
// delivered to local filesystem inboxes.
package bus

import (
	"crypto/ed25519"
	"crypto/rand"
	"encoding/hex"
	"fmt"
)

// Identity holds an Ed25519 keypair used to sign and verify envelopes.
type Identity struct {
	priv ed25519.PrivateKey
	pub  ed25519.PublicKey
}

// GenerateIdentity creates a new Ed25519 identity from a random seed.
func GenerateIdentity() (*Identity, error) {
// §.splinter/page/pkg/bus/identity/GenerateIdentity.fs
}

// Sign signs data with the identity's private key.
func (id *Identity) Sign(data []byte) []byte {
// §.splinter/page/pkg/bus/identity/Identity.Sign.fs
}

// Verify reports whether sig is a valid signature of data by this identity.
func (id *Identity) Verify(data, sig []byte) bool {
// §.splinter/page/pkg/bus/identity/Identity.Verify.fs
}

// Fingerprint returns the hex-encoded public key — used as the agent's
// bus address.
func (id *Identity) Fingerprint() string {
// §.splinter/page/pkg/bus/identity/Identity.Fingerprint.fs
}

// PublicKey returns the raw public key bytes.
func (id *Identity) PublicKey() ed25519.PublicKey {
// §.splinter/page/pkg/bus/identity/Identity.PublicKey.fs
}
