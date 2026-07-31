// §head page/pkg/bus/identity.go:28-30 Identity.Sign
// §sig func (id *Identity) Sign(data []byte) []byte
	return ed25519.Sign(id.priv, data)
// §foot page/pkg/bus/identity.go Identity.Sign