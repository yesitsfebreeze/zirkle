// §head page/pkg/bus/bus.go:35-61 Bus.Send
// §sig func (b *Bus) Send(to string, env Envelope) error
	env.From = b.identity.Fingerprint()
	env.To = to

	if err := env.Sign(b.identity); err != nil {
		return fmt.Errorf("bus send: %w", err)
	}

	dir := filepath.Join(b.spool, to)
	if err := os.MkdirAll(dir, 0700); err != nil {
		return fmt.Errorf("bus send: create inbox %s: %w", dir, err)
	}

	id := uuid.New().String()
	path := filepath.Join(dir, id+".env")

	data, err := json.Marshal(env)
	if err != nil {
		return fmt.Errorf("bus send: marshal: %w", err)
	}

	if err := os.WriteFile(path, data, 0600); err != nil {
		return fmt.Errorf("bus send: write: %w", err)
	}

	return nil
// §foot page/pkg/bus/bus.go Bus.Send