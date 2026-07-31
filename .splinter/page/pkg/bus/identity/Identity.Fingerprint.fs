// §head page/pkg/bus/identity.go:39-41 Identity.Fingerprint
// §sig func (id *Identity) Fingerprint() string
	return hex.EncodeToString(id.pub)
// §foot page/pkg/bus/identity.go Identity.Fingerprint