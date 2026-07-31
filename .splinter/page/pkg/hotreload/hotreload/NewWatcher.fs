// §head page/pkg/hotreload/hotreload.go:35-55 NewWatcher
// §sig func NewWatcher(cfg Config) *Watcher
	if cfg.RootDir == "" {
		cfg.RootDir = "."
	}
	if len(cfg.WatchExts) == 0 {
		cfg.WatchExts = []string{".go", "go.mod", "go.sum"}
	}
	if len(cfg.IgnoredDirs) == 0 {
		cfg.IgnoredDirs = []string{".git", "bin", ".relay", "vendor", "tmp"}
	}
	if cfg.PollInterval == 0 {
		cfg.PollInterval = 300 * time.Millisecond
	}
	if cfg.DebounceWindow == 0 {
		cfg.DebounceWindow = 250 * time.Millisecond
	}
	return &Watcher{
		cfg:    cfg,
		mtimes: make(map[string]time.Time),
	}
// §foot page/pkg/hotreload/hotreload.go NewWatcher