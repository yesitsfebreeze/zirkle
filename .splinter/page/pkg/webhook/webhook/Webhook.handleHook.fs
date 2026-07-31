// §head page/pkg/webhook/webhook.go:107-197 Webhook.handleHook
// §sig func (w *Webhook) handleHook(rw http.ResponseWriter, r *http.Request)
	start := time.Now()

	if r.Method != http.MethodPost {
		http.Error(rw, "method not allowed\n", http.StatusMethodNotAllowed)
		return
	}

	pathSecret := strings.TrimPrefix(r.URL.Path, "/hook/")
	if !w.validateSecret(pathSecret) {
		slog.Warn("webhook invalid secret",
			"path", r.URL.Path,
			"latency", time.Since(start),
		)
		http.Error(rw, "unauthorized\n", http.StatusUnauthorized)
		return
	}

	body, err := io.ReadAll(r.Body)
	r.Body.Close()
	if err != nil {
		slog.Warn("webhook read error",
			"path", r.URL.Path,
			"error", err,
		)
		http.Error(rw, "bad request\n", http.StatusBadRequest)
		return
	}

	ikey := r.Header.Get("X-Idempotency-Key")

	// Dedup check
	if ikey != "" {
		if w.isDuplicate(ikey) {
			slog.Info("webhook duplicate",
				"path", r.URL.Path,
				"idempotency_key", ikey,
				"status", http.StatusOK,
				"latency", time.Since(start),
			)
			rw.Header().Set("Content-Type", "application/json")
			rw.WriteHeader(http.StatusOK)
			json.NewEncoder(rw).Encode(map[string]string{"status": "skipped"})
			return
		}
	}

	msg := adapter.InMessage{
		Source:    "webhook",
		ChannelID: pathSecret,
		Prompt:    string(body),
		Meta:      map[string]any{"idempotency_key": ikey},
	}

	// Deliver with timeout.
	done := make(chan struct{}, 1)
	go func() {
		// close(done) must happen even on a panic, or the handler blocks to
		// its full deadline; Guard turns the swallowed panic into a record.
		defer close(done)
		defer fault.Guard(w.Faults, "", "webhook.deliver")
		w.deliver(msg)
	}()

	select {
	case <-done:
		if ikey != "" {
			w.mu.Lock()
			w.seen[ikey] = time.Now()
			w.mu.Unlock()
		}
		slog.Info("webhook delivered",
			"path", r.URL.Path,
			"idempotency_key", ikey,
			"status", http.StatusOK,
			"latency", time.Since(start),
		)
		rw.Header().Set("Content-Type", "application/json")
		rw.WriteHeader(http.StatusOK)
		json.NewEncoder(rw).Encode(map[string]string{"status": "ok"})

	case <-time.After(w.timeout):
		slog.Warn("webhook timeout",
			"path", r.URL.Path,
			"idempotency_key", ikey,
			"status", http.StatusRequestTimeout,
			"latency", time.Since(start),
		)
		http.Error(rw, "request timeout\n", http.StatusRequestTimeout)
	}
// §foot page/pkg/webhook/webhook.go Webhook.handleHook