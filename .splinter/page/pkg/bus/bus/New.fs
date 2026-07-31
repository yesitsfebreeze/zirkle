// §head page/pkg/bus/bus.go:25-31 New
// §sig func New(identity *Identity, spoolDir string) *Bus
	return &Bus{
		identity: identity,
		spool:    spoolDir,
		inbox:    filepath.Join(spoolDir, identity.Fingerprint()),
	}
// §foot page/pkg/bus/bus.go New