// §head page/pkg/hotreload/hotreload.go:119-138 Watcher.Watch
// §sig func (w *Watcher) Watch(ctx context.Context, events chan<- struct{})
	w.Scan()

	ticker := time.NewTicker(w.cfg.PollInterval)
	defer ticker.Stop()

	for {
		select {
		case <-ctx.Done():
			return
		case <-ticker.C:
			if w.Scan() {
				select {
				case events <- struct{}{}:
				default:
				}
			}
		}
	}
// §foot page/pkg/hotreload/hotreload.go Watcher.Watch