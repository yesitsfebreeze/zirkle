// §head page/pkg/webhook/webhook.go:72-104 Webhook.Run
// §sig func (w *Webhook) Run(ctx context.Context, deliver func(adapter.InMessage)) error
	if w.secret == "" {
		slog.Info("webhook disabled")
		return nil
	}

	w.deliver = deliver

	srv := &http.Server{
		Addr:    fmt.Sprintf(":%d", w.port),
		Handler: w.Handler(),
	}

	errCh := make(chan error, 1)
	go func() {
		slog.Info("webhook listening", "port", w.port)
		if err := srv.ListenAndServe(); err != nil && err != http.ErrServerClosed {
			errCh <- err
		}
	}()

	go w.cleanupLoop(ctx)

	select {
	case <-ctx.Done():
		shutdownCtx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
		defer cancel()
		_ = srv.Shutdown(shutdownCtx)
		return nil
	case err := <-errCh:
		return err
	}
// §foot page/pkg/webhook/webhook.go Webhook.Run