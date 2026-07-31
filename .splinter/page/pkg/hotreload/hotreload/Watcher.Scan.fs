// §head page/pkg/hotreload/hotreload.go:58-116 Watcher.Scan
// §sig func (w *Watcher) Scan() bool
	w.mu.Lock()
	defer w.mu.Unlock()

	changed := false
	seen := make(map[string]bool)

	_ = filepath.WalkDir(w.cfg.RootDir, func(path string, d fs.DirEntry, err error) error {
		if err != nil {
			return nil
		}
		name := d.Name()
		if d.IsDir() {
			for _, ignored := range w.cfg.IgnoredDirs {
				if name == ignored {
					return filepath.SkipDir
				}
			}
			return nil
		}

		matched := false
		for _, ext := range w.cfg.WatchExts {
			if strings.HasSuffix(name, ext) {
				matched = true
				break
			}
		}
		if !matched {
			return nil
		}

		info, err := d.Info()
		if err != nil {
			return nil
		}

		seen[path] = true
		oldTime, exists := w.mtimes[path]
		newTime := info.ModTime()

		if !exists || !newTime.Equal(oldTime) {
			w.mtimes[path] = newTime
			if exists {
				changed = true
			}
		}
		return nil
	})

	for path := range w.mtimes {
		if !seen[path] {
			delete(w.mtimes, path)
			changed = true
		}
	}

	return changed
// §foot page/pkg/hotreload/hotreload.go Watcher.Scan