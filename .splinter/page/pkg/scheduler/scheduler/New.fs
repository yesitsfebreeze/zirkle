// §head page/pkg/scheduler/scheduler.go:38-40 New
// §sig func New(opts ...Option) *Scheduler
	return &Scheduler{cron: cron.New(opts...)}
// §foot page/pkg/scheduler/scheduler.go New