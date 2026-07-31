// §head page/pkg/bus/identity.go:33-35 Identity.Verify
// §sig func (id *Identity) Verify(data, sig []byte) bool
	return ed25519.Verify(id.pub, data, sig)
// §foot page/pkg/bus/identity.go Identity.Verify