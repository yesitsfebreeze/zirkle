// §head page/pkg/bus/envelope.go:46-73 Envelope.Verify
// §sig func (env *Envelope) Verify() (bool, error)
	if env.Signature == "" {
		return false, errors.New("bus: empty signature")
	}
	if env.Fingerprint == "" {
		return false, errors.New("bus: empty fingerprint")
	}

	pub, err := hex.DecodeString(env.Fingerprint)
	if err != nil {
		return false, fmt.Errorf("bus: decode fingerprint: %w", err)
	}

	sig, err := base64.StdEncoding.DecodeString(env.Signature)
	if err != nil {
		return false, fmt.Errorf("bus: decode signature: %w", err)
	}

	// Re-marshal without signature for verification.
	check := *env
	check.Signature = ""
	data, err := json.Marshal(&check)
	if err != nil {
		return false, fmt.Errorf("bus: marshal for verify: %w", err)
	}

	return ed25519.Verify(ed25519.PublicKey(pub), data, sig), nil
// §foot page/pkg/bus/envelope.go Envelope.Verify