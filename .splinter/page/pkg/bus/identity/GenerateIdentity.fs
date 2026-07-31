// §head page/pkg/bus/identity.go:19-25 GenerateIdentity
// §sig func GenerateIdentity() (*Identity, error)
	pub, priv, err := ed25519.GenerateKey(rand.Reader)
	if err != nil {
		return nil, fmt.Errorf("bus: generate identity: %w", err)
	}
	return &Identity{priv: priv, pub: pub}, nil
// §foot page/pkg/bus/identity.go GenerateIdentity