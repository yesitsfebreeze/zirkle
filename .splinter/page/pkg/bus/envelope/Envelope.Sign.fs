// §head page/pkg/bus/envelope.go:30-42 Envelope.Sign
// §sig func (env *Envelope) Sign(id *Identity) error
	env.Fingerprint = id.Fingerprint()
	env.Signature = "" // ensure clean

	data, err := json.Marshal(env)
	if err != nil {
		return fmt.Errorf("bus: marshal for sign: %w", err)
	}

	sig := id.Sign(data)
	env.Signature = base64.StdEncoding.EncodeToString(sig)
	return nil
// §foot page/pkg/bus/envelope.go Envelope.Sign