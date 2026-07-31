// §head page/pkg/bus/bus.go:67-103 Bus.Poll
// §sig func (b *Bus) Poll() ([]Envelope, error)
	entries, err := os.ReadDir(b.inbox)
	if err != nil {
		if os.IsNotExist(err) {
			return nil, nil
		}
		return nil, fmt.Errorf("bus poll: read inbox: %w", err)
	}

	var envs []Envelope
	for _, e := range entries {
		if e.IsDir() || !strings.HasSuffix(e.Name(), ".env") {
			continue
		}

		path := filepath.Join(b.inbox, e.Name())
		data, err := os.ReadFile(path)
		if err != nil {
			continue // skip unreadable
		}

		var env Envelope
		if err := json.Unmarshal(data, &env); err != nil {
			continue // skip corrupt
		}

		ok, err := env.Verify()
		if err != nil || !ok {
			continue // skip invalid signature
		}

		env.ID = strings.TrimSuffix(e.Name(), ".env")
		envs = append(envs, env)
	}

	return envs, nil
// §foot page/pkg/bus/bus.go Bus.Poll