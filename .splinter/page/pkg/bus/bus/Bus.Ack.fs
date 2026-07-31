// §head page/pkg/bus/bus.go:107-113 Bus.Ack
// §sig func (b *Bus) Ack(id string) error
	path := filepath.Join(b.inbox, id+".env")
	if err := os.Remove(path); err != nil {
		return fmt.Errorf("bus ack: %w", err)
	}
	return nil
// §foot page/pkg/bus/bus.go Bus.Ack